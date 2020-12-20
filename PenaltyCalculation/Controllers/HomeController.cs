using PenaltyCalculation.Base;
using PenaltyCalculation.Base.Models;
using PenaltyCalculation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PenaltyCalculation.Controllers
{
    public class HomeController : Controller
    {
        private readonly UnitOfWork unitOfWork;

        public HomeController()
        {
            unitOfWork = new UnitOfWork();
        }

        public ActionResult Index()
        {
            ViewBag.Books = new SelectList(unitOfWork.BookRepository.GetAll(), "Id", "Name");
            ViewBag.Countries = new SelectList(unitOfWork.CountryRepository.GetAll(), "Id", "Name");

            return View(new PenaltyViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Index(PenaltyViewModel model)
        {
            ViewBag.Books = new SelectList(unitOfWork.BookRepository.GetAll(), "Id", "Name");
            ViewBag.Countries = new SelectList(unitOfWork.CountryRepository.GetAll(), "Id", "Name");

            if (ModelState.IsValid)
            {
                var book = unitOfWork.BookRepository.GetByID(model.BookId);
                book.CheckedOutDate = model.CheckedOutDate;
                book.ReturnedDate = model.ReturnedDate;
                book.IsBorrowed = true;

                unitOfWork.BookRepository.Update(book);
                unitOfWork.Save();

                var country = unitOfWork.CountryRepository.GetByID(model.CountryId);

                if (country != null && model.CheckedOutDate.HasValue && model.ReturnedDate.HasValue)
                {
                    var penaltyParameter = unitOfWork.ParameterRepository.Get(x => x.ParamCode == "Penalty");
                    var currencyParameter = unitOfWork.ParameterRepository.Get(x => x.ParamCode == "Currency" && x.ParamValue1 == country.Code);
                    var weekendsParameter = unitOfWork.ParameterRepository.Get(x => x.ParamCode == "Weekends" && x.ParamValue1 == country.Code);
                    var holidayParameter = unitOfWork.ParameterRepository.GetMany(x => x.ParamCode == "Holidays" && x.ParamValue1 == country.Code);

                    var weekendsParameters = weekendsParameter.ParamValue2.Split(',').Select(Int32.Parse).ToArray();
                    var holidaysParameters = holidayParameter.Select(x => Newtonsoft.Json.JsonConvert.DeserializeObject<Holiday>(x.ParamValue2)).ToList();

                    decimal penaltyAmount = 0;
                    decimal.TryParse(penaltyParameter?.ParamValue1, out penaltyAmount);

                    int businessDays = GetWorkingDays(model.CheckedOutDate.Value, model.ReturnedDate.Value, weekendsParameters, holidaysParameters);

                    ViewBag.CalculatedBusinessDays = businessDays;
                    ViewBag.CalculatedPenalty = $"{businessDays * penaltyAmount} {currencyParameter?.ParamValue2}";
                }
            }

            return View(model);
        }

        public JsonResult GetBookDetails(long bookId)
        {
            ViewBag.Books = new SelectList(unitOfWork.BookRepository.GetAll(), "Id", "Name");
            ViewBag.Countries = new SelectList(unitOfWork.CountryRepository.GetAll(), "Id", "Name");

            var book = unitOfWork.BookRepository.GetByID(bookId);

            var result = new
            {
                BookId = book.Id,
                CountryId = 0,
                CheckedOutDate = book.CheckedOutDate.HasValue ? book.CheckedOutDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "",
                ReturnedDate = book.ReturnedDate.HasValue ? book.ReturnedDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "",
            };

            return Json(result);
        }

        public ActionResult About()
        {
            ViewBag.Message = $"Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public int GetWorkingDays(DateTime from, DateTime to, int[] weekends, List<Holiday> holidays)
        {
            var totalDays = 0;

            for (var date = from; date < to; date = date.AddDays(1))
            {
                if (!weekends.Contains((int)date.DayOfWeek) &&
                    !holidays.Any(x => x.Day == date.Day && x.Month == date.Month))
                    totalDays++;
            }

            // Any business day after 10 days will be considered as a late day.
            return totalDays > 10 ? totalDays - 10 : 0;
        }
    }
}
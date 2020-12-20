using PenaltyCalculation.Base.Models;

namespace PenaltyCalculation.Base
{
    public partial class UnitOfWork
    {
        //private GenericRepository<ApplicationUser> userRepository;
        //public GenericRepository<ApplicationUser> UserRepository
        //{
        //    get
        //    {
        //        if (this.userRepository == null)
        //            this.userRepository = new GenericRepository<ApplicationUser>(context);
        //        return userRepository;
        //    }
        //}

        private GenericRepository<Book> bookRepository;
        public GenericRepository<Book> BookRepository
        {
            get
            {
                if (this.bookRepository == null)
                    this.bookRepository = new GenericRepository<Book>(context);
                return bookRepository;
            }
        }

        private GenericRepository<Country> countryRepository;
        public GenericRepository<Country> CountryRepository
        {
            get
            {
                if (this.countryRepository == null)
                    this.countryRepository = new GenericRepository<Country>(context);
                return countryRepository;
            }
        }

        private GenericRepository<Parameter> parameterRepository;
        public GenericRepository<Parameter> ParameterRepository
        {
            get
            {
                if (this.parameterRepository == null)
                    this.parameterRepository = new GenericRepository<Parameter>(context);
                return parameterRepository;
            }
        }
    }
}

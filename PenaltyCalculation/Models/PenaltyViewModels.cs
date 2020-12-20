using System;
using System.ComponentModel.DataAnnotations;

namespace PenaltyCalculation.Models
{
    public class PenaltyViewModel
    {
        [Required]
        [Display(Name = "Book")]
        public long? BookId { get; set; }

        [Required]
        [Display(Name = "Country")]
        public long? CountryId { get; set; }

        [Required]
        [Display(Name = "Checked Out Date")]
        public DateTime? CheckedOutDate { get; set; }

        [Required]
        [Display(Name = "Returned Date")]
        public DateTime? ReturnedDate { get; set; }
    }
}

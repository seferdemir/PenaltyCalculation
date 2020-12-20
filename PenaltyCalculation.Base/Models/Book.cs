using System;

namespace PenaltyCalculation.Base.Models
{
    public class Book
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime? CheckedOutDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public bool IsBorrowed { get; set; }
    }
}

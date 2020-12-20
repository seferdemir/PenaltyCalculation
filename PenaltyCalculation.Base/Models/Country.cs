using System.ComponentModel.DataAnnotations.Schema;

namespace PenaltyCalculation.Base.Models
{
    [Table("Countries", Schema = "dbo")]
    public class Country
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}

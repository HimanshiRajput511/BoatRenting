using System;
using System.ComponentModel.DataAnnotations;

namespace ModelLayer
{
    public class RentingBusiness
    {
        [Key]
        public int RentId { get; set; }
        public string CustomerName { get; set; }
        public int BoatNumber { get; set; }
        public decimal? HourlyRate { get; set; }
        public DateTime? RentOutDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public TimeSpan? TotalTime { get; set; }
        public decimal? Price { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}

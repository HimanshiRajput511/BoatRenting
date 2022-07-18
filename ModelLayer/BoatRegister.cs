using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace ModelLayer
{
    public class BoatRegister
    {
        [Key]
        public int BoatId { get; set; }
        [Required]
        [Remote("IsAlreadyExist", "Home",  ErrorMessage = "Boat Name already exists in database.")]
        public string BoatName { get; set; }
        [Required]
        public decimal HourlyRate { get; set; }
        public int BoatNumber { get; set; }
        public bool IsRented { get; set; }
        public bool IsReturned { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}

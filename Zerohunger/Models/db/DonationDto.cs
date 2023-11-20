using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zerohunger.Models.db
{
    public class DonationDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

       
        public int DonorId { get; set; }

        public string Status { get; set; }

       
    }
}
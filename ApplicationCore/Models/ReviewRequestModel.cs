using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class ReviewRequestModel
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }

        [Required]
        [Range(0.0, 10.0)]
        public decimal Rating { get; set; }

        [Required]
        [MaxLength(550)]
        public string ReviewText { get; set; }
       
    }
}

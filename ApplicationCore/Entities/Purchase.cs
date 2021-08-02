using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    [Table("Purchase")]
    public class Purchase
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public Guid PurchaseNumber { get; set; }
        [Column(TypeName = "decimal(18,2)")][Required]
        public decimal TotalPrice { get; set; }
        [Column(TypeName = "datetime")][Required]
        public DateTime PurchaseDateTime { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}

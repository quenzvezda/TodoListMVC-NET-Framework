using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNet_Framework_WebApp.Models
{
    public class Tire
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Brand { get; set; }

        [Required]
        public int Health { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        // Relasi ke Car
        [ForeignKey("Car")]
        public int CarId { get; set; }
        public virtual Car Car { get; set; }
    }
}
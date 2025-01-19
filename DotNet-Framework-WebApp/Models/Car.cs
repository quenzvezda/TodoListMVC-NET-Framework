﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotNet_Framework_WebApp.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Brand { get; set; }

        [Required]
        [StringLength(50)]
        public string Color { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        
        // Relasi ke Tire
        public virtual ICollection<Tire> Tires { get; set; }
    }
}
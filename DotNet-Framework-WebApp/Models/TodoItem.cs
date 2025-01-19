using System;
using System.ComponentModel.DataAnnotations;

namespace DotNet_Framework_WebApp.Models
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        public bool IsComplete { get; set; }
        
        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public DateTime? FinishDate { get; set; }
    }
}
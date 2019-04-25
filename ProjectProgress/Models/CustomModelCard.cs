using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectProgress.Models
{
    public class CustomModelCard
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int IdBoard { get; set; }
        [Required]
        public string Title { get; set; }
        public virtual Board Board { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
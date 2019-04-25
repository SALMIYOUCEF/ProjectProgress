using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectProgress.Models
{
    public class CustomModelTask
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public int IdCard { get; set; }

        public virtual Cards Card { get; set; }
        public virtual ICollection<Event> Event { get; set; }
    }
}
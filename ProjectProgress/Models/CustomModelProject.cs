using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectProgress.Models
{
    public class CustomModelProject
    {
        
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public System.DateTime DteConsult { get; set; }
        public string Color { get; set; }
        [Required]
        public string UserId { get; set; }

        public virtual ICollection<Board> Boards { get; set; }
    }
}
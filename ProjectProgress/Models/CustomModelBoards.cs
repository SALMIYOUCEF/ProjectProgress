using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using DataAccessLayer;

namespace ProjectProgress.Models
{
    public class CustomModelBoards
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cards> Cards { get; set; }
    }
}
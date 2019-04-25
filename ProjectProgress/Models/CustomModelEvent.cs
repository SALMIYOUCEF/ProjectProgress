using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectProgress.Models
{

    //[MetadataType(typeof(EventMetadata))]
    public class CustomModelEvent
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int TaskId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Start date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMM HH:mm}")]
        public Nullable<System.DateTime> DteStart { get; set; }
        [Required]
        [Display(Name = "End date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMM HH:mm}")]
        public Nullable<System.DateTime> DteEnd { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string UserId { get; set; }

        public virtual Task Task { get; set; }

    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Event
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public Nullable<System.DateTime> DteStart { get; set; }
        public Nullable<System.DateTime> DteEnd { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
        public string UserId { get; set; }
    
        public virtual Task Task { get; set; }
    }
}

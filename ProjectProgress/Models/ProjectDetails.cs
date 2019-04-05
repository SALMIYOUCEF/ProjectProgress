using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectProgress.Models
{
    public class ProjectDetails
    {
        public string ProjectName { get; set; }
        public string ProjectColor { get; set; }
        public string BoardName { get; set; }
        public string CardName { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
    }
}
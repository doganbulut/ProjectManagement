﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class tblUnit
    {
        public tblUnit()
        {
            this.tblDevelopers = new HashSet<tblDeveloper>();
            this.tblProjects = new HashSet<tblProject>();
        }

        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "İsim")]
        public string NAME { get; set; }
    
        public virtual ICollection<tblDeveloper> tblDevelopers { get; set; }
        public virtual ICollection<tblProject> tblProjects { get; set; }
    }
}

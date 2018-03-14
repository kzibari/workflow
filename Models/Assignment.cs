//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Workflow.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Assignment
    {
        public int AssignmentID { get; set; }
        [Required]
        [Display(Name = "Set Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> SetDate { get; set; }
        [Display(Name = "Due Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public Nullable<System.DateTime> DueDate { get; set; }
        public bool Completed { get; set; }
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        public int TaskID { get; set; }
        [Display(Name = "Completion Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> CompletionDate { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Task Task { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mahc_Final.DBContext
{
    using System;
    using System.Collections.Generic;
    
    [MetadataType(typeof(Models.JobMetadata))]
    public partial class Job
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        
        public Job()
        {
            this.Job_applications = new HashSet<Job_applications>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }
        public string Desc { get; set; }
        public bool Status { get; set; }
        public Nullable<System.DateTime> Date_created { get; set; }
        public Nullable<int> Created_by { get; set; }
        public Nullable<System.DateTime> Date_last_modified { get; set; }
        public Nullable<int> Modified_by { get; set; }
    
        public virtual HosMember HosMember { get; set; }
        public virtual HosMember HosMember1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Job_applications> Job_applications { get; set; }
        public virtual Job_types Job_types { get; set; }
    }
}

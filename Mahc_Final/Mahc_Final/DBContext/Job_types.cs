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
    [MetadataType(typeof(Models.Job_typesMetadata))]
    public partial class Job_types
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Job_types()
        {
            this.Jobs = new HashSet<Job>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Job> Jobs { get; set; }
    }
}

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
    
    public partial class ERArchive
    {
        public int Id { get; set; }
        public System.DateTime StoreDate { get; set; }
        public int AverageWaitTime { get; set; }
        public int NumberOfPatients { get; set; }
        public int SavedBy { get; set; }
    
        public virtual HosMember HosMember { get; set; }
    }
}

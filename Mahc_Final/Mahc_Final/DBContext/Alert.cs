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
    
    public partial class Alert
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public System.DateTime Due_time { get; set; }
        public string Desc { get; set; }
        public bool Status { get; set; }
        public Nullable<System.DateTime> Date_created { get; set; }
        public Nullable<int> Created_by { get; set; }
        public Nullable<System.DateTime> Date_last_modified { get; set; }
        public Nullable<int> Modified_by { get; set; }
    
        public virtual HosMember HosMember { get; set; }
        public virtual HosMember HosMember1 { get; set; }
    }
}

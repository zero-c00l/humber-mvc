//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using Mahc_Final.Metadata;

namespace Mahc_Final.DBContext
{
    using System;
    using System.Collections.Generic;
    [MetadataType(typeof(ERParamMetadata))]
    public partial class ERParam
    {
        public int Id { get; set; }
        public System.DateTime ArrivalTime { get; set; }
        public System.DateTime TreatmentTime { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SAT.DATA.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class ScheduledClassStatuses
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ScheduledClassStatuses()
        {
            this.ScheduledClasses = new HashSet<ScheduledClasses>();
        }
    
        public int SCID { get; set; }
        public string SCSName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScheduledClasses> ScheduledClasses { get; set; }
    }
}

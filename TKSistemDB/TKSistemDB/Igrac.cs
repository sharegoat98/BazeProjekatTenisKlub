//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TKSistemDB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Igrac
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Igrac()
        {
            this.Zicas = new HashSet<Zica>();
        }
    
        public int IdIgr { get; set; }
        public string ImeIgr { get; set; }
        public string PrzIgr { get; set; }
        public string TipIgr { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zica> Zicas { get; set; }
    }
}
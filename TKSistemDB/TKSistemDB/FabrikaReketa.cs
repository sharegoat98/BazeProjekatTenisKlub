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
    
    public partial class FabrikaReketa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FabrikaReketa()
        {
            this.Rekets = new HashSet<Reket>();
            this.ima_ugovor = new HashSet<ima_ugovor>();
        }
    
        public int IdFab { get; set; }
        public string AdrFab { get; set; }
        public string NazFab { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reket> Rekets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ima_ugovor> ima_ugovor { get; set; }
    }
}
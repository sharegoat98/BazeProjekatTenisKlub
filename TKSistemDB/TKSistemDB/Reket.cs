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
    
    public partial class Reket
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reket()
        {
            this.Zicas = new HashSet<Zica>();
        }
    
        public int SifRek { get; set; }
        public string MarRek { get; set; }
        public string BojaRek { get; set; }
        public string ModRek { get; set; }
        public int FabrikaReketaIdFab { get; set; }
    
        public virtual FabrikaReketa FabrikaReketa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zica> Zicas { get; set; }
    }
}

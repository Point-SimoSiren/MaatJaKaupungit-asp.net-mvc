//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MaatJaKaupungit.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Maat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Maat()
        {
            this.Kaupungit = new HashSet<Kaupungit>();
        }

        public int maaId { get; set; }
        public string maaNimi { get; set; }
        public int asukasluku { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kaupungit> Kaupungit { get; set; }
    }
}

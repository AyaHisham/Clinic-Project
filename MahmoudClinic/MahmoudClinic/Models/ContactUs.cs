//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MahmoudClinic.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ContactUs
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ContactUs()
        {
            this.ContactUsPhone = new HashSet<ContactUsPhone>();
        }
    
        public int ID { get; set; }
        public decimal Longtiude { get; set; }
        public decimal Latitude { get; set; }
        public string Email { get; set; }
        public int WorkStart { get; set; }
        public int WorkEnd { get; set; }
        public string Address { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContactUsPhone> ContactUsPhone { get; set; }
    }
}

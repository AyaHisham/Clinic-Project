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
    using System.ComponentModel;
    public partial class News
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public News()
        {
            this.NewsPicture = new HashSet<NewsPicture>();
        }
    
        public int ID { get; set; }
        public string Content { get; set; }
        [DisplayName("Video")]
        public string VideoURL { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NewsPicture> NewsPicture { get; set; }
    }
}
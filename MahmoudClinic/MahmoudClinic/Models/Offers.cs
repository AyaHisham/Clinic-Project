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
    public partial class Offers
    {
        public int ID { get; set; }
        [DisplayName("Picture")]
        public string OfferPicURL { get; set; }

        [DisplayName("Offer Content")]
        public string OfferContent { get; set; }
    }
}

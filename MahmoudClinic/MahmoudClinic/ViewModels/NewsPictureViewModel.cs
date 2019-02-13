using MahmoudClinic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MahmoudClinic.ViewModels
{
    public class NewsPictureViewModel
    {
        public NewsPictureViewModel()
        {
            NewsPicture = new List<NewsPicture>();
        }
        public int ID { get; set; }
        public string Content { get; set; }
        [DisplayName("Video")]
        public string VideoURL { get; set; }
        public ICollection<NewsPicture> NewsPicture { get; set; }
    }
}
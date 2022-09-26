using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Youtube_Website.Models
{
    public class Video
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public string source { get; set; }
        [Required]
        public string uploadedby { get; set; }
        [Required]
        public string uploadedat { get; set; }
        
    }
    /*public class emailvideo
    {
        public string uploaderemail { get; set; }
    }*/
}
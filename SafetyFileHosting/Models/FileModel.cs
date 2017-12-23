using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace SafetyFileHosting.Models
{
    public class FileModel
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        [Display(Name = "Insertion Date")]
        public DateTime? CreationDate { get; set; }

        [Display(Name = "Name")]
        public string FileName { get; set; }

        public string PathToFile { get; set; }
    }
}
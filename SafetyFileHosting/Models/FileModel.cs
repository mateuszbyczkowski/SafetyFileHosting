using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SafetyFileHosting.Models
{
    public class FileModel
    {
        [Key]
        public int Id { get; set; }
        public int Description { get; set; }
        public string Author { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
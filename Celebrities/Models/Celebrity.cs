using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Celebrities.Models
{
    public class Celebrity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BirthDay { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; }
        public string picUrl { get; set; }
        public bool isViewable { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myTwitterProject.Models
{
    public class Comment
    {
        public int StatusId { get; set; }
        public string Name { get; set; }
        public string comment{ get; set; }
        public string time { get; set; }
        public string date { get; set; }
    }
}
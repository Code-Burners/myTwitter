using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace myTwitterProject.Models
{
    public class status
    {
     
        public int StatusId { get; set; }
        public string Name { get; set; }
        public string statuses { get; set; }
        public string like { get; set; }
        public string time { get; set; }
        public string date { get; set; }


    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace myTwitterProject.Models
{
    public class StatusContext : DbContext
    {
           
        public StatusContext() : base("name=StatusContext")
        {
        }

        public System.Data.Entity.DbSet<myTwitterProject.Models.status> status { get; set; }
    
    }
}

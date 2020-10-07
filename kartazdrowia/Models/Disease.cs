using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kartazdrowia.Models
{
    public class Disease
    {
        public int Id { get; set; }

        public String Name { get; set; }
        public String Description { get; set; }
        public bool Active { get; set; }

        [DisplayFormat(DataFormatString = "D:dd/MM/yyyy")]
        public DateTime Startdate { get; set; }
        [DisplayFormat(DataFormatString = "D:dd/MM/yyyy")]
        public DateTime Enddate { get; set; }
        public String UserId { get; set; }
        public IdentityUser User { get; set; }

        public Disease(string name, string description, DateTime startdate)
        {
            Name = name;
            Description = description;
            Active = true;
            Startdate = startdate;

        }

    }
}

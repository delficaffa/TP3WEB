using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class TurnModel
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dtos
{
    public class EmployeeDto
    {

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Country { get; set; }

        public DateTime? Date { get; set; }

        public string Turn { get; set; }

        
    }
}

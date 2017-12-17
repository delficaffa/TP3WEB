using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dtos
{
    public class HorariosDto
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public DateTime StartlHour { get; set; }
        
        public DateTime? FinishHour { get; set; }
    }
}

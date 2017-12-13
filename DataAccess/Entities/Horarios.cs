using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class Horarios
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public EnumTurns Turn { get; set; }

        [Required]
        public DateTime StartlHour { get; set; }

        [Required]
        public DateTime FinishHour { get; set; }

        public virtual List<Employee> Employees { get; set; }

    }
}

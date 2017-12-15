using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table("Horarios")]
    public class Horarios
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime StartlHour { get; set; }

        [Required]
        public DateTime FinishHour { get; set; }
        
        [ForeignKey("EmployeeId")]
        public virtual Employee Employees { get; set; }
    }
}

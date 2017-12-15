namespace DataAccess
{
    using DataAccess;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        public int Country { get; set; }

        public DateTime? Date { get; set; }

        [Required]
        public EnumTurns Turn { get; set; }

       
        public virtual IEnumerable<Horarios> Shift { get; set; }

        [ForeignKey("Country")]
        public virtual Country Country1 { get; set; }
    }
}

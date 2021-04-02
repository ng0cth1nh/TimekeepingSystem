namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AttendanceDaily")]
    public partial class AttendanceDaily
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ID { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MonthID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeID { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "date")]
        public DateTime Date { get; set; }

        public bool isPresent { get; set; }

        public string Note { get; set; }

        public virtual AttendanceMonthly AttendanceMonthly { get; set; }

        public virtual Employee Employee { get; set; }
    }
}

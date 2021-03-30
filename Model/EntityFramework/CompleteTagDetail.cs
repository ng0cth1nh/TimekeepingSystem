namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompleteTagDetail")]
    public partial class CompleteTagDetail
    {
        [Key]
        [Column(Order = 0)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TagID { get; set; }

        public int StepID { get; set; }

        public int EmployeeID { get; set; }

        public virtual CompleteTag CompleteTag { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Step Step { get; set; }

        public virtual Tag Tag { get; set; }
    }
}

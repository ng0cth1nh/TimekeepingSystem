namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompleteTag")]
    public partial class CompleteTag
    {
        public int ID { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TagID { get; set; }

        [Required]
        [StringLength(50)]
        public string Table { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductID { get; set; }

        public int Quantity { get; set; }

        public int CompleteQuantity { get; set; }

        public virtual Product Product { get; set; }

        public virtual Tag Tag { get; set; }
    }
}

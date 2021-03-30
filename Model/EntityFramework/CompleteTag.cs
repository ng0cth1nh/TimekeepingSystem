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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CompleteTag()
        {
            CompleteTagDetails = new HashSet<CompleteTagDetail>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required]
        [StringLength(10)]
        public string Table { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public int ProductID { get; set; }

        public int Quantity { get; set; }

        public int ErrorQuantity { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompleteTagDetail> CompleteTagDetails { get; set; }

        public virtual Tag Tag { get; set; }
    }
}

namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            AttendanceDailies = new HashSet<AttendanceDaily>();
            CompleteTagDetails = new HashSet<CompleteTagDetail>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public bool Gender { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateJoin { get; set; }

        public int RoleID { get; set; }

        public int TypeID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttendanceDaily> AttendanceDailies { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompleteTagDetail> CompleteTagDetails { get; set; }

        public virtual EmployeeRole EmployeeRole { get; set; }

        public virtual EmployeeType EmployeeType { get; set; }
    }
}

namespace BigSchool.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Course")]
    public partial class Course
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Course()
        {
            Attendences = new HashSet<Attendence>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string LecturerId { get; set; }


        [Required(ErrorMessage = "Tên địa điểm không được để trống")]
        [Display(Name = "Địa điểm")]
        [StringLength(255, ErrorMessage = "Tên địa điểm không được quá 255 ký tự.")]
        public string Place { get; set; }

        [Required(ErrorMessage = "Thời gian không được để trống")]
        [Display(Name = "Thời gian")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }

        [Display(Name = "Ngành")]
        public int CategoryId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attendence> Attendences { get; set; }

        public virtual Category Category { get; set; }

        //add list
        public List<Category> ListCategory = new List<Category>();

    }
}

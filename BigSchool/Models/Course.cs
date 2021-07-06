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
        [DataType(DataType.DateTime, ErrorMessage = "Sai định dạng")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }

        [Display(Name = "Ngành")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        //add list category
        public List<Category> ListCategory = new List<Category>();

    }
}

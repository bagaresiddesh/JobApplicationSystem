using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplicationSystem.DAL.Model
{
    public class Education
    {
        [Key]
        public int Id { get; set; }

        public int EId { get; set; }

        [Required]
        [MaxLength(10)]
        public string Qualification { get; set; }

        [Required]
        [StringLength(8)]
        [Display(Name ="Passing Year")]
        public string PassingYear { get; set; }

        [Required]
        [Range(35,100,ErrorMessage ="Invalid")]
        public float Percentage { get; set; }

        [ForeignKey("EId")]
        public virtual EducationalDetails EducationalDetails { get; set; }  
    }
}

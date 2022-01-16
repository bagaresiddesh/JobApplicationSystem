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

        [MaxLength(10)]
        public string Qualification { get; set; }

        [MaxLength(4)]
        public string PassingYear { get; set; }

        [Range(35,100,ErrorMessage ="Invalid")]
        public float Percentage { get; set; }

        [ForeignKey("EId")]
        public virtual EducationalDetails EducationalDetails { get; set; }  
    }
}

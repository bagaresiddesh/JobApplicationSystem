using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplicationSystem.DAL.Model
{
    public class EducationalDetails
    {
        [Key]
        public int Id { get; set; }

        public int UserDetailsId { get; set; }

        [Required]
        [MaxLength(5)]
        public string SSCPassingYear { get; set; }

        [Required]
        [MaxLength(5)]
        public string HSCPassingYear { get; set; }

        [Required]
        [MaxLength(5)]
        public string GraduationPassingYear { get; set; }

        [Required]
        [MaxLength(5)]
        public string PostGraduationPassingYear { get; set; }

        [Required]
        public bool IsYearGap { get; set; }

        [Required]
        public bool IsActiveBacklogs { get; set; }

        [MaxLength(30)]
        public string AcademicProjects { get; set; }

        [ForeignKey("UserDetailsId")]
        public virtual UserDetails UserDetails { get; set; }

    }
}

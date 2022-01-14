using System.ComponentModel;
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
        [DisplayName("SSC Pasing Year")]
        public string SSCPassingYear { get; set; }

        [Required]
        [MaxLength(5)]
        [DisplayName("HSC Pasing Year")]
        public string HSCPassingYear { get; set; }

        [Required]
        [MaxLength(5)]
        [DisplayName("Graduation Pasing Year")]
        public string GraduationPassingYear { get; set; }

        [Required]
        [MaxLength(5)]
        [DisplayName("Post Graduation Pasing Year")]
        public string PostGraduationPassingYear { get; set; }

        [Required]
        [DisplayName("Is Year Gap ?")]
        public bool IsYearGap { get; set; }

        [Required]
        [DisplayName("Is Active Backlog ?")]
        public bool IsActiveBacklogs { get; set; }

        [MaxLength(30)]
        [DisplayName("Academic Projects")]
        public string AcademicProjects { get; set; }

        [ForeignKey("UserDetailsId")]
        public virtual UserDetails UserDetails { get; set; }

    }
}

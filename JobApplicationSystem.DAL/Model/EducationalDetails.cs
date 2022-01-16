using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplicationSystem.DAL.Model
{
    public class EducationalDetails
    {
        [Key]
        public int EId { get; set; }

        public int UserDetailsId { get; set; }

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

        public virtual ICollection<Education> Education { get; set; }
    }
}

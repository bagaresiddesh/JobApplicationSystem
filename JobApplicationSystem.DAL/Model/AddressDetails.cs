using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplicationSystem.DAL.Model
{
    public class AddressDetails
    {
        [Key]
        public int Id { get; set; }

        public int UserDetailsId { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [MaxLength(10)]
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Address Line 1")]
        public string AddressLine1 { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Address Line 2")]
        public string AddressLine2 { get; set; }

        [ForeignKey("UserDetailsId")]
        public virtual UserDetails UserDetails { get; set; }
    }
}

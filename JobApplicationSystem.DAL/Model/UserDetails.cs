using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JobApplicationSystem.DAL.Model
{
    public class UserDetails
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(20)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "01/01/1980", "01/01/2000")]
        [DisplayName("Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [RegularExpression(".+@.+\\..+", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [RegularExpression("((\\+\\d{1,2}\\s?)?1?\\-?\\.?\\s?\\(?\\d{3}\\)?[\\s.-]?)?\\d{3}[\\s.-]?\\d{4}", ErrorMessage = "Invalid Phone Number")]
        public string Phone { get; set; }

    }

    public enum Gender
    {
        male = 1,
        female = 2,
        other = 3,
    }
}

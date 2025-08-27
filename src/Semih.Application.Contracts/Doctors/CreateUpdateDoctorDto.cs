using System.ComponentModel.DataAnnotations;

namespace Semih.Doctors
{
    public class CreateUpdateDoctorDto
    {
        [Required(ErrorMessage = "İsim alanı zorunludur.")]
        [StringLength(100, ErrorMessage = "İsim 100 karakterden uzun olamaz.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Soyisim alanı zorunludur.")]
        [StringLength(100, ErrorMessage = "Soyisim 100 karakterden uzun olamaz.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Uzmanlık alanı zorunludur.")]
        [StringLength(200, ErrorMessage = "Uzmanlık alanı 200 karakterden uzun olamaz.")]
        public string Specialty { get; set; } = string.Empty;
    }
}
using System.ComponentModel.DataAnnotations;

namespace MidAssignment2.Models
{
    public class DonorsModel
    {
        public int DonorId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Blood Group is required")]
        [RegularExpression("^(A|B|AB|O)[+-]$", ErrorMessage = "Invalid Blood Group format. Use A+, A-, B+, B-, AB+, AB-, O+, or O-.")]
        public string BloodGroup { get; set; } = string.Empty;

        [Required(ErrorMessage = "Contact Number is required")]
        public string ContactNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Donated Date is required")]
        public DateTime LastDonatedDate { get; set; }

    }
}

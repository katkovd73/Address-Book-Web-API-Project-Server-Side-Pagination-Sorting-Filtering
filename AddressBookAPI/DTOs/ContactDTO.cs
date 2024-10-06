using System.ComponentModel.DataAnnotations;

namespace AddressBookAPI.DTOs
{
    public class ContactDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;
    }
}

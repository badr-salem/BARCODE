using System.ComponentModel.DataAnnotations;

namespace GenerateQrCode.Models
{
    public class SendMailDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
    }
}

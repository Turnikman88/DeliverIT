using System.ComponentModel.DataAnnotations;

namespace DeliverIT.Services.DTOs
{
    public class MailDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }
        public bool isSent { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace OnlineBookShopTuto.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;
        [Required, StringLength(20)]
        public string Role { get; set; } = "Customer";
    }
}
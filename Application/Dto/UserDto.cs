
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        [Required(ErrorMessage = "UserName kiritilishi shart")]

        public string UserName { get; set; }
        [Required(ErrorMessage = "Email manzili bo'sh bo'lishi mumkin emas")]
        [EmailAddress(ErrorMessage = "Iltimos, haqiqiy email manzili kiriting (masalan: info@example.com)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parol kiritilishi shart")]
    
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "role kiritilishi shart")]
        public List<string> Roles { get; set; } = [];
    }
}

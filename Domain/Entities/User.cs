using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("user", Schema = "auth")]
    public class User
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("full_name")]
        public string FullName { get; set; }
        [Column("user_name")]
        public string UserName { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("date_of_birth")]
        public DateTime DateOfBirth { get; set; }
        [Column("phone_number")]
        public string PhoneNumber { get; set; }
        [Column("refresh_token")]
        public string? RefreshToken { get; set; }
        [Column("refresh_token_expiry")]
        public DateTime? RefreshTokenExpiry { get; set; }


        //navigation prop
        public ICollection<UserRole> UserRoles { get; set; }

    }
}

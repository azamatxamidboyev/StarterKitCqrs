using Application.DTO;
using Domain.Entities;
using System.Security.Claims;

namespace Application.Services
{
    public interface IJwtTokenService
    {
        /// <summary>
        /// Foydalanuvchi uchun yangi Access va Refresh tokenlar juftligini yaratadi.
        /// </summary>
        Task<TokenResponseDto> GenerateToken(User user, IList<string> roles);

        /// <summary>
        /// Tasodifiy Refresh Token generatsiya qiladi.
        /// </summary>
        string GenerateRefreshToken();

        /// <summary>
        /// Muddati o'tgan tokendan uning ichidagi ma'lumotlarni (Claims) xavfsiz ajratib oladi.
        /// </summary>
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}

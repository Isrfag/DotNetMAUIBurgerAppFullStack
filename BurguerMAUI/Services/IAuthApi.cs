using Burger.Shared.Dtos;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurguerMAUI.Services
{
    public interface IAuthApi
    {
        [Post("/api/auth/signup")]
        Task<ResultWithDataDto<AuthResponseDto>> SignUpAsync(SignUpRequestDto dto);

        [Post("/api/auth/signin")]
        Task<ResultWithDataDto<AuthResponseDto>> SignInAsync(SignInRequestDto dto);

        [Headers("Authorization: Bearer")]
        [Put("/api/auth/change-password")]
        Task<ResultDto> ChangePasswordAsync(ChangePasswordDto dto);
    }
}

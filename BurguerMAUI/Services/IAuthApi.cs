﻿using Burger.Shared.Dtos;
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
        [Post("/api/signup")]
        Task<ResultWithDataDto<AuthResponseDto>> SignUpAsync(SignUpRequestDto dto);

        [Post("/api/signin")]
        Task<ResultWithDataDto<AuthResponseDto>> SignInAsync(SignInRequestDto dto);
    }
}

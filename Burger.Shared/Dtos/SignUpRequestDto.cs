using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burger.Shared.Dtos
{
    public record SignUpRequestDto(string name, string Email, string Password, string Address);
}

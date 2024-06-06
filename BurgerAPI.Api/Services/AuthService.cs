using Burger.Shared.Dtos;
using BurgerAPI.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using BurgerAPI.Api.Data.Entities;

namespace BurgerAPI.Api.Services
{
    public class AuthService
    {
        private readonly DataContext _context;
        private readonly TokenService _tokenService;
        private readonly PasswordService _passwordService;
        public AuthService(DataContext context, TokenService tokenService, PasswordService passwordService)
        {
            _context = context;
            _tokenService = tokenService;
            _passwordService = passwordService;
            
        }

        public async Task<ResultWithDataDto<AuthResponseDto>> SignUpAsync(SignUpRequestDto dto)
        {
            if (await _context.Users.AsNoTracking().AnyAsync(user => user.Email == dto.Email))
            {
                return ResultWithDataDto<AuthResponseDto>.Failure("Email already exists.");
            }

            User user = new User
            {
                Name = dto.name,
                Email = dto.Email,
                Address = dto.Address
            };

            (user.Salt, user.Hash) = _passwordService.GenerateSaltAndHash(dto.Password); //Generamos la contrasela hasheada

            try
            {
                await _context.Users.AddAsync(user); //Añadimos el usuario
                await _context.SaveChangesAsync(); //Guardamos cambios en la base de datos

                //Ahora que el usuario esta creado lo logueamos

                return GenerateAuthResponse(user);
            
            } catch (Exception ex) 
            {
                return ResultWithDataDto<AuthResponseDto>.Failure(ex.Message);
            }
        }

        public async Task<ResultWithDataDto<AuthResponseDto>> SignInAsync(SignInRequestDto dto)
        {
            User? dbUser = await _context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email == dto.Email);

            if (dbUser is null)
                return ResultWithDataDto<AuthResponseDto>.Failure("User is not registered ");

            if (!_passwordService.IsEqual(dto.Password, dbUser.Salt, dbUser.Hash))
                return ResultWithDataDto<AuthResponseDto>.Failure("Password is incorrect");

            //Todo correcto, procedemos a loguear al usuario y a mandar la respuesta

            return GenerateAuthResponse(dbUser);
        }

        //Extraemos el metodo que logea y manda la respuesta para no ser redundantes
        private ResultWithDataDto<AuthResponseDto> GenerateAuthResponse (User user)
        {
            LoggedInUser loggedInUser = new LoggedInUser(user.Id, user.Name, user.Email, user.Address);
            string token = _tokenService.GenerateJwt(loggedInUser); //Hemos modificado el metodo para que reciba el usuario logueado completo

            AuthResponseDto authResponse = new AuthResponseDto(loggedInUser, token);

            return ResultWithDataDto<AuthResponseDto>.Success(authResponse); //Enviamos la respuesta positiva
        }


        //Vamos a hacer que el controlador de autentificaciones se encargue del cambio de contraseña
        public async Task<ResultDto> ChangePasswordAsync (ChangePasswordDto dto, Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(usr => usr.Id == userId);

            if (user is null)
                return ResultDto.Failure("Invalid Request");

            if (!_passwordService.IsEqual(dto.OldPassword, user.Salt, user.Hash))
            {
                return ResultDto.Failure("Incorrect password");
            }

            (user.Salt, user.Hash) = _passwordService.GenerateSaltAndHash(dto.NewPassword);

            await _context.SaveChangesAsync();
            return ResultDto.Success();
        }
    }
}

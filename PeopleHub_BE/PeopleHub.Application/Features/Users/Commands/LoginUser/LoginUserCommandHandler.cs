using FluentValidation.Results;
using PeopleHub.Application.Features.Users.DTOs;
using PeopleHub.Application.Interfaces;
using PeopleHub.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PeopleHub.Application.Features.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, UserLoginResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public LoginUserCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }
        public async Task<UserLoginResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.Users.FindByUsernameAsync(request.Username, cancellationToken);
            if (existingUser == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            using var hmac = new HMACSHA512(existingUser.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

            if (!CryptographicOperations.FixedTimeEquals(computedHash, existingUser.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            var token = _tokenService.CreateToken(existingUser);

            return new UserLoginResponseDto(existingUser.UserName, token);
        }
    }
}

using FluentValidation.Results;
using PeopleHub.Application.Features.Users.DTOs;
using PeopleHub.Application.Interfaces;
using PeopleHub.Application.Interfaces.Services;
using System.Security.Cryptography;
using System.Text;

using ValidationException = PeopleHub.Application.Common.Exceptions.ValidationException;

namespace PeopleHub.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserRegisterResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public RegisterUserCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }
        public async Task<UserRegisterResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.Users.FindByUsernameAsync(request.Username, cancellationToken);
            if (existingUser != null) 
            {
                var failures = new List<ValidationFailure>
                {
                    new ValidationFailure("Username", "Username already exists")
                };
                throw new ValidationException(failures);
            }

            using var hmac = new HMACSHA512();

            var user = new CreateUserRequestDto
            (
                request.Username,
                hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
                hmac.Key
            );

            var createdUser = await _unitOfWork.Users.CreateUserAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            var token = _tokenService.CreateToken(createdUser);
            return new UserRegisterResponseDto(createdUser.UserName, token);
        }
    }
}

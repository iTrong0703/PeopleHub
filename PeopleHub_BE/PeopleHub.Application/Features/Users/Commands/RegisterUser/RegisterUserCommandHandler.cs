using FluentValidation.Results;
using PeopleHub.Application.Features.Users.Dtos.Responses;
using PeopleHub.Application.Interfaces;
using PeopleHub.Application.Interfaces.Services;
using PeopleHub.Domain.Entities;
using System.Security.Cryptography;
using System.Text;

using ValidationException = PeopleHub.Application.Common.Exceptions.ValidationException;

namespace PeopleHub.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserRegisterResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private const string DefaultProfilePhotoUrl = "https://www.gravatar.com/avatar/?d=mp";

        public RegisterUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<UserRegisterResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var dto = request.RegisterRequest;
            var existingUsername = await _unitOfWork.Users.UsernameExistsAsync(dto.Username, cancellationToken);
            var existingEmail = await _unitOfWork.Users.EmailExistsAsync(dto.Email, cancellationToken);
            if (existingUsername) 
            {
                var failures = new List<ValidationFailure>
                {
                    new ValidationFailure("Username", "Username already exists")
                };
                throw new ValidationException(failures);
            }
            if (existingEmail)
            {
                var failures = new List<ValidationFailure>
                {
                    new ValidationFailure("Email", "Email already exists")
                };
                throw new ValidationException(failures);
            }

            // hash password
            using var hmac = new HMACSHA512();
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));
            var salt = hmac.Key;

            var user = new AppUser
            {
                UserName = dto.Username,
                Email = dto.Email,
                PasswordHash = hash,
                PasswordSalt = salt,
                Profile = new UserProfile
                {
                    FullName = dto.FullName,
                    DateOfBirth = dto.DateOfBirth,
                    Photos = new List<Photo>
                {
                    new Photo
                    {
                        Url = DefaultProfilePhotoUrl,
                        IsMain = true
                    }
                }
                }
            };

            var createdUser = await _unitOfWork.Users.CreateUserAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            return new UserRegisterResponseDto(Message: "User registered successfully");
        }
    }
}

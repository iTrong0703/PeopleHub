using FluentValidation.Results;
using PeopleHub.Application.Features.Users.DTOs;
using PeopleHub.Application.Interfaces;
using System.Security.Cryptography;
using System.Text;

using ValidationException = PeopleHub.Application.Common.Exceptions.ValidationException;

namespace PeopleHub.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<UserResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
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
            return new UserResponseDto(createdUser.Id, createdUser.UserName);
        }
    }
}

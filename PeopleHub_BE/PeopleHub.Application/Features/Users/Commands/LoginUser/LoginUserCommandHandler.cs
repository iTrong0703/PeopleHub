using PeopleHub.Application.Features.Users.Dtos.Responses;
using PeopleHub.Application.Interfaces;
using PeopleHub.Application.Interfaces.Services;
using System.Security.Cryptography;
using System.Text;

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
            var dto = request.LoginRequest;

            var existingUser = await _unitOfWork.Users.FindByUsernameAsync(dto.Username, cancellationToken);
            if (existingUser == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            // hash password
            using var hmac = new HMACSHA512(existingUser.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));

            if (!CryptographicOperations.FixedTimeEquals(computedHash, existingUser.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            var token = _tokenService.CreateToken(existingUser);
            var user = new UserLoginResponseDto
            (
                Username: existingUser.UserName,
                FullName: existingUser.Profile.FullName,
                PhotoUrl: existingUser.Profile.Photos.FirstOrDefault(x => x.IsMain)!.Url,
                Token: token
            );

            return user;
        }
    }
}

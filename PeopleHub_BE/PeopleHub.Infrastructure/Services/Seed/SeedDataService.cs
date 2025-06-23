using Microsoft.Extensions.Configuration;
using PeopleHub.Application.Interfaces.Services;
using PeopleHub.Domain.Entities;
using PeopleHub.Infrastructure.Data;
using PeopleHub.Infrastructure.Services.Seed.DTOs;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace PeopleHub.Infrastructure.Services.Seed
{
    public class SeedDataService : ISeedDataService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public SeedDataService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync();

            if (await _context.Users.AnyAsync()) return;

            string basePath = Path.GetFullPath(_configuration["SeedDataPath"]!);

            string usersFilePath = Path.Combine(basePath, "users.json");

            var usersData = await File.ReadAllTextAsync(usersFilePath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var usersDto = JsonSerializer.Deserialize<List<AppUserSeedDto>>(usersData, options);
            if (usersDto == null) return;

            foreach (var dto in usersDto)
            {
                using var hmac = new HMACSHA512();

                var user = new AppUser
                {
                    UserName = dto.UserName.ToLower(),
                    Email = dto.Email,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("P@ssw0rd")),
                    PasswordSalt = hmac.Key,
                    Profile = new UserProfile
                    {
                        FullName = dto.FullName,
                        Gender = dto.Gender,
                        DateOfBirth = DateOnly.Parse(dto.DateOfBirth),
                        Position = dto.Position,
                        Department = dto.Department,
                        Location = dto.Location,
                        Bio = dto.Bio,
                        LastActive = DateTimeOffset.Parse(dto.LastActive).ToUniversalTime(),
                        Photos = dto.Photos.Select(p => new Photo
                        {
                            Url = p.Url,
                            IsMain = p.IsMain
                        }).ToList()
                    }
                };

                _context.Users.Add(user);
            }
            
            await _context.SaveChangesAsync();
        }
    }
}

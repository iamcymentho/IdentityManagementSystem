using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityManagement.Application.Commands;
using IdentityManagement.Application.Common.Models;
using IdentityManagement.Application.DTOs;
using IdentityManagement.Application.Interfaces;
using IdentityManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityManagement.Application.Handlers.CommandHandler
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Result<UserDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public CreateUserHandler(IApplicationDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }
        public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // check if the user exits 
            var exisitingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.CreateUserDto.Email, cancellationToken);
            if (exisitingUser != null)
            {
                return Result<UserDto>.Failure("User with this email already exists");
            }
            // create new user
            var user = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = request.CreateUserDto.FirstName,
                LastName = request.CreateUserDto.LastName,
                Email = request.CreateUserDto.Email,
                PasswordHash = _passwordHasher.HashPassword(request.CreateUserDto.Password),
                PhoneNumber = request.CreateUserDto.PhoneNumber,
                DateOfBirth = request.CreateUserDto.DateOfBirth,
                CreatedAt = DateTime.UtcNow
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            var userDto = new UserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth,
                IsActive = user.IsActive,
                IsEmailVerified = user.IsEmailVerified,
                CreatedAt = user.CreatedAt,
                LastLoginAt = user.LastLoginAt
            };
            return Result<UserDto>.Success(userDto, "User created successfully");
        }
    }
}

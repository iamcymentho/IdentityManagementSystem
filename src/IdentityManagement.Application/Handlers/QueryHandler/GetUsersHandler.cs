using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityManagement.Application.Common.Models;
using IdentityManagement.Application.DTOs;
using IdentityManagement.Application.Interfaces;
using IdentityManagement.Application.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityManagement.Application.Handlers.QueryHandler
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, Result<List<UserDto>>>
    {
        private readonly IApplicationDbContext _context;

        public GetUsersHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<List<UserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _context.Users
               .Include(u => u.UserRoles)
               .ThenInclude(ur => ur.Role)
               .Skip((request.Page - 1) * request.PageSize)
               .Take(request.PageSize)
               .Select(u => new UserDto
               {
                   Id = u.Id,
                   FirstName = u.FirstName,
                   LastName = u.LastName,
                   Email = u.Email,
                   PhoneNumber = u.PhoneNumber,
                   DateOfBirth = u.DateOfBirth,
                   IsActive = u.IsActive,
                   IsEmailVerified = u.IsEmailVerified,
                   CreatedAt = u.CreatedAt,
                   LastLoginAt = u.LastLoginAt,
                   Roles = u.UserRoles.Select(ur => ur.Role.Name).ToList()
               })
               .ToListAsync(cancellationToken);

            return Result<List<UserDto>>.Success(users);

        }
    }
}

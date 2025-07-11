using IdentityManagement.Application.Commands;
using IdentityManagement.Application.Common.Models;
using IdentityManagement.Application.DTOs;
using IdentityManagement.Application.Interfaces;
using MediatR;

namespace IdentityManagement.Application.Handlers.CommandHandler
{
    public class LoginHandler : IRequestHandler<LoginCommand, Result<AuthResponseDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenetator;
        public Task<Result<AuthResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
           
        }
    }
}

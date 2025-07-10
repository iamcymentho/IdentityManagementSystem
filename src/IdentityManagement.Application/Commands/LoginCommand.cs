using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityManagement.Application.Common.Models;
using IdentityManagement.Application.DTOs;
using MediatR;

namespace IdentityManagement.Application.Commands
{
    public class LoginCommand : IRequest<Result<AuthResponseDto>>
    {
        public LoginDto LoginDto { get; set; } = null!;
    }
}

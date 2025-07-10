using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityManagement.Application.Common.Models;
using IdentityManagement.Application.DTOs;
using MediatR;

namespace IdentityManagement.Application.Queries
{
    public class GetUserByIdQuery : IRequest<Result<UserDto>>
    {
        public Guid UserId { get; set; }
    }
}

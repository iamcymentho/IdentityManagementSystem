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
    public class GetUsersQuery : IRequest<Result<List<UserDto>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

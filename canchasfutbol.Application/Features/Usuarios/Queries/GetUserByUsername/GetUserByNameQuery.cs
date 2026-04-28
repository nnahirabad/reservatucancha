using canchasfutbol.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace canchasfutbol.Application.Features.Usuarios.Queries.GetUserByUsername
{
    public class GetUserByNameQuery : IRequest<string>
    {
        public string Username {  get; set; }

        public GetUserByNameQuery(string username) 
        {
            Username = username; 
        }
    }
}

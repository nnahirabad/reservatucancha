using AutoMapper;
using canchasfutbol.Application.Contracts.Persistence;
using FluentValidation.TestHelper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace canchasfutbol.Application.Features.Usuarios.Queries.GetUserByUsername
{
    public class GetUserByUserQueryHandler : IRequestHandler<GetUserByNameQuery, string>
    {
        private ILogger<GetUserByUserQueryHandler> _logger;
        private IUnitOfWorkRepository _unitOfWorkRepository;
        

        public GetUserByUserQueryHandler(ILogger<GetUserByUserQueryHandler> logger, IUnitOfWorkRepository unitOfWorkRepository)
        {
            _logger = logger;
            _unitOfWorkRepository = unitOfWorkRepository;
           
        }

        public async Task<string> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
        {
            var userByName = await _unitOfWorkRepository.UserRepository.GetByName(request.Username);
            if (userByName == null) {
                _logger.LogError("No existe el usuario"); 
            }
            return userByName!.Username!; 

        }
    }
}

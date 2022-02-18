﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkSchedule.Api.Commands.Users;
using WorkSchedule.Application.Data;
using WorkSchedule.Application.Persistency.Entities;

namespace WorkSchedule.Application.CommandHandlers.Users
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<User> _userManager;

        public DeleteUserCommandHandler(UserManager<User> userManager, IUnitOfWork uow)
        {
            _userManager = userManager;
            _uow = uow;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return Unit.Value;
        }
    }
}
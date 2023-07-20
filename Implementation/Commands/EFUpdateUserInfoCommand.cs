using Application;
using Application.Commands;
using Application.Exceptions;
using DataAcess;
using Domain;
using ProjectNetworkMediaApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands
{
    public class EFUpdateUserInfoCommand : IUpdateUserInfoCommand
    {
        private readonly MyDbContext _context;
        private readonly IApplicationActor _actor;
        public EFUpdateUserInfoCommand(MyDbContext context, IApplicationActor actor) 
        {
            _context = context;
            _actor = actor;
        }

        public int Id => 29;

        public string Name => "Updateing user info using EF";

        public void Execute(UserDto request)
        {

            var user = _context.Users.Find(_actor.Id);

            if (user == null)
            {
                throw new EntityNotFoundException(_actor.Id, typeof(User));
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;   
            user.Email = request.Email;
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            _context.SaveChanges();
        }
    }
}

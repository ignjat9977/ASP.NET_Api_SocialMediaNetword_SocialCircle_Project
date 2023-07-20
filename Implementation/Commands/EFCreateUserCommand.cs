using Application.Commands;
using Application.Email;
using DataAcess;
using Domain;
using FluentValidation;
using ProjectNetworkMediaApi.Core;
using ProjectNetworkMediaApi.Core.Validators;
using ProjectNetworkMediaApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands
{
    public class EFCreateUserCommand : ICreateUserCommand
    {
        private readonly MyDbContext _context;
        private readonly CreateUserValidator _userValidator;
        private readonly IEmailSender _emailSender;

        public EFCreateUserCommand(MyDbContext context, CreateUserValidator userValidator, IEmailSender emailSender)
        {
            _context = context;
            _userValidator = userValidator;
            _emailSender = emailSender;
        }

        public int Id => 8;

        public string Name => "Createing user using Ef";

        public void Execute(UserDto request)
        {
            _userValidator.ValidateAndThrow(request);
            var hash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new User
            {
                Email = request.Email,
                Password = hash,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                RoleId = 1
            };

            
            _context.Users.Add(user);
            _context.SaveChanges();

            //_emailSender.Send(new EmailSendDto
            //{
            //    Content = "<h1>Successffuly registred!</h1>",
            //    Created = DateTime.UtcNow,
            //    SendTo = request.Email,
            //    Subject = "Registracion"
            //});


        }
    }
}

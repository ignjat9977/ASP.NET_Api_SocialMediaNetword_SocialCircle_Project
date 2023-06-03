using Application.Commands;
using Application.Dto;
using DataAcess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands
{
    public class EFCreateFriendCommand : ICreateFriendCommand
    {
        private readonly MyDbContext _context;
        private readonly CreateFriendValidator _validator;

        public EFCreateFriendCommand(MyDbContext context, CreateFriendValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 2;

        public string Name => "Adding friend using EF Command";

        public void Execute(FriendDto request)
        {
            _validator.ValidateAndThrow(request);
            var friend = new Friend
            {
                FriendId = request.FriendId,
                UserId = request.UserId
            };

            _context.Friends.Add(friend);
            _context.SaveChanges();
        }
    }
}

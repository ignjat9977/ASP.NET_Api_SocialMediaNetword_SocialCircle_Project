using Application.Dto;
using DataAcess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators
{
    public class CreateFriendValidator : AbstractValidator<FriendDto>
    {
        private readonly MyDbContext _context;
        public CreateFriendValidator(MyDbContext context)
        {
            _context = context;

            RuleFor(x => x.FriendId)
                .NotEmpty()
                .WithMessage("Friend id cant be empty!")
                .Must(x => _context.Users.Any(y => y.Id == x))
                .WithMessage("User with that friend id must exist in db!")
                .Must(checkIfRelationShipAlreadyExistB)
                .WithMessage("Relation ship already exist!");

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User id cant be empty!")
                .Must(x => _context.Users.Any(y => y.Id == x))
                .WithMessage("User with that user id must exist in db!")
                .Must(checkIfRelationShipAlreadyExistA)
                .WithMessage("Relation ship already exist!");
        }
        private bool checkIfRelationShipAlreadyExistA(FriendDto friend, int id)
            => !_context.Friends.Any(x=> x.FriendId == friend.FriendId && x.UserId == id);

        private bool checkIfRelationShipAlreadyExistB(FriendDto friend, int id)
            => !_context.Friends.Any(x => x.FriendId == friend.UserId && x.FriendId == id);
    }
}

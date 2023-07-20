using Application;
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
    public class CreateReportValidator : AbstractValidator<ReportDto>
    {
        private readonly IApplicationActor _actor;
        private readonly MyDbContext _context;

        public CreateReportValidator(IApplicationActor actor, MyDbContext context)
        {
            _actor = actor;
            _context = context;

            RuleFor(x => x.ReportedUserId).NotEmpty().WithMessage("ReportedUserId cant be empty")
                .Must(x => x != _actor.Id).WithMessage("User cant report himself!")
                .Must(x => _context.Users.Any(y => y.Id == x)).WithMessage("ReportedUser id must exist in DB!");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Description cant be empty!")
                .MinimumLength(10).WithMessage("Description cant be less longer than 10 characters!")
                .MaximumLength(500).WithMessage("Description cant be longer than 500 characters!");

            RuleFor(x => x.PostId).NotEmpty().WithMessage("PostId cant be empty!")
                .Must(x => _context.Posts.Any(y => y.Id == x)).WithMessage("Post must exist!");
        }
    }
}

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
    public class UpdateGroupValidator : AbstractValidator<GroupInsertDto>
    {
        public UpdateGroupValidator(MyDbContext context)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Name is required field!")
                .MinimumLength(3)
                .WithMessage("Name must have at least 3 characters!")
                .MaximumLength(50)
                .WithMessage("Name must have less or equal 50 characters!")
                .Must(x => !context.Groups.Any(y => y.Name == x))
                .WithMessage("Name already exists in database!");

            RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Description is required field!")
                .MinimumLength(10)
                .WithMessage("Description must have at least 10 characters!")
                .MaximumLength(1000)
                .WithMessage("Description must have less or equal 1000 characters!");
        }
    }
}

using Application.Dto;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators
{
    public class CreatePostOrVideoInGroupValidator : AbstractValidator<GroupPostDto>
    {
        public CreatePostOrVideoInGroupValidator()
        {
            RuleFor(x => x.Title)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .WithMessage("Name is required field!")
               .MinimumLength(3)
               .WithMessage("Name must have at least 3 characters!")
               .MaximumLength(50)
               .WithMessage("Name must have less or equal 50 characters!");

            RuleFor(x => x.Content)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Content is required field!")
                .MinimumLength(10)
                .WithMessage("Name must have at least 10 characters!")
                .MaximumLength(1000)
                .WithMessage("Name must have less or equal 1000 characters!");
        }
    }
}

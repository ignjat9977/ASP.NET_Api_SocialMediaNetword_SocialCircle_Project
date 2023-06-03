using Application.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators
{
    public class CreateCommentValidator : AbstractValidator<CommentInsertDto>
    {
        public CreateCommentValidator()
        {
            RuleFor(x => x.Content).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Comment cant be empty!")
                .MinimumLength(3).WithMessage("Comment must be at least 3 character long!")
                .MaximumLength(500).WithMessage("Comment can be long 500 characters!");
        }
    }
}

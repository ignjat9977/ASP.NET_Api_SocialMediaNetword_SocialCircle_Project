using DataAcess;
using FluentValidation;
using ProjectNetworkMediaApi.Dto;

namespace ProjectNetworkMediaApi.Core.Validators
{
    public class UpdatePostValidator : AbstractValidator<PostDto>
    {

        public UpdatePostValidator(MyDbContext context)
        {
            RuleFor(x => x.Title)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Title is required field!")
                .MinimumLength(3)
                .WithMessage("Title must have at least 3 characters!")
                .MaximumLength(50)
                .WithMessage("Title must have less or equal 50 characters!")
                .Must(e=> !context.Posts.Any(x=> x.Title == e))
                .WithMessage("Title cant be the same!");

            RuleFor(x => x.Content)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Content is required field!")
                .MinimumLength(10)
                .WithMessage("Content must have at least 10 characters!")
                .MaximumLength(1000)
                .WithMessage("Content must have less or equal 1000 characters!")
                .Must(e => !context.Posts.Any(x => x.Content == e))
                .WithMessage("Content cant be the same!"); 
        }
    }
}

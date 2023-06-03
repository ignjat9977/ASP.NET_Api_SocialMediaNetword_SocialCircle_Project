using FluentValidation;
using ProjectNetworkMediaApi.Dto;

namespace ProjectNetworkMediaApi.Core.Validators
{
    public class CreatePostValidator : AbstractValidator<PostDto>
    {

        public CreatePostValidator()
        {
            RuleFor(x => x.Title)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Name is required field!")
                .MinimumLength(3)
                .WithMessage("Name must have at least 3 characters!")
                .MaximumLength(50)
                .WithMessage("Name must have less or equal 50 characters!");

            RuleFor(x=>x.Content)
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

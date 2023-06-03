using DataAcess;
using FluentValidation;
using ProjectNetworkMediaApi.Dto;

namespace ProjectNetworkMediaApi.Core.Validators
{
    public class CreateUserValidator : AbstractValidator<UserDto>
    {

        private readonly MyDbContext _dbContext;
        public CreateUserValidator(MyDbContext context)
        {

            _dbContext = context;

            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("First Name field is required!")
                .MinimumLength(3)
                .WithMessage("First Name must have at least 3 character")
                .MaximumLength(20)
                .WithMessage("First Name can have maximum of 20 characters");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Last Name field is required!")
                .MinimumLength(3)
                .WithMessage("Last Name must have at least 3 character")
                .MaximumLength(20)
                .WithMessage("Last Name can have maximum of 20 characters");


            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Email field is required!")
                .EmailAddress()
                .WithMessage("Email is not in right format!")
                .MaximumLength(40)
                .WithMessage("Email can have maximum of 40 characters")
                .Must(checkIfUserEmailExists)
                .WithMessage(p => $"That Email of {p.Email} already exists in database!");

            var regexPassword = @"^[A-Za-z0-9!@#$%%&*()+_(]{8,20}$";

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Password field is required!")
                .Matches(regexPassword)
                .WithMessage("Password is not in right format");

            RuleFor(x => x.DateOfBirth)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Date of Birth field is required!");
        }

        public bool checkIfUserEmailExists(string email)
        {
            return !_dbContext.Users.Any(y => y.Email == email);
        }
    }
}

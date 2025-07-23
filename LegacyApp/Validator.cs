using FluentValidation;
using LegacyApp.Common;
using LegacyApp.Model;
using System;

namespace LegacyApp
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator() 
        {
            RuleFor(User => User.Firstname)
                .NotEmpty()
                .WithMessage(ValidationMessage.FirstNameRequired);
            
            RuleFor(User => User.Surname)
                .NotEmpty()
                .WithMessage(ValidationMessage.SurnameRequired);

            RuleFor(user => user.EmailAddress)
                .NotEmpty()
                .Must(email => email.Contains("@") && email.Contains("."))
                .WithMessage(ValidationMessage.EmailInvalid);

            RuleFor(user => user.DateOfBirth)
              .Cascade(CascadeMode.Stop)
              .NotEmpty()
              .Must((user, dob) =>
              {
                  var now = DateTime.Now;
                  int age = now.Year - dob.Year;
                  if (now.Month < dob.Month || (now.Month == dob.Month && now.Day < dob.Day)) 
                      age--;

                  return age >= 21;
              })
            .WithMessage(ValidationMessage.UnderAge);

        }
    }

}

using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class AuthValidator : AbstractValidator<UserLoginDto>
    {
        public AuthValidator()
        {
            RuleFor(p => p.Email).NotEmpty();
            RuleFor(p => p.Email).Length(10, 30);
            RuleFor(p => p.Password).NotEmpty();
            RuleFor(p => p.Password).Length(3, 30);
        }
    }
}

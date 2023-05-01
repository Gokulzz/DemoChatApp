using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.BLL.DTO;
using FluentValidation;

namespace app.BLL.Validation
{
    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8)
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase characters")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase characrers")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number")
                .Matches(@"[\!\@\#\$]+").WithMessage("Your password must contain at least one specialcharacter" +
                "(!, @, #, $)");
            RuleFor(x => x.ConfirmPassword).NotEmpty().Matches(x => x.Password).WithMessage("Password and Confirmed" +
                "Password should match");
        }
    }
}

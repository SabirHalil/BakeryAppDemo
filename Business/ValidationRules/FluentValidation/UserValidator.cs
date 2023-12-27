using Core.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        /*
        public UserValidator()
        {
            //RuleFor(s => s.UserName).NotEmpty();
            //RuleFor(s => s.UserName).MinimumLength(2);

           //RuleFor(s => s.Address).Must(StartWithKayseri); 

        }
        */
        /*
        private bool StartWithKayseri(string arg)
        {
            return arg.StartsWith("Kayseri");
        }
        */
       
    }
}

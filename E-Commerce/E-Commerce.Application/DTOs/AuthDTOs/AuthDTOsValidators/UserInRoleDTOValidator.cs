using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.DTOs.AuthDTOs.AuthDTOsValidators
{
    public class UserInRoleDTOValidator:AbstractValidator<UserInRoleDTO>
    {
        public UserInRoleDTOValidator()
        {

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required");
            RuleFor(x => x.IsSelected)
                .NotEmpty().WithMessage("Selection is required");
        }
    }
}

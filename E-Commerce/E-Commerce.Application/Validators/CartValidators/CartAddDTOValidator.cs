using E_Commerce.Application.DTOs.CartDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Validators.CartValidators
{
    public class CartAddDTOValidator:AbstractValidator<CartAddDTO>
    {
        public CartAddDTOValidator()
        {
            RuleFor(dto => dto.UserId)
                   .NotEmpty().WithMessage("User Id is required.");
            RuleFor(dto => dto.TotalPrice)
                .GreaterThan(0).WithMessage("Total Price must be greater than zero.");
        }
    }
}

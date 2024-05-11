using E_Commerce.Application.DTOs.CartItemDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Validators.CartItemValidators
{
    public class CartItemAddDTOValidator: AbstractValidator<CartItemAddDTO>
    {
        public CartItemAddDTOValidator()
        {
            RuleFor(dto => dto.ProductId)
                .NotEmpty().WithMessage("Product Id is required.");
            RuleFor(dto => dto.CartId)
                .NotEmpty().WithMessage("Product Id is required.");
            RuleFor(dto => dto.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");
            RuleFor(dto => dto.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        }
    }
}

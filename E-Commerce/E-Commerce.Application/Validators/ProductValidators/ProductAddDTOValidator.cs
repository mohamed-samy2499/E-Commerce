using E_Commerce.Application.DTOs.ProductDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Validators.ProductValidators
{
    public class ProductAddDTOValidator:AbstractValidator<ProductAddDTO>
    {
        public ProductAddDTOValidator()
        {
            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage("Product Name is required.")
                .MaximumLength(100).WithMessage("Product Name cannot be longer than 100 characters.");
            RuleFor(dto => dto.Description)
               .MaximumLength(1000).WithMessage("Product Description cannot be longer than 1000 characters.");
            RuleFor(dto => dto.Price)
                .NotEmpty().WithMessage("Price is required.")
                .GreaterThan(0).WithMessage("Price must be greater than zero.");
        }
    }
}

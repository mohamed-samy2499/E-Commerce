using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Helpers
{
    public static class FilterExpression
    {
        public static Expression<Func<TEntity, bool>> BuildFilterExpression<TEntity>(string expression)
        {
            // Define common comparison operators
            string[] operators = { "==", "!=", "<", ">", "<=", ">=" };

            // Try to find the operator in the expression
            string operatorString = operators.FirstOrDefault(op => expression.Contains(op));

            if (operatorString == null)
            {
                throw new ArgumentException("Invalid filter expression format");
            }

            // Split the expression based on the operator
            var parts = expression.Split(new[] { operatorString }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
            {
                throw new ArgumentException("Invalid filter expression format");
            }

            var propertyName = parts[0].Trim();
            var value = parts[1].Trim();

            // Get the property type
            var propertyType = typeof(TEntity).GetProperty(propertyName)?.PropertyType;

            if (propertyType == null)
            {
                throw new ArgumentException($"Property '{propertyName}' not found on type {typeof(TEntity).Name}");
            }

            // Create parameter expression
            var parameter = Expression.Parameter(typeof(TEntity), "entity");

            // Create property access expression
            var property = Expression.Property(parameter, propertyName);

            // Create constant expression for the value
            var valueExpression = Expression.Constant(Convert.ChangeType(value, propertyType));

            // Create the binary expression based on the operator
            BinaryExpression binaryExpression;
            switch (operatorString)
            {
                case "==":
                    binaryExpression = Expression.Equal(property, valueExpression);
                    break;
                case "!=":
                    binaryExpression = Expression.NotEqual(property, valueExpression);
                    break;
                // Add more cases for other operators as needed
                default:
                    throw new ArgumentException($"Invalid operator: {operatorString}");
            }

            // Create the final lambda expression
            var lambda = Expression.Lambda<Func<TEntity, bool>>(binaryExpression, parameter);

            return lambda;
        }
    }
}

using Application.Dto;
using Domain;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using ProjectNetworkMediaApi.Dto;

namespace ProjectNetworkMediaApi.Core.Extensions
{
    public static class ApiExtensions
    {

        public static IActionResult AsClientErrors(this ValidationResult result)
        {
            var errorMessages = result.Errors.Select(e => new ClientErrorDto
            {
                ErrorMessage = e.ErrorMessage,
                PropertyName = e.PropertyName
            });

            return new UnprocessableEntityObjectResult(new
            {
                Errors = errorMessages
            });

        }

    }
}

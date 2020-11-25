using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.Catalog.Buses
{
    public class CreateBusRequestValidator : AbstractValidator<CreateBusRequestDto>
    {
        public CreateBusRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.LicenseCode).NotEmpty().WithMessage("LicenseCode is required");
        }
    }
}

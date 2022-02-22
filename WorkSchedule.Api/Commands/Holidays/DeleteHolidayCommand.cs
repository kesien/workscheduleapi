using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkSchedule.Api.Commands.Holidays
{
    public class DeleteHolidayCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }

    public class DeleteHolidayCommanddValidator : AbstractValidator<DeleteHolidayCommand>
    {
        public DeleteHolidayCommanddValidator()
        {
            RuleFor(c => c.Id)
                .NotNull()
                .NotEmpty();
        }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkSchedule.Api.Commands.Holidays;
using WorkSchedule.Application.Data;

namespace WorkSchedule.Application.CommandHandlers.Holidays
{
    public class DeleteHolidayCommandHandler : IRequestHandler<DeleteHolidayCommand, Unit>
    {
        private readonly IUnitOfWork _uow;

        public DeleteHolidayCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(DeleteHolidayCommand request, CancellationToken cancellationToken)
        {
            var holiday = _uow.HolidayRepository.GetByID(request.Id);
            _uow.HolidayRepository.Delete(holiday);
            _uow.Save();
            return Unit.Value;
        }
    }
}

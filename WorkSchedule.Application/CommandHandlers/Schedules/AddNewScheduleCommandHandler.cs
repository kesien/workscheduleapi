﻿using AutoMapper;
using MediatR;
using WorkSchedule.Api.Commands.Schedules;
using WorkSchedule.Api.Dtos;
using WorkSchedule.Application.Exceptions;
using WorkSchedule.Application.Services.EmailService;
using WorkSchedule.Application.Services.ScheduleService;

namespace WorkSchedule.Application.CommandHandlers.Schedules
{
    public class AddNewScheduleCommandHandler : IRequestHandler<AddNewScheduleCommand, ScheduleDto>
    {
        private readonly IScheduleService _scheduleService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public AddNewScheduleCommandHandler(IScheduleService scheduleService, IEmailService emailService, IMapper mapper)
        {
            _scheduleService = scheduleService;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<ScheduleDto> Handle(AddNewScheduleCommand request, CancellationToken cancellationToken)
        {
            var validator = new AddNewScheduleCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                throw new BusinessException { ErrorCode = 599, ErrorMessages = validatorResult.Errors.Select(e => e.ErrorMessage).ToList() };
            }
            var schedule = await _scheduleService.CheckSchedule(request.Year, request.Month);
            if (schedule)
            {
                throw new BusinessException { ErrorCode = 599, ErrorMessages = new List<string> { $"There is a schedule on date: { request.Year }-{ request.Month}" } };
            }
            var newSchedule = await _scheduleService.CreateSchedule(request.Year, request.Month);
            if (newSchedule is null)
            {
                throw new BusinessException { ErrorCode = 599, ErrorMessages = new List<string> { $"Couldn't create schedule on date: {request.Year}-{request.Month}" } };
            }
            var scheduleDto = _mapper.Map<ScheduleDto>(newSchedule);
            scheduleDto.Days = scheduleDto.Days.OrderBy(day => day.Date).ToList();
            await _emailService.SendNewScheduleEmail(request.UserId, request.Year, request.Month);
            return scheduleDto;
        }
    }
}

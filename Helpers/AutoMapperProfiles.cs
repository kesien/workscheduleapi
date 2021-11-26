using AutoMapper;
using AutoMapper.EquivalencyExpression;
using WorkScheduleMaker.Dtos;
using WorkScheduleMaker.Entities;

namespace WorkScheduleMaker.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserForRegisterDto, User>().ReverseMap();
            //CreateMap<User, UserForDetailDto>()
            //    .ForMember(dest => dest.PhotoUrl, opt =>
            //        opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
            //    .ForMember(dest => dest.Age, opt =>
            //        opt.MapFrom(src => src.DateOfBirth.CalculateAge())); ;
            CreateMap<UserForUpdateDto, User>();
            CreateMap<User, UsersWithRequestsDto>();
            CreateMap<MorningSchedule, UserToRequestDto>()
                .ForMember(dest => dest.UserId, opt =>
                    opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.Name, opt => 
                    opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.UserName, opt =>
                    opt.MapFrom(src => src.User.UserName)).ReverseMap();
            CreateMap<Forenoonschedule, UserToRequestDto>()
                .ForMember(dest => dest.UserId, opt =>
                    opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.Name, opt => 
                    opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.UserName, opt =>
                    opt.MapFrom(src => src.User.UserName)).ReverseMap();
            CreateMap<HolidaySchedule, UserToRequestDto>()
                .ForMember(dest => dest.UserId, opt =>
                    opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.Name, opt => 
                    opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.UserName, opt =>
                    opt.MapFrom(src => src.User.UserName)).ReverseMap();
            CreateMap<Day, DayDto>().ReverseMap();
            CreateMap<UserToRequestDto, User>()
                .EqualityComparison((dto, user) => dto.UserId == user.Id);
            CreateMap<User, UserToRequestDto>()
                .EqualityComparison((user, dto) => user.Id == dto.UserId)
                .ForMember(dest => dest.UserId, opt =>
                    opt.MapFrom(src => src.Id));
            CreateMap<UserToListDto, User>()
                .EqualityComparison((dto, user) => dto.UserId == user.Id);
            CreateMap<User, UserToListDto>()
                .EqualityComparison((user, dto) => user.Id == dto.UserId)
                .ForMember(dest => dest.UserId, opt =>
                    opt.MapFrom(src => src.Id));
            CreateMap<MonthlySchedule, ScheduleDto>().ReverseMap();
            CreateMap<RequestDto, Request>().ReverseMap();
            CreateMap<Request, RequestWithUserDto>()
                .ForMember(dest => dest.Username, opt => 
                    opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.UserId, opt => 
                    opt.MapFrom(dest => dest.UserId));
            CreateMap<Summary, SummaryDto>().ReverseMap();
        }
    }
}

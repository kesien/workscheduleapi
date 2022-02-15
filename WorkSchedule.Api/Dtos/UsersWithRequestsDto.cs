namespace WorkSchedule.Api.Dtos
{
    public class UsersWithRequestsDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public List<RequestDto> Requests { get; set; }
    }
}

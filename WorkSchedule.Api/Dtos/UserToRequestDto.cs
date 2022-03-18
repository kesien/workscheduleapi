namespace WorkSchedule.Api.Dtos
{
    public class UserToRequestDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public bool IsRequest { get; set; }
    }
}
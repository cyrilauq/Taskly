namespace TodoList.Application.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Pseudo { get; set; }
        public string Token { get; set; }
        public DateOnly BirthDate { get; set; }
    }
}

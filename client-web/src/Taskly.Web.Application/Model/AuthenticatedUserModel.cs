namespace Taskly.Web.Application.Model
{
    public class AuthenticatedUserModel
    {
        public string Pseudo { get; set; }
        public Guid Id { get; set; }
        public string Token { get; set; }
    }
}

namespace BETSoftware.Domain.Models.Dtos
{
    public class LoginOutDto
    {
        public string Token { get; set; }
        public DateTime? Expires { get; set; }
        public int Id { get; set; }
    }
}

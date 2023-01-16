namespace BETSoftware.Domain.Models.Dtos
{
    public class LoginOutDto
    {
        public dynamic Token { get; set; }
        public DateTime? Expires { get; set; }
        public int Id { get; set; }
    }
}

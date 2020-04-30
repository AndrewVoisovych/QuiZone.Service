namespace QuiZone.DataAccess.Models.DTO
{
    public sealed class UserDTO
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Verification { get; set; }

        
    }
}

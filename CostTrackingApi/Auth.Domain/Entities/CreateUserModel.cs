namespace Auth.Domain.Entities
{
    public class CreateUserModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Role> MultipleRoles { get; set; }
        //public string RoleId { get; set; }

        //public string accessToken { get; set; }
    }
}

namespace Auth.Domain.Entities
{
    public class CreateUserModel
    {
        public string userId { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        //public string RoleId { get; set; }

        //public string accessToken { get; set; }
    }
}

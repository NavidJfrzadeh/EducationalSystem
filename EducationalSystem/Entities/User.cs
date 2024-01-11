using EducationalSystem.Emuns;

namespace EducationalSystem.Entities
{
    public abstract class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleEnum Role { get; set; }


        //public bool SetPassword(string password, out string message)
        //{
        //    if (password != null)
        //    {
        //        Password = password;
        //        message = "Password has been set Successfully";
        //        return true;
        //    }

        //    message = "error while setting a Password";
        //    return false;
        //}
    }
}

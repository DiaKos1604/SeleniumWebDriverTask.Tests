namespace SeleniumWebDriverTask.Business.Models
{
    public class UserBuilder
    {
        public UserModel _user = new UserModel();

        public UserBuilder WithName(string name)
        {
            _user.Name = name;
            return this;
        }

        public UserBuilder WithUsername(string username)
        {
            _user.Username = username;
            return this;
        }

        public UserModel Build()
        {
            return _user;
        }
    }
}
namespace Domain.Entities;

public class User : BaseEntity
{
    public User() { }

    public User(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }

    public string UserName { get; set; }
    public string Password { get; set; }
}

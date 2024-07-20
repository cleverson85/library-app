namespace Domain.Entities;

public class User : BaseEntity
{
    public User() { }

    public User(string id) 
    { 
        Id = id;
    }

    public User(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }

    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

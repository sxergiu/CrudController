using System.Text.RegularExpressions;
using FirstCRUDController.Entities.Rooms;

namespace FirstCRUDController.Entities.Maids;

public class Maid : Entity
{
    public string Name { get; set; }
    public List<Room> Rooms { get; set; } = new();
    public string Email { get; set; }

    private Maid(){}

    public static async Task<Maid> CreateMaidAsync(
            IMaidRepository repo,
            List<Room> rooms,
            string name,
            string email
        )
    {
        if (!await repo.IsEmailUnique(email))
            throw new Exception("Email must be unique!"); 
        if (!IsEmailValid(email))
            throw new Exception("Invalid email!");
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception("Name can't be empty!");
        return new Maid
        {
            Name = name,
            Rooms = rooms,
            Email = email
        };
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception("Name can't be empty");
        name = name.Replace(" ", "");
        Name = name;
    }

    public void SetEmail(string email)
    {
        if (!IsEmailValid(email))
            throw new Exception("Invalid email!");
        Email = email;
    }

    private static bool IsEmailValid(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;
        try
        {
            var pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"; 
            var regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }
    

}
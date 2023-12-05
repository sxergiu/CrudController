using System.Text.RegularExpressions;
using FirstCRUDController.Entities.Rooms;
using FirstCRUDController.Services;

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
        if (!EmailValidation.IsValid(email))
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
        if (!EmailValidation.IsValid(email))
            throw new Exception("Invalid email!");
        Email = email;
    }

}
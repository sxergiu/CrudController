using FirstCRUDController.Entities.Maids;

namespace FirstCRUDController.Entities.Rooms;

public class Room : Entity
{
    public bool isOccupied { get; private set; }
    public string Name { get; private set; }
    public string Review { get; private set; }
    
    public Maid Maid {get; set; }

    private Room()
    {
    }

    public static async Task<Room> CreateAsync(
        IRoomRepo repo,
        Maid maid,
        string name,
        bool isoccupied,
        string review)
    {

        if (!await repo.IsReviewUnique(review) || string.IsNullOrWhiteSpace(review))
        {
            throw new Exception("Already existing/Inexisting review!");
        }
        
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception("Name can't be empty!");

        return new Room
        {
            Name = name,
            isOccupied = isoccupied,
            Maid = maid,
            Review = review
        };
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception("Name can't be empty!");
        Name = name;
    }

    public void SetIsOccupied(bool isoccupied)
    {
        isOccupied = isoccupied;
    }

    public void SetReview(string review)
    {
        if (string.IsNullOrWhiteSpace(review))
            throw new Exception("Review can't be empty!");
        
        Review = review;
    }
}
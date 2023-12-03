using FirstCRUDController.Entities;

namespace FirstCRUDController.Repository;

public class RoomRepository : IRoomRepository
{
    private readonly AppDbContext _Context;

    public RoomRepository(AppDbContext context)
    {
        _Context = context;
    }

    public async Task<bool> isReviewUnique(string review)
    {
    //    return !await _Context.Rooms.W
    }
}
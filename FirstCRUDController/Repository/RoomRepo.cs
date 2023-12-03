using FirstCRUDController.Entities.Maids;
using FirstCRUDController.Entities.Rooms;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace FirstCRUDController.Repository;

public class RoomRepo : IRoomRepo
{
    private readonly AppDbContext _context;

    public RoomRepo(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsReviewUnique(string review)
    {
        return !await _context.Rooms.AnyAsync(r => r.Review == review );
    }

    public async Task<IEnumerable<Room>> GetAllRoomsAsync()
    {
        return await _context.Set<Room>().ToListAsync();
    }

    public async Task<Room> GetRoomById(string Id)
    {
        var room = await _context.Rooms.Where(r => r.Id == Id)
            .OrderBy(r =>r.Name).FirstOrDefaultAsync();
        
        return room;
    }

    public async Task<Room> CreateRoom(Room room)
    { 
        _context.Rooms.Add(room);
        return room;
    }

    public async Task<bool> DeleteRoom(string Id)
    {
        var room = await _context.Rooms.Where((r => r.Id == Id)).FirstOrDefaultAsync();

        if (room is null)
            return false;

        _context.Rooms.Remove(room);

        return true;
    }

    public async void SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
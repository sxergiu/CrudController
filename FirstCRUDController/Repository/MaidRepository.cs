using FirstCRUDController.Entities.Maids;
using FirstCRUDController.Entities.Rooms;
using Microsoft.EntityFrameworkCore;

namespace FirstCRUDController.Repository;

public class MaidRepository : IMaidRepository
{
    private readonly AppDbContext _context;

    public MaidRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsEmailUnique(string email)
    {
        return !await _context.Maids.AnyAsync(m => m.Email == email);
    }

    public async Task<IEnumerable<Maid>> GetAllMaidsAsync()
    {
        return await _context.Set<Maid>().ToListAsync();
    }

    public async Task<Maid> CreateMaid(Maid maid)
    {
        _context.Maids.Add(maid);
        return maid;
    }

    public async Task<bool> DeleteMaid(string id)
    {
        var m = await _context.Maids.Where(maid => maid.Id == id).FirstOrDefaultAsync();

        if (m is null)
            return false;
        _context.Maids.Remove(m);
        return true;
    }

    public async Task<Maid> GetMaidById(string id)
    {
        var m = await _context.Maids.Where(maid => maid.Id == id).
            OrderBy(maid => maid.Name).FirstOrDefaultAsync();

        return m;
    }

    public async Task<IEnumerable<Maid>> GetMaidsIncludingRooms()
    {
        return await _context.Maids.Include(m => m.Rooms).ToListAsync();
    }

    public async Task<IEnumerable<Room>> GetMaidRooms(string id)
    {
        List<Room> wantedRooms = new List<Room>();

        foreach (var room in await _context.Set<Room>().ToListAsync() )
        {
            if (room.Maid.Id is not null && room.Maid.Id == id)
                wantedRooms.Add(room);
        }
        return wantedRooms;
    }
    public async void SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
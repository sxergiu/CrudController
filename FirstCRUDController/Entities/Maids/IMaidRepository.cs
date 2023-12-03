using FirstCRUDController.Entities.Rooms;

namespace FirstCRUDController.Entities.Maids;

public interface IMaidRepository
{
        Task<Maid> GetMaidById(string id);
        Task<bool> IsEmailUnique(string email);
        Task<bool> DeleteMaid(string id);
        Task<IEnumerable<Maid>> GetAllMaidsAsync();
        Task<Maid> CreateMaid(Maid m);
        Task<IEnumerable<Maid>> GetMaidsIncludingRooms();
        Task<IEnumerable<Room>> GetMaidRooms(string id);
        void SaveAsync();
}
namespace FirstCRUDController.Entities.Rooms;

public class IRoomRepository
{
    Task<bool> isReviewUnique(string Review);
}
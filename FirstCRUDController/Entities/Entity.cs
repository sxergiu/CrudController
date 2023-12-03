namespace FirstCRUDController.Entities;

public class Entity : IEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /*
    public Entity()
    {
        Id = Guid.NewGuid().ToString();
    }
    */
}
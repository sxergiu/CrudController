using FirstCRUDController.Entities.Maids;
using FirstCRUDController.Entities.Rooms;
using FirstCRUDController.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstCRUDController.Controllers.Rooms;

[Route("rooms")]
[ApiController]

public class RoomsController : ControllerBase
{
    private readonly IRoomRepo _repo;
    private readonly IMaidRepository _mrepo;

    public RoomsController(AppDbContext dbContext, IRoomRepo repo, IMaidRepository maidRepository)
    {
        _repo = repo;
        _mrepo = maidRepository;
    }
    
    [HttpGet(Name = "GetAllRooms")]
    public async Task<ActionResult> GetRooms()
    {
        //var roomz = await _dbContext.Set<Room>().ToListAsync();
        var roomz = await _repo.GetAllRoomsAsync();
        return Ok(roomz);
    }

    [HttpGet("{Id}")]

    public async Task<ActionResult> GetRoom(string id)
    {
        var room = await _repo.GetRoomById(id); // async methods = parallel computing to avoid UI freeze

        if (room is null)
            return NotFound($"Room with Id: {id} does not exist");

        return Ok(room);
    }

    [HttpPost]
    public async Task<ActionResult> CreateRoom([FromBody] RoomRequest roomRequest)
    {
        
        var maid =  await _mrepo.GetMaidById(roomRequest.MaidId);
        if (maid is null)
            return NotFound($"maid with id: {roomRequest.MaidId} not found!");

        Room r = null;
        
        try
        {
            r =  await Room.CreateAsync(
                _repo,
                maid,
                roomRequest.Name,
                roomRequest.IsOccupied,
                roomRequest.Review);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        await _repo.CreateRoom(r);
        _repo.SaveAsync();
        
        return Ok(new RoomResponse
        {
            Id = r.Id,
            Name = r.Name,
            isOccupied = r.isOccupied,
            Review = r.Review,
            MaidId = maid.Id
        });
    }
    
    
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteRoom(string Id)
    {
        
        if ( !await _repo.DeleteRoom(Id) )
            return NotFound($"Room with Id {Id} not found");

        _repo.SaveAsync();
        return Ok($"Room with Id {Id} was removed");
    }

    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateRoomAvailability(string Id,[FromBody] bool isoccupied)
    {
        var r = await _repo.GetRoomById(Id);
        
        if (r is null)
            return NotFound($"Room with Id {Id} not found!");
        
        r.SetIsOccupied(isoccupied);
        _repo.SaveAsync();
        
        return Ok(r);
    }

    [HttpPut("{Id}")]

    public async Task<ActionResult> UpdateRoom(string Id, [FromBody] RoomRequest roomRequest)
    {
        var r = await _repo.GetRoomById(Id);
        
        if( r is null )
            return NotFound($"Room with Id {Id} not found!");

        try
        {
            r.SetName(roomRequest.Name);
            r.SetReview(roomRequest.Review);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
        r.SetIsOccupied(roomRequest.IsOccupied);
        
        _repo.SaveAsync();
        
        return Ok(r);
    }
    
    
 }
using FirstCRUDController.Controllers.Rooms;
using FirstCRUDController.Entities.Maids;
using FirstCRUDController.Entities.Rooms;
using FirstCRUDController.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FirstCRUDController.Controllers.Maids;

[Route("maids")]
[ApiController]
public class MaidsController : ControllerBase
{
    private readonly IMaidRepository _repo;
    private readonly IRoomRepo _roomrepo;

    public MaidsController(IMaidRepository repo,IRoomRepo roomrepo)
    {
        _roomrepo = roomrepo;
        _repo = repo;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MaidsResponse>>> GetMaids()
    {
        var maids = await _repo.GetMaidsIncludingRooms();

        return Ok(maids.Select(m => new MaidsResponse
        {
            Id = m.Id,
            Name = m.Name,
            Email = m.Email,
            Rooms = m.Rooms.Select(r => new RoomResponse
            {
                Id = r.Id,
                Name = r.Name,
                isOccupied = r.isOccupied,
                Review = r.Review,
                MaidId = m.Id
            }).ToList()
        }));
    }

    [HttpPost]
    public async Task<ActionResult<Maid>> CreateMaid([FromBody] MaidRequest request)
    {
        Maid maid = null;
        List<Room> rooms = new List<Room>();
        try
        {
            maid = await Maid.CreateMaidAsync(
                _repo,
                rooms,
                request.Name,
                request.Email
            );
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        await _repo.CreateMaid(maid);
        _repo.SaveAsync();

        return Ok(maid);
    }
    
    [HttpGet("{Id}")]
    public async Task<ActionResult> GetMaid(string Id)
    {
        var maid = await _repo.GetMaidById(Id);

        if (maid is null)
            return NotFound($"Maid with id: {Id} not found!");

        return Ok(maid);
    }

    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteMaid(string Id)
    {
        var maid = _repo.GetMaidById(Id);

        if (maid is null)
            return NotFound($"Maid with id: {Id} not found!");
        
        await _repo.DeleteMaid(Id);
        _repo.SaveAsync();
        
        return Ok($"Maid with Id: {Id} was removed!");
    }

    [HttpPatch("{Id}")]
    public async Task<ActionResult> AddMaidRoom(string Id, [FromBody] string RoomId)
    {
        var room = await _roomrepo.GetRoomById(RoomId);
        var maid = await _repo.GetMaidById(Id);
        
        if (room is null || maid is null)
            return NotFound($"Room/Maid with ids: {RoomId}/{Id} not found!");
       
        maid.Rooms.Add(room);
        _repo.SaveAsync();
        
        return Ok(maid);
    }

    [HttpPut("{Id}")]
    public async Task<ActionResult> UpdateMaid(string Id, [FromBody] MaidRequest maidRequest)
    {
        var maid = await _repo.GetMaidById(Id);

        if (maid is null)
            return NotFound($"Maid with Id: {Id} was not found!");

        try
        {
            maid.SetName(maidRequest.Name);
            maid.SetEmail(maidRequest.Email);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
        _repo.SaveAsync();
        return Ok(maid);
    }
}
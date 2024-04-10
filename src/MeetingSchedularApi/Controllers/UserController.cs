using MeetingSchedularApi;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserRepository _userRepository;

    public UsersController()
    {
        _userRepository = new UserRepository();
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> Get()
    {
        return Ok(_userRepository.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<User> Get(Guid id)
    {
        var user = _userRepository.GetById(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public IActionResult Post([FromBody] User user)
    {
        //User user = new User();
        //user.Id = model.Id;

        TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(user.timeZone);        
        _userRepository.Add(user);
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody] User user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        _userRepository.Update(user);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        _userRepository.Delete(id);
        return NoContent();
    }
}
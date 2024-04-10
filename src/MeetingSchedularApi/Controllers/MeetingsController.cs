using MeetingSchedularApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class MeetingsController : ControllerBase
{
    private readonly MeetingRepository _meetingRepository;

    private readonly UserRepository _userRepository;

    public MeetingsController(MeetingRepository meetingRepository, UserRepository userRepository)
    {
        _meetingRepository = meetingRepository;
        _userRepository = userRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Meeting>> Get()
    {
        var Meetings = _meetingRepository.GetAll();
        return Ok(Meetings);
    }

    [HttpGet("{id}")]
    public ActionResult<Meeting> GetById(Guid id)
    {
        var @Meeting = _meetingRepository.GetById(id);
        if (@Meeting == null)
        {
            return NotFound();
        }
        return Ok(@Meeting);
    }

    [HttpPost]
    public ActionResult<Meeting> Create([FromBody] MeetingModel @Meeting)
    {
        List<User> userList = new List<User>();
        User user = _userRepository.GetById(@Meeting.UserId);
        userList.Add(user);
        Meeting meeting = new Meeting();
        meeting.Id = @Meeting.Id;
        meeting.Name = @Meeting.Name;
        meeting.Start = DateTime.Parse(@Meeting.Start);
        meeting.End = DateTime.Parse(@Meeting.End);
        meeting.Attendees = userList.ToList();
        _meetingRepository.Add(meeting);
        return CreatedAtAction(nameof(GetById), new { id = @Meeting.Id }, @Meeting);
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, [FromBody] Meeting @Meeting)
    {
        if (id != @Meeting.Id)
        {
            return BadRequest();
        }

        _meetingRepository.Update(@Meeting);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        _meetingRepository.Delete(id);
        return NoContent();
    }
    
}

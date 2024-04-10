using Newtonsoft.Json;
using MeetingSchedularApi;

public class MeetingRepository
 {
        
    private readonly string _filePath = "Meetings.json";

    public List<Meeting> GetAll()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Meeting>();
        }

        var json = File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<List<Meeting>>(json);
    }

    public Meeting GetById(Guid id)
    {
        return GetAll().Find(u => u.Id == id);
    }

    public void Add(Meeting Meeting)
    {
        var Meetings = GetAll();
        if (Meetings == null)
        {
            Meetings = new List<Meeting>();
            Meeting.Id = Guid.NewGuid();
        }
        else
        {
            Meeting.Id = Guid.NewGuid();
        }
        Meetings.Add(Meeting);
        Save(Meetings);
    }

    public void Update(Meeting Meeting)
    {
        var Meetings = GetAll();
        var index = Meetings.FindIndex(u => u.Id == Meeting.Id);
        if (index != -1)
        {
            Meetings[index] = Meeting;
            Save(Meetings);
        }
    }

    public void Delete(Guid id)
    {
        var Meetings = GetAll();
        Meetings.RemoveAll(u => u.Id == id);
        Save(Meetings);
    }

    private void Save(List<Meeting> Meetings)
    {
        var json = JsonConvert.SerializeObject(Meetings, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(_filePath, json);
    }
 }



// UserRepository.cs
using System.Collections.Generic;
using System.IO;
using MeetingSchedularApi;
using System.Xml;
using Newtonsoft.Json;

public class UserRepository
{
    private readonly string _filePath = "users.json";

    public List<User> GetAll()
    {
        if (!File.Exists(_filePath))
        {
            return new List<User>();
        }

        var json = File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<List<User>>(json);
    }

    public User GetById(Guid id)
    {
        return GetAll().Find(u => u.Id == id);
    }

    public void Add(User user)
    {
        var users = GetAll();
        user.Id = Guid.NewGuid();
        users.Add(user);
        Save(users);
    }

    public void Update(User user)
    {
        var users = GetAll();
        var index = users.FindIndex(u => u.Id == user.Id);
        if (index != -1)
        {
            users[index] = user;
            Save(users);
        }
    }

    public void Delete(Guid id)
    {
        var users = GetAll();
        users.RemoveAll(u => u.Id == id);
        Save(users);
    }

    private void Save(List<User> users)
    {
        var json = JsonConvert.SerializeObject(users, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(_filePath, json);
    }
}

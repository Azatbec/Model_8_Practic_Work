using System;
using System.Collections.Generic;

// 1. Интерфейс IMediator
public interface IMediator
{
    void SendMessage(string message, IUser sender, string channel);
    void AddUser(IUser user, string channel);
    void RemoveUser(IUser user, string channel);
    void SendPrivateMessage(string message, IUser sender, IUser receiver);
}

// 2. Класс ChatMediator
public class ChatMediator : IMediator
{
    private readonly Dictionary<string, List<IUser>> _channels = new();

    public void SendMessage(string message, IUser sender, string channel)
    {
        if (!_channels.ContainsKey(channel))
        {
            Console.WriteLine($"Channel '{channel}' does not exist. Creating it now.");
            _channels[channel] = new List<IUser>();
        }

        if (!_channels[channel].Contains(sender))
        {
            Console.WriteLine($"{sender.Name} is not a member of channel '{channel}'. Please join first.");
            return;
        }

        foreach (var user in _channels[channel])
        {
            if (user != sender)
            {
                user.ReceiveMessage(message, sender, channel);
            }
        }
    }

    public void AddUser(IUser user, string channel)
    {
        if (!_channels.ContainsKey(channel))
        {
            _channels[channel] = new List<IUser>();
        }
        _channels[channel].Add(user);
        Console.WriteLine($"{user.Name} has joined the channel '{channel}'.");
        NotifyUsers($"{user.Name} has joined the channel '{channel}'.", channel);
    }

    public void RemoveUser(IUser user, string channel)
    {
        if (_channels.ContainsKey(channel))
        {
            _channels[channel].Remove(user);
            Console.WriteLine($"{user.Name} has left the channel '{channel}'.");
            NotifyUsers($"{user.Name} has left the channel '{channel}'.", channel);
        }
    }

    private void NotifyUsers(string message, string channel)
    {
        if (_channels.ContainsKey(channel))
        {
            foreach (var user in _channels[channel])
            {
                user.ReceiveNotification(message, channel);
            }
        }
    }

    public void SendPrivateMessage(string message, IUser sender, IUser receiver)
    {
        receiver.ReceiveMessage(message, sender, "Private");
    }
}

// 3. Интерфейс IUser
public interface IUser
{
    string Name { get; }
    void ReceiveMessage(string message, IUser sender, string channel);
    void ReceiveNotification(string message, string channel);
}

// 4. Класс User
public class User : IUser
{
    public string Name { get; }
    private readonly IMediator _mediator;

    public User(string name, IMediator mediator)
    {
        Name = name;
        _mediator = mediator;
    }

    public void SendMessage(string message, string channel)
    {
        Console.WriteLine($"{Name} sends message: '{message}' in channel '{channel}'.");
        _mediator.SendMessage(message, this, channel);
    }

    public void SendPrivateMessage(string message, IUser receiver)
    {
        Console.WriteLine($"{Name} sends private message: '{message}' to {receiver.Name}.");
        _mediator.SendPrivateMessage(message, this, receiver);
    }

    public void ReceiveMessage(string message, IUser sender, string channel)
    {
        Console.WriteLine($"{Name} received message: '{message}' from {sender.Name} in channel '{channel}'.");
    }

    public void ReceiveNotification(string message, string channel)
    {
        Console.WriteLine($"{Name} received notification: '{message}' in channel '{channel}'.");
    }
}

// 5. Клиентский код
class Program
{
    static void Main(string[] args)
    {
        var mediator = new ChatMediator();

        var user1 = new User("Alice", mediator);
        var user2 = new User("Bob", mediator);
        var user3 = new User("Charlie", mediator);
        var user4 = new User("Admin", mediator); // Добавим администратора

        mediator.AddUser(user1, "General");
        mediator.AddUser(user2, "General");
        mediator.AddUser(user3, "Sports");
        mediator.AddUser(user4, "General"); // Администратор может тоже участвовать

        user1.SendMessage("Hello, everyone!", "General");
        user3.SendMessage("Good game last night!", "Sports");

        mediator.RemoveUser(user2, "General");
        user1.SendMessage("Where did Bob go?", "General");

        // Пример отправки приватного сообщения
        user1.SendPrivateMessage("Hi Bob, are you there?", user2); // Bob is removed from the channel

        // Тестирование отправки сообщения в несуществующий канал
        user4.SendMessage("New rules for the General channel.", "Rules");

        // Проверка кросс-канальной отправки
        mediator.AddUser(user2, "Sports"); // Bob возвращается в Sports
        user2.SendMessage("Can we talk about the game here?", "Sports");
    }
}

using System;
using System.Collections.Generic;

namespace Model8_Practic_work_3
{
    public interface IMediator
    {
        void SendMessage(string message, IUser sender);
        void AddUser(IUser user);
        void RemoveUser(IUser user);
    }

    public interface IUser
    {
        void SendMessage(string message);
        void ReceiveMessage(string message, IUser sender);
        void ReceiveSystemMessage(string message);
        string GetName();
    }

    public class ChatMediator : IMediator
    {
        private List<IUser> users;

        public ChatMediator()
        {
            users = new List<IUser>();
        }

        public void AddUser(IUser user)
        {
            users.Add(user);
        }

        public void RemoveUser(IUser user)
        {
            users.Remove(user);
        }

        public void SendMessage(string message, IUser sender)
        {
            foreach (var user in users)
            {
                if (user != sender)
                {
                    user.ReceiveMessage(message, sender);
                }
            }
        }

        public class User : IUser
        {
            private string name;
            private IMediator mediator;

            public User(string name, IMediator mediator)
            {
                this.name = name;
                this.mediator = mediator;
            }

            public void SendMessage(string message)
            {
                Console.WriteLine($"{name} sends message: {message}");
                mediator.SendMessage(message, this);
            }

            public void ReceiveMessage(string message, IUser sender)
            {
                Console.WriteLine($"{name} receives message from {sender.GetName()}: {message}");
            }

            public void ReceiveSystemMessage(string message)
            {
                Console.WriteLine($"[System message to {name}]: {message}");
            }

            public string GetName()
            {
                return name;
            }
        }
    }

    public class ChannelMediator : IMediator
    {
        private string channelName;
        private Dictionary<string, List<IUser>> channels;

        public ChannelMediator(string channelName)
        {
            this.channelName = channelName;
            channels = new Dictionary<string, List<IUser>>();
        }

        public void AddUser(IUser user)
        {
            if (!channels.ContainsKey(channelName))
            {
                channels[channelName] = new List<IUser>();
            }
            channels[channelName].Add(user);
        }

        public void RemoveUser(IUser user)
        {
            if (channels.ContainsKey(channelName))
            {
                channels[channelName].Remove(user);
            }
        }

        public void SendMessage(string message, IUser sender)
        {
            if (channels.ContainsKey(channelName))
            {
                foreach (IUser user in channels[channelName])
                {
                    if (user != sender)
                    {
                        user.ReceiveMessage(message, sender);
                    }
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Example usage
            IMediator chatMediator = new ChatMediator();
            IUser user1 = new ChatMediator.User("Alice", chatMediator);
            IUser user2 = new ChatMediator.User("Bob", chatMediator);
            IUser user3 = new ChatMediator.User("Charlie", chatMediator);

            chatMediator.AddUser(user1);
            chatMediator.AddUser(user2);
            chatMediator.AddUser(user3);

            user1.SendMessage("Hello, everyone!");
            user2.SendMessage("Hi, Alice!");

            Console.WriteLine("\nUsing Channel Mediator:");

            IMediator channelMediator = new ChannelMediator("General");
            IUser user4 = new ChatMediator.User("David", channelMediator);
            IUser user5 = new ChatMediator.User("Eve", channelMediator);

            channelMediator.AddUser(user4);
            channelMediator.AddUser(user5);

            user4.SendMessage("Welcome to the General channel!");
            user5.SendMessage("Thanks, David!");

            Console.ReadKey();
        }
    }
}

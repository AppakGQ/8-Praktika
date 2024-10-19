using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace ConsoleApp5
{
    public interface IMediator
    {
        void SendMessage(string message, IUser user);
        void AddUser(IUser user);
        void RemoveUser(IUser user);
    }
    public class ChatMediator : IMediator
    {
        private List<IUser> users;
        public ChatMediator()
        {
            users = new List<IUser>();
        }
        private void NotifyUsers(string message)
        {
            foreach (var user in users)
            {
                user.ReceiveSystemMessage(message);
            }
        }
        public void AddUser(IUser user)
        {
            users.Add(user);
            NotifyUsers(user.GetName() + " join to chat");
        }
        public void RemoveUser(IUser user)
        {
            users.Remove(user);
            NotifyUsers(user.GetName() + " left chat");
        }
        public void SendMessage(string message, IUser sender)
        {
            foreach (var user in users)
            {
                if (user != sender)
                    user.ReceiveMessage(message, user);
            }
        }
    }


    public interface IUser
    {
        void SendMessage(string message);
        void ReceiveMessage(string message, IUser sender);
        void ReceiveSystemMessage(string message);
        string GetName();
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
        public string GetName()
        {
            return name;
        }
        public void SendMessage(string message)
        {
            Console.WriteLine("{0} send message: {1}", name, message);
            mediator.SendMessage(message, this);
        }
        public void ReceiveMessage(string message, IUser sender)
        {
            Console.WriteLine("{0} receive message from {1}: {2}",
                name, sender.GetName(), message);
        }
        public void ReceiveSystemMessage(string message)
        {
            Console.WriteLine("[System message for {0}]: {1}", name, message);
        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            ChatMediator chat = new ChatMediator();
            IUser user1 = new User("User1", chat);
            IUser user2 = new User("User2", chat);
            IUser user3 = new User("User3", chat);
            IUser user4 = new User("User4", chat);

            chat.AddUser(user1);
            chat.AddUser(user2);
            chat.AddUser(user3);
            chat.AddUser(user4);

            user1.SendMessage("Hello all");
        }
    }
}
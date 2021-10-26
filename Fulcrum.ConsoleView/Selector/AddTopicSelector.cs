using System;
using Fulcrum.Data.Models;

namespace Fulcrum.ConsoleView.Selector
{
    public class AddTopicSelector : IAddSelector<Topic>
    {
        public Topic SelectData()
        {
            Console.WriteLine("Adding a new topic");
            var topic = new Topic();

            Console.Write("Enter name: ");
            topic.Name = Console.ReadLine();

            return topic;
        }
    }
}
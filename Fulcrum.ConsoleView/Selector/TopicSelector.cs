using System;

namespace Fulcrum.ConsoleView.Selector
{
    public class TopicSelector : ISelector
    {
        public void SelectOutput()
        {
            Console.WriteLine("Select topic");
        }

        public string ReadChoice()
        {
            Console.Write("Your option: ");
            return Console.ReadLine();
        }
    }
}
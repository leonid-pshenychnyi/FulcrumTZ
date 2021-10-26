using System;

namespace Fulcrum.ConsoleView.Selector
{
    public class BaseSelector : ISelector
    {
        public void SelectOutput()
        {
            Console.WriteLine("Welcome to TZLearn");
            Console.WriteLine("1 - Learn words");
            Console.WriteLine("2 - Add word");
            Console.WriteLine("3 - Add topic");
            Console.WriteLine("0 - Exit");
        }

        public string ReadChoice()
        {
            Console.Write("Your option: ");
            return Console.ReadLine();
        }
    }
}
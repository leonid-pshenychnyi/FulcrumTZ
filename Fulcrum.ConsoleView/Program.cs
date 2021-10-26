using System;

namespace Fulcrum.ConsoleView
{
    static class Program
    {
        static void Main()
        {
            var selects = new OptionSelects.OptionSelects();
            var run = true;
            while (run)
            {
                var option = selects.Start();

                int topicId;
                switch (option)
                {
                    case "1":
                        topicId = selects.Topics();
                        selects.LearnWords(topicId);
                        break;
                    case "2":
                        topicId = selects.Topics();
                        selects.AddWord(topicId);
                        break;
                    case "3":
                        selects.AddTopic();
                        break;
                    default:
                        run = false;
                        break;
                }
            }
        }
    }
}
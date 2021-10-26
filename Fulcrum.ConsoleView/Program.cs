using Fulcrum.ConsoleView.Selector;

namespace Fulcrum.ConsoleView
{
    internal static class Program
    {
        private static void Main()
        {
            var selects =
                new OptionSelects.OptionSelects(
                    new BaseSelector(),
                    new TopicSelector(),
                    new AddTopicSelector(),
                    new AddWordSelector());
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
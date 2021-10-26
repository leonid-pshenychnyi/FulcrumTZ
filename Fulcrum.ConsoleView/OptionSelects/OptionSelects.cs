using System;
using System.Linq;
using Fulcrum.ConsoleView.Selector;
using Fulcrum.Data.EF;
using Fulcrum.Data.Models;

namespace Fulcrum.ConsoleView.OptionSelects
{
    public class OptionSelects
    {
        private readonly IAddSelector<Card> _addCardSelector;
        private readonly IAddSelector<Topic> _addTopicSelector;

        private readonly ISelector _baseSelector;
        private readonly TzLearnContext _db;
        private readonly ISelector _topicSelector;

        public OptionSelects(ISelector baseSelector,
            ISelector topicSelector,
            IAddSelector<Topic> addTopicSelector,
            IAddSelector<Card> addCardSelector)
        {
            _baseSelector = baseSelector;
            _topicSelector = topicSelector;
            _addTopicSelector = addTopicSelector;
            _addCardSelector = addCardSelector;
            _db = new TzLearnContext();
        }

        public string Start()
        {
            _baseSelector.SelectOutput();
            return _baseSelector.ReadChoice();
        }

        public int Topics()
        {
            _topicSelector.SelectOutput();
            var topics = _db.Topics.OrderBy(o => o.Id).ToList();

            for (var i = 0; i < topics.Count; i++) Console.WriteLine(i + 1 + " - " + topics[i].Name);

            var option = _topicSelector.ReadChoice();

            var isNumeric = int.TryParse(option, out var n);
            if (isNumeric && n > 0 && n <= topics.Count) return topics[n - 1].Id;

            Console.WriteLine("Incorrect input");
            return default;
        }

        public void LearnWords(int topicId)
        {
            if (topicId == default) return;

            var rng = new Random();
            var cards = _db.Cards.Where(w => w.TopicId == topicId).Where(w => !w.Learned).ToList();

            if (cards.Count == 0)
            {
                Console.WriteLine("This topic doesn't have cards");
                return;
            }

            var shuffledcards = cards.OrderBy(a => rng.Next()).ToList();

            for (var i = 0; i < shuffledcards.Count; i++)
            {
                Console.Write(i + 1 + "/" + shuffledcards.Count + " word: ");
                Console.WriteLine(shuffledcards[i].Word);

                Console.WriteLine("Do you know this word?");
                Console.Write("[Y/N]: ");
                var option = Console.ReadLine();

                var word = shuffledcards[i];

                switch (option?.ToLower())
                {
                    case "y":
                        word.RepeatTimes += 1;
                        if (word.RepeatTimes == 5) word.Learned = true;

                        Console.WriteLine("Good job!");

                        if (word.Learned)
                        {
                            Console.WriteLine("You learned a new word");
                        }
                        
                        Console.WriteLine("Word translation is: " + word.Translation);

                        _db.SaveChanges();
                        break;
                    case "n":
                        word.Learned = false;
                        if (word.RepeatTimes != 0) word.RepeatTimes -= 1;
                        Console.WriteLine("Word translation is: " + word.Translation);

                        _db.SaveChanges();
                        break;
                    default:
                        Console.WriteLine("Wrong input. Word skipped.");
                        break;
                }
            }
        }

        public void AddWord(int topicId)
        {
            if (topicId == default) return;

            var card = _addCardSelector.SelectData();
            card.TopicId = topicId;

            _db.Cards.Add(card);
            _db.SaveChanges();
        }

        public void AddTopic()
        {
            var topic = _addTopicSelector.SelectData();

            _db.Topics.Add(topic);
            _db.SaveChanges();
        }
    }
}
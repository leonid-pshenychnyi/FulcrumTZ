using System;
using System.Linq;
using Fulcrum.Data.EF;
using Fulcrum.Data.Models;

namespace Fulcrum.ConsoleView.OptionSelects
{
    public class OptionSelects
    {
        private readonly TzLearnContext _db;

        public OptionSelects()
        {
            _db = new TzLearnContext();
        }

        public string Start()
        {
            Console.WriteLine("Welcome to TZLearn");
            Console.WriteLine("1 - Learn words");
            Console.WriteLine("2 - Add word");
            Console.WriteLine("3 - Add topic");
            Console.WriteLine("0 - Exit");
                
            Console.Write("Your option: ");
            return Console.ReadLine();
        }

        public int Topics()
        {
            var topics = _db.Topics.OrderBy(o => o.Id).ToList();
            Console.WriteLine("Select topic");

            for (var i = 0; i < topics.Count; i++)
            {
                Console.WriteLine((i + 1) + " - " + topics[i].Name);
            }

            Console.Write("Your option: ");
            var option = Console.ReadLine();

            var isNumeric = int.TryParse(option, out var n);
            if (isNumeric && (n > 0 && n <= topics.Count))
            {
                return topics[n - 1].Id;
            }

            Console.WriteLine("Incorrect input");
            return default;
        }

        public void LearnWords(int topicId)
        {
            if (topicId == default)
            {
                return;
            }
            
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
                Console.Write((i + 1) +"/" + shuffledcards.Count + " word: ");
                Console.WriteLine(shuffledcards[i].Word);
                
                Console.WriteLine("Do you know this word?");
                Console.Write("[Y/N]: ");
                var option = Console.ReadLine();

                var word = shuffledcards[i];
                
                switch (option?.ToLower())
                {
                    case "y":
                        word.RepeatTimes += 1;
                        if (word.RepeatTimes == 5)
                        {
                            word.Learned = true;
                        }
                        
                        Console.WriteLine("Good job!");
                        Console.WriteLine(word.Learned ? "You learned a new word" : string.Empty);

                        _db.SaveChanges();
                        break;
                    case "n":
                        word.Learned = false;
                        if (word.RepeatTimes != 0)
                        {
                            word.RepeatTimes -= 1;
                        }

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
            if (topicId == default)
            {
                return;
            }
            
            Console.WriteLine("Adding a new word");
            var card = new Card();
            card.TopicId = topicId;
            
            Console.Write("Enter word: ");
            card.Word = Console.ReadLine();
            
            Console.Write("Enter translation: ");
            card.Translation = Console.ReadLine();
            
            _db.Cards.Add(card);
            _db.SaveChanges();
        }

        public void AddTopic()
        {
            Console.WriteLine("Adding a new topic");
            var topic = new Topic();
            
            Console.Write("Enter name: ");
            topic.Name = Console.ReadLine();

            _db.Topics.Add(topic);
            _db.SaveChanges();
        }
    }
}
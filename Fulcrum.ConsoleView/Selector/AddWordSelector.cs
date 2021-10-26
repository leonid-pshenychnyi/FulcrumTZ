using System;
using Fulcrum.Data.Models;

namespace Fulcrum.ConsoleView.Selector
{
    public class AddWordSelector : IAddSelector<Card>
    {
        public Card SelectData()
        {
            Console.WriteLine("Adding a new word");
            var card = new Card();

            Console.Write("Enter word: ");
            card.Word = Console.ReadLine();

            Console.Write("Enter translation: ");
            card.Translation = Console.ReadLine();

            return card;
        }
    }
}
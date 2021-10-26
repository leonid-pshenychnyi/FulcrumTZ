namespace Fulcrum.Data.Models
{
    public class Card
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string Word { get; set; }
        public string Translation { get; set; }
        public int RepeatTimes { get; set; }
        public bool Learned { get; set; }
    }
}
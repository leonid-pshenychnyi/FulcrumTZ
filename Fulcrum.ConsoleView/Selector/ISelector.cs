namespace Fulcrum.ConsoleView.Selector
{
    public interface ISelector
    {
        void SelectOutput();
        string ReadChoice();
    }
}
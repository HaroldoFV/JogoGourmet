namespace JogoGourmet.Application.Interfaces
{
    public interface IUserInteractionService
    {
        public void ShowMessage(string message);
        public string PromptUser(string prompt);
    }

}

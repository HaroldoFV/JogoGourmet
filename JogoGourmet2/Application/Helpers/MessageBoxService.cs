using JogoGourmet.Application.Interfaces;

namespace JogoGourmet.Application.Helpers
{
    public class MessageBoxService : IMessageBoxService
    {
        public MessageBoxService() { }
        public DialogResult ShowMessageBox(string message, MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            return MessageBox.Show(message, "Jogo Gourmet", buttons);
        }
    }


}

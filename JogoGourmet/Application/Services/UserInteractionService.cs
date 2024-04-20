using JogoGourmet.Application.Interfaces;
using JogoGourmet.UI.Views;

namespace JogoGourmet.Application.Services
{
    public class UserInteractionService : IUserInteractionService
    {
        private readonly IMessageBoxService _messageBoxService;

        public UserInteractionService(IMessageBoxService messageBoxService)
        {
            _messageBoxService = messageBoxService;
        }

        public void ShowMessage(string message)
        {
            _messageBoxService.ShowMessageBox(message, MessageBoxButtons.OK);
        }

        public string PromptUser(string prompt)
        {
            string userInput = string.Empty;
            do
            {
                using (var form = new PromptForm(prompt))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        userInput = form.TextBox.Text;
                        if (string.IsNullOrWhiteSpace(userInput))
                        {
                            _messageBoxService.ShowMessageBox("Por favor, insira um valor válido.", MessageBoxButtons.OK);
                        }
                    }
                }
            } while (string.IsNullOrWhiteSpace(userInput));

            return userInput;
        }
    }

}

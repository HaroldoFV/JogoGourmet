namespace JogoGourmet.Application.Interfaces
{
    public interface IMessageBoxService
    {
        DialogResult ShowMessageBox(string message, MessageBoxButtons buttons);
    }
}

namespace JogoGourmet.UI.Views
{
    public class PromptForm : Form
    {
        public TextBox TextBox { get; private set; }
        public Button Button { get; private set; }

        public PromptForm(string prompt)
        {
            Label label = new Label();
            label.Text = prompt;
            label.Location = new Point(10, 10);
            label.AutoSize = false;
            label.Width = 300;
            label.Height = 20;

            TextBox = new TextBox();
            TextBox.Location = new Point(10, 40);
            TextBox.Size = new Size(300, 20);

            Button = new Button();
            Button.Text = "OK";
            Button.Location = new Point(10, 70);
            Button.DialogResult = DialogResult.OK;

            Text = "Jogo Gourmet";
            ClientSize = new Size(330, 120);
            Controls.AddRange(new Control[] { label, TextBox, Button });
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterScreen;
            AcceptButton = Button;

            // Event handling for closing the form properly
            Button.Click += (sender, e) => this.DialogResult = DialogResult.OK;
        }
    }
}

using System.IO;
using System.Windows;

namespace Label_Sticker
{
    public partial class EditWindow : Window
    {
        private string filePath;

        public EditWindow(string path)
        {
            InitializeComponent();
            filePath = path;
            LoadFileContent();
        }

        private void LoadFileContent()
        {
            if (File.Exists(filePath))
            {
                EditorTextBox.Text = File.ReadAllText(filePath);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(filePath, EditorTextBox.Text);
            this.Close();
        }
    }
}
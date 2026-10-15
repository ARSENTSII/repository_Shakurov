using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;



namespace Task2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
        private string currentFile = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog { Filter = Filter };

            if (dialog.ShowDialog() != true)
                return;

            try
            {
                textBox1.Text = File.ReadAllText(dialog.FileName, Encoding.UTF8);
                currentFile = dialog.FileName;
                Title = "Текстовый редактор — " + Path.GetFileName(currentFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog { Filter = Filter };

            if (currentFile != null)
                dialog.FileName = Path.GetFileName(currentFile);

            if (dialog.ShowDialog() != true)
                return;

            try
            {
                File.WriteAllText(dialog.FileName, textBox1.Text, Encoding.UTF8);
                currentFile = dialog.FileName;
                Title = "Текстовый редактор — " + Path.GetFileName(currentFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

```
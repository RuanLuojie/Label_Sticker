using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Label_Sticker
{
    public partial class MainWindow : Window
    {
        private string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Work_itme.txt");
        private FileSystemWatcher watcher;
        private DispatcherTimer debounceTimer;

        public MainWindow()
        {
            InitializeComponent();
            EnsureFileExists();
            LoadWorkItems();
            SetupFileWatcher();
        }

        private void EnsureFileExists()
        {
            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine("Task 1");
                    sw.WriteLine("Task 2");
                    sw.WriteLine("Task 3");
                }
            }
        }

        private void LoadWorkItems()
        {
            try
            {
                ItemPanel.Children.Clear();
                if (File.Exists(filePath))
                {
                    var lines = File.ReadAllLines(filePath);
                    foreach (var line in lines)
                    {
                        var textBlock = new TextBlock
                        {
                            Text = line.Trim(),
                            FontFamily = new System.Windows.Media.FontFamily("Microsoft JhengHei"),
                            FontSize = 16,
                            Foreground = System.Windows.Media.Brushes.White,
                            TextWrapping = TextWrapping.Wrap
                        };
                        ItemPanel.Children.Add(textBlock);
                    }
                }
                else
                {
                    var textBlock = new TextBlock
                    {
                        Text = "No items found.",
                        FontFamily = new System.Windows.Media.FontFamily("Microsoft JhengHei"),
                        FontSize = 16,
                        Foreground = System.Windows.Media.Brushes.White,
                        TextWrapping = TextWrapping.Wrap
                    };
                    ItemPanel.Children.Add(textBlock);
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show($"文件无法访问: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetupFileWatcher()
        {
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }

            watcher = new FileSystemWatcher
            {
                Path = Path.GetDirectoryName(filePath),
                Filter = Path.GetFileName(filePath),
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.Size
            };
            watcher.Changed += OnChanged;
            watcher.EnableRaisingEvents = true;

            debounceTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };
            debounceTimer.Tick += (s, e) =>
            {
                debounceTimer.Stop();
                LoadWorkItems();
            };
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                if (!debounceTimer.IsEnabled)
                {
                    debounceTimer.Start();
                }
            });
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditWindow editWindow = new EditWindow(filePath)
            {
                Owner = this,
                Left = this.Left,
                Top = this.Top
            };
            editWindow.ShowDialog();
            LoadWorkItems();
        }



        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}

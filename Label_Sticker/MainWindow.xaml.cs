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
            // 检查文件是否存在，如果不存在则创建
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
                            TextWrapping = TextWrapping.Wrap // 允许文本自动换行
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
                        TextWrapping = TextWrapping.Wrap // 允许文本自动换行
                    };
                    ItemPanel.Children.Add(textBlock);
                }
            }
            catch (IOException ex)
            {
                // 处理文件正在被其他进程使用的情况
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
                Interval = TimeSpan.FromMilliseconds(500) // 延迟执行，防止多次触发
            };
            debounceTimer.Tick += (s, e) =>
            {
                debounceTimer.Stop();
                LoadWorkItems();
            };
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            // 触发延迟加载，避免重复触发导致的问题
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
                Owner = this, // 设置主窗口为Owner
                Left = this.Left, // 将EditWindow的左边位置对齐到MainWindow
                Top = this.Top // 将EditWindow的顶部位置对齐到MainWindow
            };
            editWindow.ShowDialog();
            LoadWorkItems(); // 重新加载内容以反映更改
        }


        // 实现窗口拉伸功能
        private void ResizeWindow(object sender, MouseButtonEventArgs e)
        {
            Border border = sender as Border;

            if (border != null)
            {
                // 设置不同方向的光标和调整窗口大小的方向
                if (border.Cursor == Cursors.SizeWE)
                {
                    this.Width = e.GetPosition(this).X;
                }
                else if (border.Cursor == Cursors.SizeNS)
                {
                    this.Height = e.GetPosition(this).Y;
                }
            }
        }

        // 关闭窗口按钮
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // 最小化窗口按钮
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        // 最大化或恢复窗口按钮
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

        // 通过标题栏拖动窗口
        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}

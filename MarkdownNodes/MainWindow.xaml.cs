using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;

namespace MarkdownNodes
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel vm = new MainViewModel();
        public MainWindow()
        {
            InitializeComponent();
            this.Closing += MainWindow_Closing;
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Top = 0;
            this.Left = 745;
            this.DataContext = vm;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            vm.Close();
        }

        private void Btn_Shutdown_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_Setting_Click(object sender, RoutedEventArgs e)
        {
            SettingWindow window = new SettingWindow();
            window.Owner = this;
            window.DataContext = vm;
            window.ShowDialog();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            models.MarkDownFile file = (sender as FrameworkElement).DataContext as models.MarkDownFile;
            if (file != null)
            { 
                NoteWindow window = new NoteWindow(file);
                window.Owner = this;
                window.Show();
            }
        }

        private void Btn_Close_FileList_Click(object sender, RoutedEventArgs e)
        {
            this.vm.FileListVisible = Visibility.Collapsed;
        }

        private void Btn_Show_FileList_Click(object sender, RoutedEventArgs e)
        {
            this.vm.FileListVisible = Visibility.Visible;
        }

        private void Card_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }

    public class VisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visiblility = (Visibility)value;
            if (visiblility == Visibility.Visible)
            {
                return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

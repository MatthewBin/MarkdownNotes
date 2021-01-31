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
using System.Windows.Shapes;

namespace MarkdownNodes
{
    /// <summary>
    /// NoteWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NoteWindow : Window
    {
        public NoteWindow()
        {
            InitializeComponent();
            this.Loaded += NoteWindow_Loaded;
        }

        private void NoteWindow_Loaded(object sender, RoutedEventArgs e)
        {
            List<Brush> colors = new List<Brush>();
            colors.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9c1")));
            colors.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222")));
            colors.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#922")));
            colors.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#292")));
            colors.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#229")));
            colors.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#c2c")));
            cb_color.ItemsSource = colors;
        }

        private void Btn_close_window(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }

}

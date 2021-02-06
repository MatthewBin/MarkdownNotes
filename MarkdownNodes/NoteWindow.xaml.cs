using MarkdownNodes.models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
        private MarkDownFile file = null;
        public NoteWindow(MarkDownFile file)
        {
            InitializeComponent();
            this.Loaded += NoteWindow_Loaded;
            this.file = file;
        }

        private void NoteWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (file == null)
            {
                MessageBox.Show("目录设置内容有误，请重新设置！");
                this.Close();
                return;
            }
            if (!File.Exists(file.FullName))
            {
                MessageBox.Show("文件已经不存在，请检查！");
                this.Close();
                return;
            }
            string text = utils.Helper.GetMardownFileText(file.FullName);
            if(text == null)
            {
                MessageBox.Show("文件内容为空或读取文件异常，请检查！");
                this.Close();
                return;
            }
            this.FileText = text;
            this.FileName = file.Name;
        }

        private void Btn_close_window(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ColorPicker_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var colorPicker = (sender as MaterialDesignThemes.Wpf.ColorPicker);
            if (colorPicker != null)
            {
                this.Background = new SolidColorBrush(colorPicker.Color);
            }
        }



        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }
        public static readonly DependencyProperty FileNameProperty = DependencyProperty.Register("FileName", typeof(string), typeof(NoteWindow), new PropertyMetadata(""));


        public string FileText
        {
            get { return (string)GetValue(FileTextProperty); }
            set { SetValue(FileTextProperty, value); }
        }
        public static readonly DependencyProperty FileTextProperty = DependencyProperty.Register("FileText", typeof(string), typeof(NoteWindow), new PropertyMetadata(""));


    }

    public class TextToFlowDocumentConverter : DependencyObject, IValueConverter
    {
        public Markdown Markdown
        {
            get { return (Markdown)GetValue(MarkdownProperty); }
            set { SetValue(MarkdownProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Markdown.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkdownProperty =
            DependencyProperty.Register("Markdown", typeof(Markdown), typeof(TextToFlowDocumentConverter), new PropertyMetadata(null));

        /// <summary>
        /// Converts a value. 
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
            {
                return null;
            }

            var text = (string)value;

            var engine = Markdown ?? mMarkdown.Value;

            return engine.Transform(text);
        }

        /// <summary>
        /// Converts a value. 
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private readonly Lazy<Markdown> mMarkdown
            = new Lazy<Markdown>(() => new Markdown());
    }

}

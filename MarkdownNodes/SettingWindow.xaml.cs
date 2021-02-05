using MarkdownNodes.models;
using MarkdownNodes.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MarkdownNodes
{
    /// <summary>
    /// SettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindow : Window
    {
        private MarkDownDir dir = null;
        private MainViewModel mv = null;
        public SettingWindow()
        {
            InitializeComponent();
            this.Loaded += SettingWindow_Loaded;
        }

        private void SettingWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.mv = this.DataContext as MainViewModel;
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Folder_Select_Button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "选择需要设置的md文件目录";
            DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            string dirPath = dialog.SelectedPath.Trim();
            dir = Helper.GetMarkDownDir(dirPath);

            this.tb_folder.Text = dir.FullPath;
            this.tb_display.Text = dir.DisplayName;
            this.list_file.ItemsSource = dir.MarkDownFiles;
        }

        private void Add_Item_Button_Click(object sender, RoutedEventArgs e)
        {
            if (dir == null)
            {
                return;
            }

            if (tb_display.Text.Trim() == "")
            {
                tb_display.Text = dir.DisplayName;
            }
            else
            {
                dir.DisplayName = tb_display.Text.Trim();
            }

            if (dir.DisplayName == null || dir.DisplayName.Trim() == "")
            {
                System.Windows.MessageBox.Show("显示名称不能为空");
                return;
            }

            mv.AddDir(dir);
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MarkDownDir dir = (sender as FrameworkElement).DataContext as MarkDownDir;
            if (dir!=null)
            {
                this.mv.RemoveDir(dir);
            }
        }
    }
}

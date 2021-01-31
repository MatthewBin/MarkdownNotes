using MarkdownNodes.models;
using MarkdownNodes.utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MarkdownNodes
{
    public class MainViewModel : ViewModelBase
    {
        private string saveFilePath = Path.Combine(Environment.CurrentDirectory, "data.save");
        public MainViewModel()
        {
            this.initDataFromFile();
        }

        public ObservableCollection<MarkDownDir> DirList { get; set; }

        public void AddDir(MarkDownDir dir)
        {
            bool hasAdd = false;
            for (int i = 0; i < DirList.Count; i++)
            {
                if (DirList[i].FullPath == dir.FullPath)
                {
                    DirList.RemoveAt(i);
                    DirList.Insert(i, dir);
                    hasAdd = true;
                    break;
                }
            }
            if (!hasAdd)
            {
                DirList.Add(dir);
            }
        }

      
        public void Close()
        {
            this.saveToFile();
        }

        private void initDataFromFile()
        {
            if (!File.Exists(saveFilePath))
            {
                File.Create(saveFilePath);
            }
            else
            {
                DirList = Helper.XmlDeserialize<ObservableCollection<MarkDownDir>>(saveFilePath);
            }

            if (DirList == null)
            {
                DirList = new ObservableCollection<MarkDownDir>();
            }

        }

        private void saveToFile()
        {
            Helper.XmlSerializer<ObservableCollection<MarkDownDir>>(DirList, saveFilePath);
        }
    }

    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropChang(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}

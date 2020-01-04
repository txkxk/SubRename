using Microsoft.Win32;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SubRename
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] movieList;
        string[] subList;
        public MainWindow()
        {
            InitializeComponent();
        }

        private string[] GetFiles()
        {
            string[] files = null;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;
            //dlg.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            //dlg.FilterIndex = 2; 
            //dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == true)
                files = dlg.FileNames;
            return files;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            movieList = GetFiles();
            if(movieList!=null)
            {
                for (int i = 0; i < movieList.Length; i++)
                {
                    TextBlock t = new TextBlock();
                    FileInfo file = new FileInfo(movieList[i]);
                    t.Text = file.Name;
                    movieListView.Items.Add(t);
                }
            }
        }

        private void MovieListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            subList = GetFiles();
            if (subList != null)
            {
                for (int i = 0; i < subList.Length; i++)
                {
                    TextBlock t = new TextBlock();
                    FileInfo file = new FileInfo(subList[i]);
                    t.Text = file.Name;
                    subListView.Items.Add(t);
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (movieList.Length != subList.Length)
            {
                MessageBox.Show("选择的文件数量不一致！");
                return;
            }

            for (int i = 0; i < movieList.Length; i++)
            {
                string subPath = subList[i];
                string moviePath = movieList[i];
                FileInfo subFile = new FileInfo(subPath);
                FileInfo movieFile = new FileInfo(moviePath);
                int lastPoint = movieFile.Name.LastIndexOf('.');
                string movieName = movieFile.Name.Substring(0, lastPoint);
                string subSuffix = "." + subFile.Name.Split('.').Last();
                string newSubPath = subFile.DirectoryName + @"\" + movieName + subSuffix;
                //Console.WriteLine(movieName);
                subFile.MoveTo(newSubPath);
            }

            MessageBox.Show("完成！");
        }
    }
}

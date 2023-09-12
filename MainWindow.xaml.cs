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
using System.Windows.Shapes;

namespace ai_ui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            /*
            * Windowの表示位置をマニュアル指定
            */
            this.WindowStartupLocation = WindowStartupLocation.Manual;

            /*
             * 表示位置(Top)を調整。
             * 「ディスプレイの作業領域の高さ」-「表示するWindowの高さ」
             */
            this.Top = SystemParameters.WorkArea.Height - this.Height;

            /*
             * 表示位置(Left)を調整
             * 「ディスプレイの作業領域の幅」-「表示するWindowの幅」
             */
            this.Left = SystemParameters.WorkArea.Width - this.Width;
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void RichTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var inputBox = sender as RichTextBox;
                var textRange = new TextRange(inputBox.Document.ContentStart, inputBox.Document.ContentEnd);
                var textContent = textRange.Text;
                textRange.Text = "";

                // Messageの作成
                var msgBox = new RichTextBox();
                msgBox.AppendText(textContent);
                msgBox.Margin = new Thickness(5);
                msgBox.IsReadOnly = true;
                msgBox.Document.LineHeight = 1;

                msgBox.Width = 320;

                var lineCount = msgBox.Document.Blocks.Count;
                msgBox.Height = lineCount * 20;

                // メッセージをChatに追加
                var listBoxItem = new ListBoxItem();
                listBoxItem.Content = msgBox;
                chatBox.Items.Add(listBoxItem);
                chatBox.SelectedItem = chatBox.Items[chatBox.Items.Count - 1];
                chatBox.ScrollIntoView(chatBox.SelectedItem);

            }
        }
    }
}

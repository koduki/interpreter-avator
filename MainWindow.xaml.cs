using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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
                var prompt = textRange.Text;
                textRange.Text = "";

                chat(prompt);

                chatBox.SelectedItem = chatBox.Items[chatBox.Items.Count - 1];
                chatBox.ScrollIntoView(chatBox.SelectedItem);
            }
        }

        private void chat(string prompt)
        {
            chatBox.Items.Add(makeMessage(prompt, true));

            var repoonse = "あああああああ\nああああああああああああ\nああああああああああ\nあああああああああああああああああ\nあああああああああ\nあああああああああ\nあああああああああああ";
            chatBox.Items.Add(makeMessage(repoonse, false));
        }

        private static ListBoxItem makeMessage(string textContent, bool isPrompt)
        {
            // Gridの作成
            var mainGrid = new Grid();
            mainGrid.RowDefinitions.Add(new RowDefinition());
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition());

            // Messageの作成
            var msgBox = new RichTextBox();
            msgBox.AppendText(textContent);
            msgBox.Margin = new Thickness(5);
            msgBox.IsReadOnly = true;
            msgBox.Document.LineHeight = 1;
            msgBox.Width = 300;
            var lineCount = msgBox.Document.Blocks.Count;
            msgBox.Height = lineCount * 20;
            mainGrid.Children.Add(msgBox);

            if (isPrompt)
            {
                msgBox.Margin = new Thickness(48 + 25 + 5, 5, 5, 5);
            }
            else
            {
                // Icon
                var faceIcon = new Image();
                faceIcon.Height = 48;
                faceIcon.Width = 48;
                faceIcon.Margin = new Thickness(1);
                faceIcon.Source = new BitmapImage(new Uri("pack://application:,,,/icon.png"));
                mainGrid.Children.Add(faceIcon);

                // Layoutにマップ
                Grid.SetRow(faceIcon, 0);
                Grid.SetColumn(faceIcon, 0);
                Grid.SetRow(msgBox, 0);
                Grid.SetColumn(msgBox, 1);

            }
            // ListBoxItemを追加
            var listBoxItem = new ListBoxItem();
            listBoxItem.Content = mainGrid;

            return listBoxItem;
        }
    }
}

using System;
using System.Windows;

namespace Lab5_test2
{
    public partial class MainWindow : Window
    {
        private WordViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new WordViewModel();
            DataContext = _viewModel;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SearchWord();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddWord();
        }
    }
}
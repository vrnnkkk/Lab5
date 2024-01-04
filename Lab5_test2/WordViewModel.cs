using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Lab5_test2
{
    public class WordViewModel : INotifyPropertyChanged
    {
        private string _searchText;
        private string _newWordText;
        private ObservableCollection<Word> _treeData;
        private Word _selectedWord;

        public Dictionary dictionary = new Dictionary();

        public event PropertyChangedEventHandler PropertyChanged;

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }

        public string NewWordText
        {
            get => _newWordText;
            set
            {
                _newWordText = value;
                OnPropertyChanged(nameof(NewWordText));
            }
        }

        public ObservableCollection<Word> TreeData
        {
            get => _treeData;
            set
            {
                _treeData = value;
                OnPropertyChanged(nameof(TreeData));
            }
        }

        public Word SelectedWord
        {
            get => _selectedWord;
            set
            {
                _selectedWord = value;
                OnPropertyChanged(nameof(SelectedWord));
            }
        }

        public WordViewModel()
        {
            TreeData = new ObservableCollection<Word>();
        }

        public void SearchWord()
        {
            TreeData.Clear();
            if (!string.IsNullOrEmpty(SearchText))
            {
                Word rootWord = new Word(null, SearchText, null, null);
                foreach(Word word in dictionary.Words)
                {
                    if(rootWord.HasTheSameRoot(word))
                    {
                        TreeData.Add(word);
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid word format. Please use the format: [root]");
            }
        }

        public void AddWord()
        {
            Word newWord = ParseWord(NewWordText);
            if (newWord != null)
            {
                dictionary.AddWord(newWord);
                NewWordText = string.Empty;
            }
            else
            {
                MessageBox.Show("Invalid word format. Please use the format: pre-[root]-suf-end");
            }
        }

        public Word ParseWord(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            string[] parts = input.Split('-');

            if (parts.Length < 4)
            {
                return null;
            }

            string prefix = parts[0];
            string root = parts[1];
            string suffix = parts[2];
            string ending = parts[3];

            return new Word(prefix, root, suffix, ending); 
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class WordValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = (value ?? string.Empty).ToString();

            if (IsValidWordFormat(input))
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "Invalid word format");
            }
        }

        private bool IsValidWordFormat(string input)
        {
            return input.Contains("-[") && input.Contains("]-");
        }
    }

    public class WordValidationRuleTwo : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = (value ?? string.Empty).ToString();

            if (IsValidWordFormat(input))
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "Invalid word format");
            }
        }

        private bool IsValidWordFormat(string input)
        {
            return input.Contains("[") && input.Contains("]");
        }
    }

    public class WordConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as Word)?.FullWord;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Word(value?.ToString(), null, null, null);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5_test2
{
    public class Dictionary
    {
        //совокупность объектов класса Word 
        private readonly List<Word> dictionary = new List<Word>();

        public IEnumerable<Word> Words
        {
            get
            {
                return dictionary;
            }

        }

        //метод добавления нового слова 
        public void AddWord(Word word)
        {
            dictionary.Add(word);
            dictionary.Sort((x, y) => x.count.CompareTo(y.count));
        }

        //метод поиска индекса переданного слова в словаре
        public Word GetWordByText(String word)
        {
            return dictionary.FirstOrDefault(w => w.CompareWord(word));
        }
    }
}

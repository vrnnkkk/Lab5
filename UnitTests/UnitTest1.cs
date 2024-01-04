using Lab5_test2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class WordViewModelTests
    {
        [TestMethod]
        public void SearchWord_PopulatesTreeData()
        {
            var viewModel = new WordViewModel();
            viewModel.NewWordText = "pre-[root]-suf-ending";
            viewModel.SearchText = "[root]";
            
            viewModel.AddWord();
            viewModel.SearchWord();

            Assert.AreEqual(1, viewModel.TreeData.Count);
            Assert.IsTrue(viewModel.TreeData.Any(word => word.Root.Contains("[root]")));
        }

        [TestMethod]
        public void AddWord_AddsToDictionaryAndClearsNewWordText()
        {
            var viewModel = new WordViewModel();
            viewModel.NewWordText = "pre-[root]-suf-ending";

            viewModel.AddWord();

            Assert.AreEqual(1, viewModel.dictionary.Words.Count()); 
            Assert.AreEqual(string.Empty, viewModel.NewWordText);
        }

        [TestMethod]
        public void AddWord_ShowsMessageBoxOnInvalidWordFormat()
        {
            var viewModel = new WordViewModel();
            viewModel.NewWordText = "invalid-word-format";

            viewModel.AddWord();
        }

        [TestMethod]
        public void ParseWord_ReturnsValidWord()
        {
            var viewModel = new WordViewModel();
            string input = "pre-[root]-suf-kon";

            var result = viewModel.ParseWord(input);

            Assert.IsNotNull(result);
            Assert.AreEqual("pre", result.Prefix);
            Assert.AreEqual("[root]", result.Root);
            Assert.AreEqual("suf", result.Suffix);
            Assert.AreEqual("kon", result.Ending);
        }

        [TestMethod]
        public void ParseWord_ReturnsNullOnInvalidInput()
        {
            var viewModel = new WordViewModel();
            string input = "invalid-input";

            var result = viewModel.ParseWord(input);

            Assert.IsNull(result);
        }
    }
}
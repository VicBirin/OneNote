using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OneNote.Sample.Api.Tests
{
    [TestClass]
    public class NoteboolTests
    {
        [TestMethod]
        public void GetAllNotebooks()
        {
            var factory = new GraphNotebookFactory();
            var list = factory.GetAllItems();

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod]
        public void GetNotebook()
        {
            var id = "0-3AE547978144BF51!117";

            var factory = new GraphNotebookFactory();
            var notebook = factory.GetItem(id);

            Assert.IsNotNull(notebook);
            Assert.IsTrue(notebook.Id.Equals(id));
        }

        [TestMethod]
        public void CreateNotebook()
        {
            var notebookInst = new Notebook
            {
                DisplayName = "New Notebook",
            };

            var factory = new GraphNotebookFactory();
            var notebook = factory.AddItem(notebookInst);

            Assert.IsNotNull(notebook.Id);
            Assert.IsTrue(notebook.DisplayName.Equals(notebookInst.DisplayName));
        }
    }
}

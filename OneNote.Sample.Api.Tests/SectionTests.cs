using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OneNote.Sample.Api.Tests
{
    [TestClass]
    public class SectionTests
    {
        [TestMethod]
        public void GetAllSections()
        {
            var notebookId = "0-3AE547978144BF51!117";
            var factory = new SectionFactory();
            var list = factory.GetAllItems(notebookId);

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod]
        public void GetSection()
        {
            var id = "0-3AE547978144BF51!131";

            var factory = new SectionFactory();
            var section = factory.GetItem(id);

            Assert.IsNotNull(section);
            Assert.IsTrue(section.Id.Equals(id));
        }

        [TestMethod]
        public void CreateSection()
        {
            var notebookId = "0-3AE547978144BF51!117";

            var sectionInst = new Document
            {
                DisplayName = "New Notebook Section",
            };

            var factory = new SectionFactory();
            var notebook = factory.AddItem(sectionInst, notebookId);

            Assert.IsNotNull(notebook.Id);
            Assert.IsTrue(notebook.DisplayName.Equals(sectionInst.DisplayName));
        }
    }
}

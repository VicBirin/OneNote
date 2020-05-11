using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace OneNote.Sample.Api.Tests
{
    [TestClass]
    public class PageTests
    {
        [TestMethod]
        public void GetAllPages()
        {
            var sectionId = "0-3AE547978144BF51!131";
            var factory = new GraphPageFactory();
            var list = factory.GetAllItems(sectionId);

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod]
        public void GetPage()
        {
            var id = "0-d17714e498b540ae8bc5656b55049fc3!145-3AE547978144BF51!147";

            var factory = new GraphPageFactory();
            var page = factory.GetItem(id);

            Assert.IsNotNull(page);
            Assert.IsTrue(page.Id.Equals(id));
        }

        [TestMethod]
        public void CreatePage()
        {
            var sectionId = "0-3AE547978144BF51!131";
            var contentString =
            @"<html lang=""en-US"">
	            <head>
		            <title>Microsoft Graph API example</title>
		            <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />
		            <meta name=""created"" content=""2020-04-14T17:15:00.0000000"" />
	            </head>
	            <body data-absolute-enabled=""true"" style=""font-family:Calibri;font-size:11pt"" />
            </html>";

            var pageInst = new Page(ElementType.Page);
            pageInst.Title = "Microsoft Graph API example";
            //pageInst.Source.LoadHtml(contentString);

            var factory = new GraphPageFactory();
            var page = factory.AddItem(pageInst, sectionId);

            Assert.IsNotNull(page.Id);
            Assert.IsTrue(page.Title.Equals(pageInst.Title));
        }
    }
}

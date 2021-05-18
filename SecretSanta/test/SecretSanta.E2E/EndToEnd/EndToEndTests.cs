using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using PlaywrightSharp;
using System.Linq;

namespace SecretSanta.Web.Tests{
    [TestClass]
    public class EndToEndTests{

        private static WebHostServerFixture<SecretSanta.Web.Startup, SecretSanta.Api.Startup> Server;

        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {
            Server = new();
        }

        [TestMethod]
        public async Task LaunchHomepage(){
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = false,
                SlowMo = 250
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);

            Assert.IsTrue(response.Ok);

            var Header = await page.GetTextContentAsync("body > header > div > a");
            Assert.AreEqual("SecretSanta", Header);
        }

        [TestMethod]
        public async Task LaunchUsers(){
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                SlowMo = 250
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);

            Assert.IsTrue(response.Ok);
            await page.ClickAsync("text=Users");

            var text = await page.GetTextContentAsync("body > section > section > a > section > div");
            Assert.AreEqual("Inigo Montoya", text);
        }

        [TestMethod]
        public async Task LaunchGroups(){
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                SlowMo = 250
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);

            await page.ClickAsync("text=Groups");

            Assert.IsTrue(response.Ok);


            var text = await page.GetTextContentAsync("body > section > section > a > section > div");
            Assert.AreEqual("GoonSquad", text);
        }


        [TestMethod]
        public async Task LaunchGifts(){
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                SlowMo = 250
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);

            await page.ClickAsync("text=Gifts");

            Assert.IsTrue(response.Ok);


            var text = await page.GetTextContentAsync("body > section > section > a > section > div");
            Assert.AreEqual("Rat Poison", text);
        }

        [TestMethod]
        public async Task AddGift(){
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                SlowMo = 250
            });



            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);
            await page.ClickAsync("text=Gifts");

            var gifts = await page.QuerySelectorAllAsync("body > section > section");
            var giftsbefore = gifts.Count();

            await page.ClickAsync("text=create");

            await page.TypeAsync("input#Title", "Test Object");
            await page.TypeAsync("input#Priority", "1");
            await page.SelectOptionAsync("select#UserId", "2");
            await page.ClickAsync("text=Create");

            Assert.IsTrue(response.Ok);

            gifts = await page.QuerySelectorAllAsync("body > section > section");
            var giftsafter = gifts.Count();
            Assert.AreEqual(giftsbefore+1, giftsafter);
        }

        [TestMethod]
        public async Task DeleteGift(){
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                SlowMo = 250
            });



            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);
            
            await page.ClickAsync("text=Gifts");

            Assert.IsTrue(response.Ok);

            var gifts = await page.QuerySelectorAllAsync("body > section > section");
            var giftsbefore = gifts.Count();

            page.Dialog += (_, args) => args.Dialog.AcceptAsync();

            await page.ClickAsync("body > section > section:last-child > a > section > form > button");
            

            gifts = await page.QuerySelectorAllAsync("body > section > section");
            var giftsafter = gifts.Count();
            Assert.AreEqual(giftsbefore, giftsafter+1);
        }

        [TestMethod]
        public async Task ModifyGift(){
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                SlowMo = 250
            });



            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);
            
            await page.ClickAsync("text=Gifts");

            Assert.IsTrue(response.Ok);

            var sectionText = await page.GetTextContentAsync("body > section  > section:last-child > a > section > div");


            await page.ClickAsync("body > section > section:last-child");

            await page.ClickAsync("input#Title", clickCount:3); // Select all text in the text box
            await page.TypeAsync("input#Title", "Updated Gift");
            await page.ClickAsync("text=Update");

            sectionText = await page.GetTextContentAsync("body > section > section:last-child > a > section > div");
            Assert.AreEqual("Updated Gift", sectionText);
        }
    }
}
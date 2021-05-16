using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using PlaywrightSharp;

namespace SecretSanta.Web.Tests{
    [TestClass]
    public class EndToEndTests{

        [TestMethod]
        public async Task LaunchHomepage(){
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = false,
                SlowMo = 250
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync("https://localhost:5001");

            Assert.IsTrue(response.Ok);
        }
    }
}
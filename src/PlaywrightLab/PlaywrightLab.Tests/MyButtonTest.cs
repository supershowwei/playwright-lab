using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;

namespace PlaywrightLab.Tests
{
    public class MyButtonTest
    {
        private IBrowserContext context;

        [SetUp]
        public async Task ContextSetUp()
        {
            this.context = await BrowserSetUp.Browser.NewContextAsync();
        }

        [Test]
        public async Task H1TextShouldBeChanged()
        {
            var page = await this.context.NewPageAsync();

            await page.GotoAsync("http://localhost:5000/");

            await page.ClickAsync("text=MyButton");

            var h1Text = await page.InnerTextAsync("h1");
            
            Assert.AreEqual("My Button Clicked!", h1Text);
        }

        [Test]
        public async Task UserAgentShouldContainsChrome95()
        {
            var page = await this.context.NewPageAsync();

            await page.GotoAsync("http://localhost:5000/");

            JsonElement? jsonObj;

            jsonObj = await page.EvaluateAsync("() => ({ userAgent: window.navigator.userAgent })");

            Assert.IsTrue(jsonObj.Value.GetProperty("userAgent").GetString().Contains("Chrome/95"));

            jsonObj = await page.EvaluateAsync("() => Promise.resolve({ userAgent: window.navigator.userAgent })");

            Assert.IsTrue(jsonObj.Value.GetProperty("userAgent").GetString().Contains("Chrome/95"));

            jsonObj = await page.EvaluateAsync("async () => await Promise.resolve({ userAgent: window.navigator.userAgent })");

            Assert.IsTrue(jsonObj.Value.GetProperty("userAgent").GetString().Contains("Chrome/95"));
        }

        [TearDown]
        public async Task ContextTearDown()
        {
            await this.context.CloseAsync();
        }
    }

    [SetUpFixture]
    public class BrowserSetUp
    {
        private IPlaywright playwright;

        public static IBrowser Browser { get; private set; }

        [OneTimeSetUp]
        public async Task SetUp()
        {
            this.playwright = await Playwright.CreateAsync();

            Browser = await this.playwright.Chromium.LaunchAsync(
                          new BrowserTypeLaunchOptions
                          {
                              ExecutablePath = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", Headless = false
                          });
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            await Browser.CloseAsync();
        }
    }
}
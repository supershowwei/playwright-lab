using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PlaywrightLab
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var playwright = await Playwright.CreateAsync();

            await using var browser = await playwright.Chromium.LaunchAsync(
                                          new BrowserTypeLaunchOptions
                                          {
                                              ExecutablePath = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe",
                                              Headless = false
                                          });

            var context = await browser.NewContextAsync(new BrowserNewContextOptions
                                                        {
                                                            
                                                        });

            var page = await browser.NewPageAsync();

            await page.GotoAsync("http://localhost:5000");

            await page.ClickAsync("#mybutton");

            var h1Text = await page.InnerTextAsync("h1");

            // Should be True.
            var result = h1Text.Equals("My Button Clicked!");
        }
    }
}
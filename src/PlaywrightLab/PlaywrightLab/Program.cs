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
            //await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            //{
            //    ExecutablePath = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe",
            //    Channel = "chrome",
            //    Headless = false
            //});
            await using var browser = await playwright.Chromium.LaunchPersistentContextAsync(
                @"C:\Users\wantgoo\AppData\Local\Google\Chrome\User Data\Profile 2"
                , new BrowserTypeLaunchPersistentContextOptions
                {
                    ExecutablePath = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe",
                    Channel = "chrome",
                    Headless = false
                });

            var page = await browser.NewPageAsync();
            await page.GotoAsync("https://playwright.dev/dotnet");

            

            await page.WaitForTimeoutAsync(10000);
            var a = await page.ScreenshotAsync(new PageScreenshotOptions { Path = "screenshot.png" });


            await File.WriteAllBytesAsync(@"D:\test.png", a);

            //Console.Read();
        }
    }
}
using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Core.Test.Helpers;
using Gallio.Framework;
using Gallio.Model;
using MbUnit.Framework;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;


namespace Core.Test
{
    [TestFixture]
    //[Header("browser", "version", "platform")] // name of the parameters in the rows
    //[Row("internet explorer", "11", "Windows 7")] // run all tests in the fixture against IE 11 for windows 7
    //[Row("chrome", "35", "Windows 7")] // run all tests in the fixture against chrome 35 for linux
    //[Row("safari", "6", "OS X 10.8")] // run all tests in the fixture against safari 6 and mac OS X 10.8
    //[Row("firefox", "37", "Windows 7")] // run all tests in the fixture against safari 6 and mac OS X 10.8
    public class TestBase
    {
        public TestBase()
        {
            
        }

        private IWebDriver _WebDriver;

        protected IWebDriver WebDriver
        {
            get
            {
                return _WebDriver;
            }
        }

        [BeforeScenario]
        public void beforeScenario()
        {
            _WebDriver = new OpenQA.Selenium.Firefox.FirefoxDriver();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _WebDriver.Quit();
        }

        public void _Setup(string browser, string version, string platform)
        {
            // construct the url to sauce labs
            Uri commandExecutorUri = new Uri("http://ondemand.saucelabs.com/wd/hub");

            // set up the desired capabilities
            DesiredCapabilities desiredCapabilites = new DesiredCapabilities(browser, version, Platform.CurrentPlatform); // set the desired browser
            desiredCapabilites.SetCapability("platform", platform); // operating system to use
            desiredCapabilites.SetCapability("username", "ashwini_ananthaswamy"); // supply sauce labs username
            desiredCapabilites.SetCapability("accessKey", "10e262be-0191-4e44-aaef-8ad04934365a");  // supply sauce labs account key
            desiredCapabilites.SetCapability("name", TestContext.CurrentContext.Test.Name); // give the test a name

            // start a new remote web driver session on sauce labs
            var _Driver = new RemoteWebDriver(commandExecutorUri, desiredCapabilites);
            _Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));

            // navigate to the page under test
            //_Driver.Navigate().GoToUrl("https://saucelabs.com/test/guinea-pig");

            _WebDriver = _Driver;
        }

        public void CleanUp()
        {
            // get the status of the current test
            bool passed = TestContext.CurrentContext.Outcome.Status == TestStatus.Passed;
            try
            {
                // log the result to sauce labs
                ((IJavaScriptExecutor)_WebDriver).ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
            }
            finally
            {
                // terminate the remote webdriver session
                _WebDriver.Quit();
            }
        }

    }
}

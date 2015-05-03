using System;
using OpenQA.Selenium;
using Core.WebDriverExtensions;
using OpenQA.Selenium.Support.UI;

namespace Core.Page
{
    public abstract class VerifiedPage : Page
    {
        public VerifiedPage(IWebDriver WebDriver)
            : base(WebDriver)
        {
            //The timeout can be stored in an xml file(TODO)
            WebDriver.WaitForPageToLoad(20);
            //VerifyURL();
        }

        public WebDriverWait Wait
        {
            get
            {
                return new WebDriverWait(WebDriver, new TimeSpan(0, 0, 10));
            }
        }



        //public abstract bool URLVerified
        //{
        //    get;
        //}

        //public abstract string PageKey
        //{
        //    get;
        //}

        //protected abstract void VerifyURL();
    }
}

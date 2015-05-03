using OpenQA.Selenium;

namespace Core.Page
{
    public abstract class Page
    {
        private IWebDriver _WebDriver;

        public Page(IWebDriver WebDriver)
        {
            _WebDriver = WebDriver;
        }

        protected IWebDriver WebDriver
        {
            get
            {
                return _WebDriver;
            }
        }
    }
}

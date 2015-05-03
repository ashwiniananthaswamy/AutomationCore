using Core;
using OpenQA.Selenium;

namespace Core.Module
{
    public abstract class Module<TPage> where TPage : Page.Page
    {
        private IWebDriver _WebDriver;
        private TPage _Page;

        public Module(IWebDriver WebDriver, TPage Page)
        {
            _WebDriver = WebDriver;
            _Page = Page;
        }

        protected IWebDriver WebDriver
        {
            get
            {
                return _WebDriver;
            }
        }

        protected TPage Page
        {
            get
            {
                return _Page;
            }
        }
    }
}

using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace Core.WebDriverExtensions
{

    public static partial class WebDriverExtensions
    {
        public static void WaitForPageToLoad(this IWebDriver Driver, int SecondsToWait)
        {
            string state = string.Empty;
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(SecondsToWait));
                wait.Until(d =>
                {
                    try
                    {
                        state = ((IJavaScriptExecutor)Driver).ExecuteScript(@"return document.readyState").ToString();
                    }
                    catch (Exception ex)
                    {
                        //ignore
                    }
                    return (state.Equals("complete", StringComparison.InvariantCultureIgnoreCase));
                });
            }
            catch (TimeoutException)
            {
                //sometimes Page remains in Interactive mode and never becomes Complete, then we can still try to access the controls
                if (!state.Equals("interactive", StringComparison.InvariantCultureIgnoreCase))
                    throw;
            }
            catch (NullReferenceException)
            {
                //sometimes Page remains in Interactive mode and never becomes Complete, then we can still try to access the controls
                if (!state.Equals("interactive", StringComparison.InvariantCultureIgnoreCase))
                    throw;
            }
            catch (WebDriverException)
            {
                if (Driver.WindowHandles.Count == 1)
                {
                    Driver.SwitchTo().Window(Driver.WindowHandles[0]);
                }
                state = ((IJavaScriptExecutor)Driver).ExecuteScript(@"return document.readyState").ToString();
                if (!(state.Equals("complete", StringComparison.InvariantCultureIgnoreCase)))
                    throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

}

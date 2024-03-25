using AutomationExerciseProject.Communication;
using OpenQA.Selenium;

namespace AutomationExerciseProject.Pages
{
    public class HomePage : WebPage
    {
        public HomePage(DriverAdapter driver) 
            : base(driver)
        {
        }

        public IWebElement SignUpAndLoginBtn => Driver.FindElement(By.CssSelector(".fa-lock"));
        public IWebElement LogoutBtn => Driver.FindElement(By.CssSelector("#header > div > div > div > div.col-sm-8 > div > ul > li:nth-child(4) > a"));
        public IWebElement DeleteBtn => Driver.FindElement(By.CssSelector("#header > div > div > div > div.col-sm-8 > div > ul > li:nth-child(5) > a"));
        public IWebElement UserInfo => Driver.FindElement(By.CssSelector("#header > div > div > div > div.col-sm-8 > div > ul > li:nth-child(10) > a > i"));
        public void ClickSignUp()
        {
            SignUpAndLoginBtn.Click();
        }
    }
}

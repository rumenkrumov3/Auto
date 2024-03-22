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

        public IWebElement SignUpButton => Driver.FindElement(By.CssSelector(".fa-lock"));

        public void ClickSignUp()
        {
            SignUpButton.Click();
        }
    }
}

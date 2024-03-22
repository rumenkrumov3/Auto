using AutomationExerciseProject.Communication;
using AutomationExerciseProject.Factory;
using AutomationExerciseProject.Models;
using OpenQA.Selenium;

namespace AutomationExerciseProject.Pages
{
    public class SingUpShortPage : WebPage
    {
        public SingUpShortPage(DriverAdapter driver)
            : base(driver)
        {
        }

        public IWebElement FieldName => Driver.FindElement(By.CssSelector("#form > div > div > div:nth-child(3) > div > form > input[type=text]:nth-child(2)"));

        public IWebElement EmailAddress => Driver.FindElement(By.CssSelector(".signup-form > form:nth-child(2) > input:nth-child(3)"));

        public IWebElement SignupButton => Driver.FindElement(By.CssSelector("button.btn:nth-child(5)"));

        public void TypeCredentialsAndSubmit(UserDetailsShort userShortDetails)
        {
            FieldName.SendKeys(userShortDetails.Name);
            EmailAddress.SendKeys(userShortDetails.Email);
            SignupButton.Click();
        }
    }    
}

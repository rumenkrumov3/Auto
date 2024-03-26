using AutomationExerciseProject.Communication;
using AutomationExerciseProject.Factory;
using AutomationExerciseProject.Models;
using OpenQA.Selenium;

namespace AutomationExerciseProject.Pages
{
    public class SingUpAndLoginPage : WebPage
    {
        public SingUpAndLoginPage(DriverAdapter driver)
            : base(driver)
        {
        }

        public IWebElement FieldName => Driver.FindElement(By.CssSelector("#form > div > div > div:nth-child(3) > div > form > input[type=text]:nth-child(2)"));
        public IWebElement LoginEmailAddress => Driver.FindElement(By.CssSelector("#form > div > div > div.col-sm-4.col-sm-offset-1 > div > form > input[type=email]:nth-child(2)"));
        public IWebElement EmailAddress => Driver.FindElement(By.CssSelector(".signup-form > form:nth-child(2) > input:nth-child(3)"));
        public IWebElement LoginPassword => Driver.FindElement(By.CssSelector("#form > div > div > div.col-sm-4.col-sm-offset-1 > div > form > input[type=password]:nth-child(3)"));
        public IWebElement SignupButton => Driver.FindElement(By.CssSelector("button.btn:nth-child(5)"));
        public IWebElement WarningMsgSignup => Driver.FindElement(By.CssSelector("#form > div > div > div:nth-child(3) > div > form > p"));
        public IWebElement WarningMsgLoggin => Driver.FindElement(By.CssSelector(".login-form > form:nth-child(2) > p:nth-child(4)"));
        public IWebElement LoginBtn => Driver.FindElement(By.CssSelector("button.btn:nth-child(4)"));

        public void TypeCredentialsAndSubmit(UserDetailsShort userShortDetails)
        {
            FieldName.SendKeys(userShortDetails.Name);
            EmailAddress.SendKeys(userShortDetails.Email);
            SignupButton.Click();
        }
    }    
}

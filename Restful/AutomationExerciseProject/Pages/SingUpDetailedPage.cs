using AutomationExerciseProject.Communication;
using AutomationExerciseProject.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationExerciseProject.Pages
{
    public class SingUpDetailedPage : WebPage
    {
        public SingUpDetailedPage(DriverAdapter driver)
            : base(driver)
        {
        }

        public IWebElement Male => Driver.FindElement(By.Id("id_gender1"));
        public IWebElement Female => Driver.FindElement(By.Id("id_gender2"));
        public IWebElement Name => Driver.FindElement(By.Id("name"));
        public IWebElement Email => Driver.FindElement(By.Id("email"));
        public IWebElement Password => Driver.FindElement(By.Id("password"));
        public IWebElement Days => Driver.FindElement(By.Id("days"));
        public IWebElement Months => Driver.FindElement(By.Id("months"));
        public IWebElement Years => Driver.FindElement(By.Id("years"));
        public IWebElement FirstName => Driver.FindElement(By.Id("first_name"));
        public IWebElement LastName => Driver.FindElement(By.Id("last_name"));
        public IWebElement Company => Driver.FindElement(By.Id("company"));
        public IWebElement Address1 => Driver.FindElement(By.Id("address1"));
        public IWebElement Country => Driver.FindElement(By.Id("country"));
        public IWebElement State => Driver.FindElement(By.Id("state"));
        public IWebElement City => Driver.FindElement(By.Id("city"));
        public IWebElement ZipCode => Driver.FindElement(By.Id("zipcode"));
        public IWebElement MobileNumber => Driver.FindElement(By.Id("mobile_number"));
        public IWebElement CreateAccBtn => Driver.FindElement(By.CssSelector("button.btn:nth-child(22)"));
        public IWebElement AccountCreated => Driver.FindElement(By.CssSelector(".title"));

        public void FillUserFormAndSubmit(UserDetails userDetails)
        {
            var days = new SelectElement(Days);
            var months = new SelectElement(Months);
            var years = new SelectElement(Years);

            if (userDetails.IsMale)
            {
                Male.Click();
            }
            else
            {
                Female.Click();
            }
            Password.SendKeys(userDetails.Password);
            days.SelectByValue(userDetails.Days.ToString());
            months.SelectByValue(userDetails.Months.ToString());
            years.SelectByValue(userDetails.Years.ToString());
            FirstName.SendKeys(userDetails.FirstName);
            LastName.SendKeys(userDetails.LastName);
            Company.SendKeys(userDetails.Company);
            Address1.SendKeys(userDetails.Address);           
            Country.SendKeys(userDetails.Company);
            State.SendKeys(userDetails.State);
            City.SendKeys(userDetails.City);
            ZipCode.SendKeys(userDetails.ZipCode.ToString());
            MobileNumber.SendKeys(userDetails.Number.ToString());

            Driver.ExecuteScript("arguments[0].scrollIntoView(true);", CreateAccBtn);          
            CreateAccBtn.Click();
        }
    }
}

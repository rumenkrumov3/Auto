using AutomationExerciseProject.Communication;
using AutomationExerciseProject.Factory;
using AutomationExerciseProject.Helpers;
using AutomationExerciseProject.Models;
using AutomationExerciseProject.Pages;
using FluentAssertions;

namespace AutomationExerciseProject.Tests
{
    public class LoginTests
    {
        private DriverAdapter _driverAdapter;
        private HomePage _homePage;
        private SingUpAndLoginPage _signUpAndLoginPage;
        private SingUpDetailedPage _signUpDetailedPage;

        [SetUp]
        public void Setup()
        {
            _driverAdapter = new DriverAdapter();
            _driverAdapter.Start(Browser.Firefox);

            _driverAdapter.GoToUrl("https://automationexercise.com/");
            _driverAdapter.DeleteAllCookies();

            _homePage = new HomePage(_driverAdapter);
            _signUpAndLoginPage = new SingUpAndLoginPage(_driverAdapter);
            _signUpDetailedPage = new SingUpDetailedPage(_driverAdapter);
        }

        [Test]
        public void LoginWithValidCredentials()
        {
            var userShortDetails = new UserDetailsFactory().GenerateUserShortDetails();
            var userDetails = new UserDetailsFactory().GenerateUserDetails();
            SignUpHelper.SignUp(_homePage, _signUpAndLoginPage, _signUpDetailedPage, userShortDetails, userDetails);

            _signUpDetailedPage.ContinueBtn.Click();
            _homePage.LogoutBtn.Click();
            _homePage.SignUpAndLoginBtn.Click();
            _signUpAndLoginPage.LoginEmailAddress.SendKeys(userShortDetails.Email);
            _signUpAndLoginPage.LoginPassword.SendKeys(userDetails.Password);
            _signUpAndLoginPage.LoginBtn.Click();

            _homePage.UserInfo.Text.Should().Be(String.Concat("Logged in as ",userShortDetails.Name));
        }

        [Test]
        public void LoginWithInvalidCredentials()
        {
            var userShortDetails = new UserDetailsFactory().GenerateUserShortDetails();
            var userDetails = new UserDetailsFactory().GenerateUserDetails();

            _homePage.SignUpAndLoginBtn.Click();
            _signUpAndLoginPage.LoginEmailAddress.SendKeys(userShortDetails.Email);
            _signUpAndLoginPage.LoginPassword.SendKeys(userDetails.Password);
            _signUpAndLoginPage.LoginBtn.Click();

            _signUpAndLoginPage.WarningMsgLoggin.Text.Should().Be("Your email or password is incorrect!");
        }

        [TearDown]
        public void TearDown()
        {
            _driverAdapter.Quit();
        }
    }
}

using AutomationExerciseProject.Communication;
using AutomationExerciseProject.Factory;
using AutomationExerciseProject.Helpers;
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
            _driverAdapter.Start(Browser.Chrome);

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

            _homePage.UserInfo.Text.Should().Be(userShortDetails.Name);
        }
    }
}

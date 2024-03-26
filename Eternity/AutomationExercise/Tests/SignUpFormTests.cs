using AutomationExerciseProject.Communication;
using AutomationExerciseProject.Factory;
using AutomationExerciseProject.Helpers;
using AutomationExerciseProject.Pages;
using FluentAssertions;

namespace AutomationExerciseProject.Tests
{
    public class Tests
    {
        private DriverAdapter _driverAdapter;
        private HomePage _homePage;
        private SingUpAndLoginPage _signUpPage;
        private SingUpDetailedPage _signUpDetailedPage;

        [SetUp]
        public void Setup()
        {
            _driverAdapter = new DriverAdapter();
            _driverAdapter.Start(Browser.Firefox);

            _driverAdapter.GoToUrl("https://automationexercise.com/");
            _driverAdapter.DeleteAllCookies();

            _homePage = new HomePage(_driverAdapter);
            _signUpPage = new SingUpAndLoginPage(_driverAdapter);
            _signUpDetailedPage = new SingUpDetailedPage(_driverAdapter);
        }

        [Test]
        public void SignUpWithValidCredentials()
        {
            var userShortDetails = new UserDetailsFactory().GenerateUserShortDetails();
            var userDetails = new UserDetailsFactory().GenerateUserDetails();

            SignUpHelper.SignUp(_homePage, _signUpPage, _signUpDetailedPage, userShortDetails, userDetails);

            _signUpDetailedPage.AccountTitle.Text.Should().Be("ACCOUNT CREATED!");
        }

        [Test]
        public void SignUpWithValidCredentialsButExistingEmail()
        {
            var userShortDetails = new UserDetailsFactory().GenerateUserShortDetails();
            var userDetails = new UserDetailsFactory().GenerateUserDetails();
            SignUpHelper.SignUp(_homePage, _signUpPage, _signUpDetailedPage, userShortDetails, userDetails);

            _signUpDetailedPage.ContinueBtn.Click();
            _homePage.LogoutBtn.Click();

            _signUpPage.FieldName.SendKeys(userShortDetails.Name);
            _signUpPage.EmailAddress.SendKeys(userShortDetails.Email);
            _signUpPage.SignupButton.Click();

            _signUpPage.WarningMsgSignup.Text.Should().Be("Email Address already exist!");
        }

        [Test]
        public void SignUpAndDeleteTheAccount()
        {
            var userShortDetails = new UserDetailsFactory().GenerateUserShortDetails();
            var userDetails = new UserDetailsFactory().GenerateUserDetails();
            SignUpHelper.SignUp(_homePage, _signUpPage, _signUpDetailedPage, userShortDetails, userDetails);

            _signUpDetailedPage.ContinueBtn.Click();
            _homePage.DeleteBtn.Click();

            _signUpDetailedPage.AccountTitle.Text.Should().Be("ACCOUNT DELETED!");

        }
        [Test]
        public void NameAndEmailShouldBeFilledAfterPassingTheShortSignUpForm()
        {
            var userShortDetails = new UserDetailsFactory().GenerateUserShortDetails();
            SignUpHelper.SignUpShort(_homePage, _signUpPage, _signUpDetailedPage, userShortDetails);

            _signUpDetailedPage.Name.GetAttribute("value").Should().Be(userShortDetails.Name);
            _signUpDetailedPage.Email.GetAttribute("value").Should().Be(userShortDetails.Email);
        }
        [TearDown]
        public void TearDown()
        {
            _driverAdapter.Quit();
        }
    }
}
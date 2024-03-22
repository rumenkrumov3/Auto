using AutomationExerciseProject.Communication;
using AutomationExerciseProject.Factory;
using AutomationExerciseProject.Pages;

namespace AutomationExerciseProject
{
    public class Tests
    {
        private DriverAdapter _driverAdapter;
        private HomePage _homePage;
        private SingUpShortPage _signUpPage;
        private SingUpDetailedPage _signUpDetailedPage;

        [SetUp]
        public void Setup()
        {
            _driverAdapter = new DriverAdapter();
            _driverAdapter.Start(Browser.Firefox);

            _driverAdapter.GoToUrl("https://automationexercise.com/");
            _driverAdapter.DeleteAllCookies();

            _homePage = new HomePage(_driverAdapter);
            _signUpPage = new SingUpShortPage(_driverAdapter);
            _signUpDetailedPage = new SingUpDetailedPage(_driverAdapter);
        }

        [Test]
        public void SignUpWithValidCredentials()
        {
            _homePage.ClickSignUp();

            var userShortDetails = new UserDetailsFactory().GenerateUserShortDetails();
            _signUpPage.TypeCredentialsAndSubmit(userShortDetails);

            var userDetails = new UserDetailsFactory().GenerateUserDetails();
            _signUpDetailedPage.FillUserFormAndSubmit(userDetails);

            Assert.That(_signUpDetailedPage.AccountCreated.Text == "ACCOUNT CREATED!");
        }

        [TearDown]
        public void TearDown()
        {
            _driverAdapter.Quit();
        }
    }
}
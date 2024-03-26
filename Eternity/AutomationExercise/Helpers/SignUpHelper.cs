using AutomationExerciseProject.Communication;
using AutomationExerciseProject.Factory;
using AutomationExerciseProject.Models;
using AutomationExerciseProject.Pages;

namespace AutomationExerciseProject.Helpers
{
    public static class SignUpHelper
    {
        public static void SignUp(HomePage _homePage, SingUpAndLoginPage _signUpPage, SingUpDetailedPage _signUpDetailedPage, UserDetailsShort userShortDetails, UserDetails userDetails)
        {
            _homePage.ClickSignUp();

            _signUpPage.TypeCredentialsAndSubmit(userShortDetails);

            _signUpDetailedPage.FillUserFormAndSubmit(userDetails);
        }

        public static void SignUpShort(HomePage _homePage, SingUpAndLoginPage _signUpPage, SingUpDetailedPage _signUpDetailedPage, UserDetailsShort userShortDetails)
        {
            _homePage.ClickSignUp();

            _signUpPage.TypeCredentialsAndSubmit(userShortDetails);
        }
    }
}

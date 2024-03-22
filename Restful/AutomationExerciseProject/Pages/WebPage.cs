using AutomationExerciseProject.Communication;

namespace AutomationExerciseProject.Pages
{
    public class WebPage
    {
        protected readonly DriverAdapter Driver;

        public WebPage(DriverAdapter driver)
        {
                Driver = driver;
        }
    }
}

using AutomationExerciseProject.Models;
using Bogus;

namespace AutomationExerciseProject.Factory
{
    public class UserDetailsFactory
    {
        private Faker<UserDetails> _userDetailsFaker;
        private Faker<UserDetailsShort> _userDetailsShortFaker;

        public UserDetailsFactory()
        {
            _userDetailsFaker = UserDetailsFaker();
            _userDetailsShortFaker = UserDetailsShortFaker();
        }

        public UserDetails GenerateUserDetails()
            => _userDetailsFaker.Generate();

        public UserDetailsShort GenerateUserShortDetails()
            => _userDetailsShortFaker.Generate();

        public Faker<UserDetails> UserDetailsFaker()
        {
            var faker = new Faker<UserDetails>()
                        .RuleFor(x => x.IsMale, y => y.Random.Bool())
                        .RuleFor(x => x.Password, y => y.Random.String())
                        .RuleFor(x => x.Days, y => y.Random.Int(1, 30))
                        .RuleFor(x => x.Months, y => y.Random.Int(1, 12))
                        .RuleFor(x => x.Years, y => y.Random.Int(1900, 2021))
                        .RuleFor(x => x.Address, y => y.Random.Word())
                        .RuleFor(x => x.FirstName, y => y.Random.Word())
                        .RuleFor(x => x.LastName, y => y.Random.Word())
                        .RuleFor(x => x.Company, y => y.Random.Word())
                        .RuleFor(x => x.State, y => "Bulgaria")
                        .RuleFor(x => x.State, y => "Bulgaria")
                        .RuleFor(x => x.City, y => y.Random.Word())
                        .RuleFor(x => x.ZipCode, y => y.Random.Int(1000, 9999))
                        .RuleFor(x => x.Number, y => y.Random.Int(1000000,2000000));

            return faker;
        }

        public Faker<UserDetailsShort> UserDetailsShortFaker()
        {
            var faker = new Faker<UserDetailsShort>()
                        .RuleFor(x => x.Name, y => y.Random.Word())
                        .RuleFor(x => x.Email, y => string.Concat("Rumen", y.Random.Int(), "@abv.bg"));
                        

            return faker;
        }
    }
}

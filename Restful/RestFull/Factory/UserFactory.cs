using Bogus;
using RestFul.Models;

namespace RestFul.Factory
{
    public class UserFactory
    {
        private Faker<User> _faker;

        public UserFactory()
        {
            _faker = AutoFaker();
        }

        public User Generate()
            => _faker.Generate();
        public Faker<User> AutoFaker()
        {
            var faker = new Faker<User>()
                        .RuleFor(x => x.Name, y => y.Random.Word())
                        .RuleFor(x => x.Job, y => y.Random.Word());

            return faker;
        }
    }
}

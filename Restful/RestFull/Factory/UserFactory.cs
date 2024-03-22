using Bogus;
using RestFul.Models;

namespace RestFul.Factory
{
    public class UserFactory
    {
        private Faker<UserPost> _faker;

        public UserFactory()
        {
            _faker = AutoFaker();
        }

        public UserPost Generate()
            => _faker.Generate();

        public Faker<UserPost> AutoFaker()
        {
            var faker = new Faker<UserPost>()
                        .RuleFor(x => x.Name, y => y.Random.Word())
                        .RuleFor(x => x.Job, y => y.Random.Word());

            return faker;
        }
    }
}

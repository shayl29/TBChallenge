namespace TenBisChallenge.Migrations
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using TenBisChallenge.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TenBisChallenge.Models.DBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TenBisChallenge.Models.DBContext context)
        {
            //  This method will be called after migrating to the latest version.
            SeedContext(context);

            context.SaveChanges();
        }

        private void SeedContext(DBContext context)
        {
            var companies = GetResource<Company>(@"C:\Users\shayl\source\repos\TenBisChallenge\TenBisChallenge\App_Data\JsonMockData\companies.json");
            context.Companies.AddOrUpdate(companies.ToArray());

            var users = GetResource<User>(@"C:\Users\shayl\source\repos\TenBisChallenge\TenBisChallenge\App_Data\JsonMockData\users.json");
            context.Users.AddOrUpdate(users.ToArray());

            var moneyCards = GetResource<MoneyCard>(@"C:\Users\shayl\source\repos\TenBisChallenge\TenBisChallenge\App_Data\JsonMockData\moneyCards.json");
            context.MoneyCards.AddOrUpdate(moneyCards.ToArray());

            var banks = GetResource<Bank>(@"C:\Users\shayl\source\repos\TenBisChallenge\TenBisChallenge\App_Data\JsonMockData\banks.json");
            context.Banks.AddOrUpdate(banks.ToArray());
        }

        private IEnumerable<T> GetResource<T>(string jsonFilePath)
        {
            var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" };
            return JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(jsonFilePath), dateTimeConverter);
        }
    }
}

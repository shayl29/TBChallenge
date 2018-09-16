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
    using System.Reflection;
    using System.Web;
    using System.Web.Hosting;
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
            var companies = GetResource<Company>(MapPath("~/App_Data/JsonMockData/companies.json"));
            context.Companies.AddOrUpdate(companies.ToArray());

            var users = GetResource<User>(MapPath("~/App_Data/JsonMockData/users.json"));
            context.Users.AddOrUpdate(users.ToArray());

            var moneyCards = GetResource<MoneyCard>(MapPath("~/App_Data/JsonMockData/moneyCards.json"));
            context.MoneyCards.AddOrUpdate(moneyCards.ToArray());

            var banks = GetResource<Bank>(MapPath("~/App_Data/JsonMockData/banks.json"));
            context.Banks.AddOrUpdate(banks.ToArray());
        }

        private string MapPath(string seedFile)
        {
            if (HttpContext.Current != null)
                return HostingEnvironment.MapPath(seedFile);

            var absolutePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath; //was AbsolutePath but didn't work with spaces according to comments
            var directoryName = Path.GetDirectoryName(absolutePath);
            var path = Path.Combine(directoryName, ".." + seedFile.TrimStart('~').Replace('/', '\\'));

            return path;
        }

        private IEnumerable<T> GetResource<T>(string jsonFilePath)
        {
            var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" };
            return JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(jsonFilePath), dateTimeConverter);
        }
    }
}

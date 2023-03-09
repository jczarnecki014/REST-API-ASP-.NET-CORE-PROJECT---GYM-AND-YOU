using Bogus;
using Bogus.Extensions.Sweden;
using GymAndYou.Entities;
using GymAndYou.StaticData;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace GymAndYou.DatabaseConnection
{
    public class DatabaseSeeder
    {
        private readonly DbConnection _context;


        public DatabaseSeeder(DbConnection context)
        {
            _context = context;
        }

        /// <summary>
        /// If database is empty, genereate and insert entities to database (It sets up default entities in database using BOGUS)
        /// </summary>


        public void SeedDatabase()
        {
            if(_context.Database.CanConnect())
            {
                /* 
                    If exisist pending migrations - update database
                 */
                if(_context.Database.GetPendingMigrations().Any())
                {
                    _context.Database.Migrate();
                }

                /* 
                    If default gyms don't exist - Generate them
                 */

                if(! (_context.Gyms.Any() || _context.Members.Any() || _context.AviableEquipments.Any()) )
                {
                    var entities = CreateEnities();
                    _context.AddRange(entities);
                    _context.SaveChanges();
                }

                /* 
                    If default roles don't exist - Generate them
                 */

                if(!_context.Roles.Any())
                {
                    var roles = CreateRoles();
                    _context.AddRange(roles);
                    _context.SaveChanges();
                }
            }
        }

        private List<Gym> CreateEnities()
        {
           
            Randomizer.Seed = new Random(911);
            var location = "pl";



            var Members = new Faker<Members>(location)
                .RuleFor(a => a.FirstName, f=> f.Person.FirstName)
                .RuleFor(a => a.LastName, f=> f.Person.LastName)
                .RuleFor(a => a.Email, f => f.Person.Phone)
                .RuleFor(a => a.Phone, f => f.Person.Phone)
                .RuleFor(a => a.Pesel, f => f.Person.Personnummer())
                .RuleFor(a => a.BirthDay, f => f.Person.DateOfBirth)
                .RuleFor("Sex",f => f.Person.Gender.ToString())
                .RuleFor("JoinDate", f => f.Date.Recent());



            var AviableEquipment = new Faker<AviableEquipment>(location)
                .RuleFor("Name", f => String.Join(' ',f.Lorem.Words(2)))
                .RuleFor(a => a.Description, f => f.Lorem.Text())
                .RuleFor(a => a.BodyPart, f =>f.Random.ListItem(Static.BodyParts))
                .RuleFor(a => a.MaxWeight, f =>f.Random.Number(100,400));

            var Addresses = new Faker<Address>(location)
                .RuleFor(a => a.City, f=> f.Address.City())
                .RuleFor(a => a.StreetName, f => f.Address.StreetName())
                .RuleFor(a => a.PostalCode, f => f.Address.ZipCode());

            var Gyms = new Faker<Gym>(location)
                .RuleFor(a => a.Name, f=> f.Company.CompanyName())
                .RuleFor(a => a.Description, f => f.Lorem.Paragraph())
                .RuleFor(a => a.OpeningHours, f =>f.Lorem.Word())
                .RuleFor(a => a.Address, f => Addresses.Generate())
                .RuleFor(a => a.Members, f => Members.Generate(20))
                .RuleFor(a => a.AviableEquipments, f => AviableEquipment.Generate(15));

            var GymList = Gyms.Generate(20);

            return GymList;
        }
        private  List<Role> CreateRoles()
        {
            var roles = new List<Role>()
            {
                new Role(){Name = Static.System_Roles_User},
                new Role(){Name = Static.System_Roles_Manager},
                new Role(){Name = Static.System_Roles_Administrator}
            };

            return roles;
        }

    }
}

using System.Collections.Generic;
using System.Linq;
using API.Entities;
using Bogus;

namespace HrSystemTests
{
    public static class PopulateDb
    {
        public static List<AppUser> PopulateUsers(IEnumerable<Department> departments)
        {
            var fakeUser = new Faker<AppUser>()
                .RuleFor(x => x.FName, x => x.Person.FirstName)
                .RuleFor(x => x.LName, x => x.Person.LastName)
                .RuleFor(x => x.Email, x => x.Person.Email)
                .RuleFor(x => x.Address, x => x.Address.FullAddress())
                .RuleFor(x => x.Nationality, x => x.Address.State())
                .RuleFor(x => x.Country, x => x.Address.Country())
                .RuleFor(x => x.PhoneNumber, x => x.Person.Phone)
                .RuleFor(x => x.DateOfBirth, x => x.Person.DateOfBirth)
                .RuleFor(x => x.PictureId, x => x.Rant.Random.Int().ToString())
                .RuleFor(x => x.PictureUrl, x => x.Image.PicsumUrl())
                .RuleFor(x => x.IsAdmin, x => x.Random.Bool())
                .RuleFor(x => x.DaysOffLeft, x => x.Random.Int())
                .RuleFor(x => x.DateOfBirth, x => x.Person.DateOfBirth)
                .Rules( (f, o) =>
                {
                    var dep = f.PickRandom<Department>(departments);
                    var role = f.PickRandom<Role>(dep.DepartmentRoles);
                    o.DepartmentId = dep.Id;
                    o.InDepartment = dep;
                    o.Role = role;
                    o.RoleId = role.Id;
                });

            var users = fakeUser.Generate(5);
            return users;
        }

        private static IEnumerable<Role> GenerateRoles(int departmentId, Faker faker)
        {
            yield return new Role() {RoleName = "Junior", DepartmentId = departmentId};
            yield return new Role() {RoleName = "Mid", DepartmentId = departmentId};
            yield return new Role() {RoleName = "Senior", DepartmentId = departmentId};
        }

        public static List<Department> PopulateDepartments()
        {
            Faker faker = new Faker();
            
            var departments = new Faker<Department>()
                .RuleFor(x => x.Id, x => x.UniqueIndex)
                .RuleFor(x => x.Name, x => x.Commerce.Department())
                .Rules((f, o) =>
                {
                    o.DepartmentRoles = GenerateRoles(o.Id, f).ToList();
                })
                .Generate(3);

            return departments;
        }
    }
}
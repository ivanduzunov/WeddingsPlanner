using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingsPlanner.Models;


namespace WeddingsPlanner.ImportJSON
{
    public class Import
    {
        public static void AddAgencies(IEnumerable<Agency> agencies)
        {

            Console.WriteLine("Importing Agencies! ");

            using (WeddingContext context = new WeddingContext())
            {
                foreach (Agency agency in agencies)
                {
                    Agency newAgency = new Agency
                    {
                        Name = agency.Name,
                        EmployeesCount = agency.EmployeesCount,
                        Town = agency.Town
                    };

                    context.Agencies.Add(newAgency);
                    Console.WriteLine($"Agency {newAgency.Name} from {newAgency.Town} added to the database!");
                }
                context.SaveChanges();
                Console.WriteLine("All agencies added. Press any key..."); Console.ReadKey();
            }
        }
        public static void AddPeople(IEnumerable<PersonDto> people)
        {
            Console.WriteLine("Importing People");
            using (WeddingContext context = new WeddingContext())
            {
                foreach (PersonDto person in people)
                {
                    List<string> genders = new List<string>() { "Male", "Female", "Not Specified" };
                    if (!genders.Contains(person.Gender) || person.MiddleInitial == null || person.FirstName == null || person.LastName == null || person.FirstName.Count() > 60 || person.MiddleInitial.Count() > 1 || person.LastName.Count() < 2)
                    {
                        Console.WriteLine("Invalid Person! ...");
                    }
                    else
                    {
                        person.Birthday = (person.Birthday.Year < 1857) ? DateTime.Now : person.Birthday;

                        Person personImp = new Person
                        {
                            FirstName = person.FirstName,
                            MiddleNameSymbol = person.MiddleInitial,
                            LastName = person.LastName,
                            Gender = person.Gender,
                            Birthday = person.Birthday,
                            Email = person.Email,
                            Phone = person.Phone,
                        };
                        Console.WriteLine($"Person: {personImp.FullName}, gender: {personImp.Gender} imported!");
                        context.People.Add(personImp);                  
                    }
                }

                context.SaveChanges();
            }
            Console.WriteLine("All People added! Press any key to continue ..."); Console.ReadKey();
        }
    }
}

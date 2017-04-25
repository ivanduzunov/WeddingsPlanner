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

            Console.WriteLine("Importin Agencies! ");

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
                Console.WriteLine("All agencies added. Press any key...");
                Console.ReadKey();

            }
        }
        public static void AddPeople(IEnumerable<Person> people)
        {
            Console.WriteLine("Importing People");
            using (WeddingContext context = new WeddingContext())
            {


                foreach (Person person in people)
                {


                    List<string> genders = new List<string>() { "Male", "Female", "Not Specified" };
                    if (genders.Contains(person.Gender))
                    {

                        Person personImp = new Person
                        {
                            FirstName = person.FirstName,
                            MiddleNameSymbol = person.MiddleNameSymbol,
                            LastName = person.LastName,
                            Gender = person.Gender,
                            Birthdate = person.Birthdate,
                            Email = person.Email,
                            Phone = person.Phone,
                        };
                        context.People.Add(personImp);
                        Console.WriteLine($"Person {personImp.FullName}, Age {personImp.Age} imported!");

                    }
                    else
                    {
                        Console.WriteLine("Invalid Person! ...");
                    }


                }
                context.SaveChanges(); //Validation failed for one or more entities.
            }

        }
    }
}

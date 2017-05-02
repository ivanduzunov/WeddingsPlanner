using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingsPlanner.Models;
using System.Data.Entity.Validation;


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

        }
        public static void AddWeddingsInvitations(IEnumerable<WeddingDto> weddings)
        {
            Console.WriteLine("Importing Weddings...");
            using (WeddingContext context = new WeddingContext())
            {
                foreach (WeddingDto wedding in weddings)
                {

                    var bride = context.People.Where(p => p.FirstName + " " + p.MiddleNameSymbol + " " + p.LastName == wedding.Bride).FirstOrDefault();
                    var bridesgroom = context.People.Where(p => p.FirstName + " " + p.MiddleNameSymbol + " " + p.LastName == wedding.Bridegroom).FirstOrDefault();
                    var agency = context.Agencies.Where(a => a.Name == wedding.Agency).FirstOrDefault();

                    if (bride == null || bridesgroom == null || wedding.Date == default(DateTime) || agency == null || wedding.Guests.Count == 0)
                    {
                        Console.WriteLine("Error. Invalid data provided");
                        continue;
                    }
                    else
                    {
                        var wedAdd = new Wedding
                        {
                            Bride = bride,
                            Bridegroom = bridesgroom,
                            Date = wedding.Date,
                            Agency = agency
                        };


                        foreach (GuestDto invitation in wedding.Guests)
                        {
                            var guest = context.People.Where(p => p.FirstName + " " + p.MiddleNameSymbol + " " + p.LastName == invitation.Name).FirstOrDefault();
                            if (guest != null)
                            {
                                wedAdd.Invitations.Add(new Invitation
                                {
                                    Guest = guest,
                                    Attending = (invitation.RSVP == "true") ? true : false,
                                    Family = invitation.Family,

                                });
                                Console.WriteLine($"Invitation with status {invitation.RSVP} added!");
                            }
                            else
                            {
                                Console.WriteLine("Invalid Invitation data!");
                            }


                        }
                        context.Weddings.Add(wedAdd);
                        context.SaveChanges();
                        Console.WriteLine($"Wedding of {bride.FirstName}, with Bridesgroom {bridesgroom.FirstName} with {wedAdd.Invitations.Count} guests imported!");
                    }
                }
            }
        }
    }
}

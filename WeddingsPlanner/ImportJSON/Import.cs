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
                    if (wedding.Bride != null && wedding.Bridegroom != null && wedding.Date != default(DateTime) && wedding.Agency != null)
                    {
                        Wedding wedAdd = new Wedding
                        {
                            Bride = context.People.Where(p => p.FirstName + " " + p.MiddleNameSymbol + p.LastName == wedding.Bride).FirstOrDefault(),
                            Bridegroom = context.People.Where(p => p.FirstName + " " + p.MiddleNameSymbol + p.LastName == wedding.Bridegroom).FirstOrDefault(),
                            Date = wedding.Date,
                            Agency = context.Agencies.Where(a => a.Name == wedding.Agency).FirstOrDefault()
                        };

                        List<Invitation> invAddList = new List<Invitation>();
                        if (wedding.Guests != null)
                        {
                            foreach (GuestDto invitation in wedding.Guests)
                            {
                                Invitation Invitation = new Invitation
                                {
                                    WeddingId = wedAdd.Id,
                                    Guest = context.People.Where(p => p.FirstName + " " + p.MiddleNameSymbol + p.LastName == invitation.Name).FirstOrDefault(),
                                    Attending = (invitation.RSVP == "true") ? true : false,
                                    Family = invitation.Family,

                                };
                                wedAdd.Invitations.Add(Invitation);
                                context.SaveChanges();
                            }
                        }

                        context.Weddings.Add(wedAdd);


                        

                       
                        context.SaveChanges();
                        Console.WriteLine($"Wedding of {wedding.Bride}, with Bridesgroom {wedding.Bridegroom} with {wedding.Guests.Count} guests imported!");


                    }
                    else
                    {
                        Console.WriteLine("Invalid Wedding!");
                    }

                }
            }
        }
    }
}

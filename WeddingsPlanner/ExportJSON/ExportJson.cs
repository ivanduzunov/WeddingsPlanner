using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using WeddingsPlanner;
using System.Web.Script.Serialization;
using WeddingsPlanner.Models;

namespace WeddingsPlanner.ExportJSON
{
    public class ExportJson
    {
        public static void CreateOrderedAgencies(WeddingContext context)
        {
            var agenciesList = context.Agencies.OrderByDescending(a => a.EmployeesCount).ThenBy(a => a.Name).Select(a => new { a.Name, a.EmployeesCount, a.Town }).ToList();
            var json = JsonConvert.SerializeObject(agenciesList, Formatting.Indented);
            File.WriteAllText(@"C:\Users\Mihail\Documents\visual studio 2015\Projects\WeddingsPlanner\WeddingsPlanner\ExportJSON\agencies-ordered.json ", json);
            Console.WriteLine("File agencies-ordered.json  created!");

        }
        public static void CreateGuestList(WeddingContext context)
        {
            var guestList = context.Weddings.Select(w => new
            {
                bride = w.Bride.FirstName + " " + w.Bride.MiddleNameSymbol + " " + w.Bride.LastName,
                bridegroom = w.Bridegroom.FirstName + " " + w.Bridegroom.MiddleNameSymbol + " " + w.Bridegroom.LastName,
                agency = new
                {
                    name = w.Agency.Name,
                    town = w.Agency.Town
                },
                invitetedGuests = context.Invitations.Where(i => i.WeddingId == w.Id).Count(),
                brideGuests = context.Invitations.Where(i => i.WeddingId == w.Id && i.Family == "Bride").Count(),
                bridegroomGuests = context.Invitations.Where(i => i.WeddingId == w.Id && i.Family == "Bridegroom").Count(),
                attendingGuests = context.Invitations.Where(i => i.WeddingId == w.Id && i.Attending == true).Count(),
                guests =

                    context.Invitations.Where(i => i.WeddingId == w.Id).Select(i => i.Guest.FirstName + " " + i.Guest.MiddleNameSymbol + " " + i.Guest.LastName)



            }).OrderByDescending(w => w.invitetedGuests).ThenByDescending(w => w.attendingGuests);

            var json = JsonConvert.SerializeObject(guestList, Formatting.Indented);
            File.WriteAllText(@"C:\Users\Mihail\Documents\visual studio 2015\Projects\WeddingsPlanner\WeddingsPlanner\ExportJSON\guest-list.json ", json);
            Console.WriteLine("Success!");
        }

    }
}

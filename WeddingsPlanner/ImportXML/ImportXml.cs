using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using WeddingsPlanner.ImportXML;
using WeddingsPlanner.Models;
using System.Data.Entity.Validation;

namespace WeddingsPlanner.ImportXML
{
    public class ImportXml
    {
        public static void AddVenues(XDocument xmlDoc)
        {
            Console.WriteLine("Adding Venues.");

            using (WeddingContext context = new WeddingContext())
            {
                var venues = xmlDoc.XPathSelectElements("venues/venue");
                int count = 0;
                foreach (var v in venues)
                {
                    var name = v.Attribute("name").Value;
                    var capacity = int.Parse(v.XPathSelectElement("capacity").Value);
                    var town = v.XPathSelectElement("town").Value;

                    Venue venue = new Venue
                    {
                        Name = name,
                        Capacity = capacity,
                        Town = town,


                    };

                    context.Venues.Add(venue);


                    Console.WriteLine($"{venue.Name} venue added!");



                }
                Console.ReadKey();
                context.SaveChanges();




            }
            var con = new WeddingContext();
            using (con)
            {
                foreach (var wedding in con.Weddings)
                {
                    Random r = new Random();
                    int venueId1 = r.Next(1, 100);
                    int venueId2 = r.Next(1, 100);
                    wedding.Venues.Add(con.Venues.Where(v => v.Id == venueId1).FirstOrDefault());
                    wedding.Venues.Add(con.Venues.Where(v => v.Id == venueId2).FirstOrDefault());

                    Console.WriteLine($"{wedding.Bride.FullName}'s wedding will be in {wedding.Venues.Count} places.");
                    Console.ReadKey();
                }
                con.SaveChanges();
            }
        }
        public static void AddPresents(XDocument xmlDoc)
        {
            Console.WriteLine("Adding Presents.");

            using (WeddingContext context = new WeddingContext())
            {
                var presents = xmlDoc.XPathSelectElements("presents/present");
                foreach (var p in presents)
                {

                    if (p.Attributes().Count() > 2 && (p.FirstAttribute.Value.Equals("cash") || p.FirstAttribute.Value.Equals("gift")))
                    {

                        var presentType = p.Attribute("type");

                        if (presentType.Value.Equals("cash"))
                        {
                            int? invitationId = int.Parse(p.Attribute("invitation-id").Value);
                            double? amount = int.Parse(p.Attribute("amount").Value);

                            if (invitationId > 0 && invitationId != null && amount != null && invitationId <= context.People.Count())
                            {
                                var invitation = context.Invitations.Where(i => i.Id == invitationId).FirstOrDefault();
                                var owner = context.People.Where(o => o.Id == invitation.GuestId).FirstOrDefault();

                                Cash cash = new Cash
                                {
                                    OwnerId = owner.Id,
                                    CashAmount = (double)amount
                                };
                                invitation.Present = "cash";
                                context.CashPresents.Add(cash);
                                Console.WriteLine($"Cash Present {cash.CashAmount} imported.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid Cash Present data! ");

                            }
                        }
                        else if (presentType.Value.Equals("gift"))
                        {

                            int? invitationIdGift = int.Parse(p.Attribute("invitation-id").Value);
                            var prName = p.Attribute("present-name");
                            var size = p.Attribute("size");
                            List<string> sizes = new List<string>() { "Small", "Medium", "NotSpecified", "Large" };
                            var invitation = context.Invitations.Where(i => i.Id == invitationIdGift).FirstOrDefault();

                            Person owner = null;

                            if (invitation != null && context.Invitations.Count() >= invitationIdGift)
                            {
                                owner = context.People.Where(o => o.Id == invitation.GuestId).FirstOrDefault();
                            }

                            if (prName != null && size != null && sizes.Contains(size.Value) && invitationIdGift > 0 && owner != null)
                            {

                                Gift gift = new Gift
                                {
                                    Name = prName.Value,
                                    Size = size.Value.ToString(),
                                    OwnerId = owner.Id
                                };

                                invitation.Present = "gift";
                                context.GiftPresents.Add(gift);
                                Console.WriteLine($"Gift {gift.Name} imported.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid gift present data!");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Type or no type at all!");
                    }

                }
                context.SaveChanges();


                Console.WriteLine("Success!");

            }
        }
    }
}





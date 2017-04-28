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
                        Town = town
                    };

                    context.Venues.Add(venue);

                    Console.WriteLine($"{venue.Name} venue added!");

                    count++;

                }

                Random r = new Random();

                foreach (var wedding in context.Weddings)
                {
                    
                    int venueId1 = r.Next(1, count);
                    int venueId2 = r.Next(1, count -3);
                    wedding.Venues.Add(context.Venues.Where(v => v.Id == venueId1).FirstOrDefault());
                    wedding.Venues.Add(context.Venues.Where(v => v.Id == venueId2).FirstOrDefault());

                    Console.WriteLine($"{wedding.Bride.FullName}'s wedding will be in {wedding.Venues.Count} places.");
                }
                context.SaveChanges();
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
                    var presentType = p.Attribute("type");

                    if (presentType != null)
                    {

                        if (presentType.Equals("cash"))
                        {
                            int? invitationId = int.Parse(p.Attribute("invitation-id").Value);
                            double? amount = int.Parse(p.Attribute("amount").Value);

                            if (invitationId <= 0 || invitationId == null || amount == null )
                            {
                                var invitation = context.Invitations.Where(i => i.Id == invitationId).FirstOrDefault();
                                var owner = context.People.Where(o => o.Id == invitation.GuestId).FirstOrDefault();

                                Cash cash = new Cash
                                {
                                    Owner = owner,                               
                                    CashAmount = (double)amount
                                };

                                try
                                {
                                    context.CashPresents.Add(cash);
                                    invitation.Present = "Cash";
                                    owner.CashPresent = cash;
                                    
                                    Console.WriteLine($"Cash Present {cash.CashAmount} imported.");
                                }
                                catch (DbEntityValidationException)
                                {

                                    Console.WriteLine("Invalid Cash Present data! ");
                                }
                          
                            }
                        }
                        else if(presentType.Equals("gift"))
                        {
                            try
                            {
                                int? invitationIdGift = int.Parse(p.Attribute("invitation-id").Value);
                                var prName = p.Attribute("present-name");
                                var size = p.Attribute("size");
                                List<string> sizes = new List<string>() { "Small", "Medium", "Not Specified", "Large" };

                                if (prName != null && size != null && sizes.Contains(size.Value))
                                {

                                    Gift gift = new Gift
                                    {
                                        Name = prName.Value,
                                        Size = size.Value.ToString(),
                                    };


                                    var invitation = context.Invitations.Where(i => i.Id == invitationIdGift).FirstOrDefault();
                                    var owner = context.People.Where(o => o.Id == invitation.GuestId).FirstOrDefault();

                                    
                                    gift.Owner = owner;
                                    invitation.Present = "Gift";
                                    owner.GiftPresent = gift;
                                    context.GiftPresents.Add(gift);                             
                                    Console.WriteLine($"Gift {gift.Name} imported.");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid");
                                }
                            }
                            catch (DbEntityValidationException)
                            {

                                Console.WriteLine("Invalid Gift Present data!");
                            }
                            

                        }
                    }
                }
                context.SaveChanges();
            }
        }
    }
}




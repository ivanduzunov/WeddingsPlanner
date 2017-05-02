using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingsPlanner.Models;
using System.Xml.Linq;

namespace WeddingsPlanner.ExportXML
{
    public class ExportXml
    {
        public static void ExportVenues(WeddingContext context)
        {
            Console.WriteLine("Creating xml venues document!");
            XDocument xmlDoc = new XDocument();


            var venueList = context.Venues.Where(v => v.Town == "Sofia").OrderBy(v => v.Capacity).ToList();
            xmlDoc.Add(new XElement("Sofia"));


            foreach (var item in venueList)
            {


                xmlDoc.Element("Sofia").Add(new XElement("venue", new XElement("name", item.Name), new XElement("weddings count", item.Weddings.Count)));
                
                

            } 
            xmlDoc.Save(@"C:\Users\Mihail\Documents\visual studio 2015\Projects\WeddingsPlanner\WeddingsPlanner\ExportXML\venues.xml");
            Console.WriteLine("Success!");
        }
    }
}

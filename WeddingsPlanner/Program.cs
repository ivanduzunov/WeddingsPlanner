using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingsPlanner.Models;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Threading;
using WeddingsPlanner.ImportJSON;

namespace WeddingsPlanner
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new WeddingContext();
            //InitDb(context);
            //ImportAgencies(context);
            //ImportPeople(context);
            //ImportWeddingsAndInvitations(context);
            foreach (var item in context.Invitations)
            {
                Console.WriteLine(item.GuestId);
            }

        }
        public static void InitDb(WeddingContext context)
        {
            Console.WriteLine("DB initializing...");
            context.Database.Initialize(true);
            Console.WriteLine();
            Console.WriteLine("Database Initialized!");
            Console.WriteLine();
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
        public static void ImportAgencies(WeddingContext context)
        {
            Console.WriteLine("Reading <agencies.json> ...");
            var json = File.ReadAllText(@"C:\Users\Mihail\Documents\visual studio 2015\Projects\WeddingsPlanner\WeddingsPlanner\ImportJSON\agencies.json");

            var jsonList = JsonConvert.DeserializeObject<IEnumerable<Agency>>(json);
            Import.AddAgencies(jsonList);
        }
        public static void ImportPeople(WeddingContext context)
        {
            Console.WriteLine("Reading <people.json> ...");
            var json = File.ReadAllText(@"C:\Users\Mihail\Documents\visual studio 2015\Projects\WeddingsPlanner\WeddingsPlanner\ImportJSON\people.json");
            var jsonList = JsonConvert.DeserializeObject<IEnumerable<PersonDto>>(json);
            Import.AddPeople(jsonList);

        }
        public static void ImportWeddingsAndInvitations(WeddingContext context)
        {
            Console.WriteLine("Reading weddings.json...");
            var json = File.ReadAllText(@"C:\Users\Mihail\Documents\visual studio 2015\Projects\WeddingsPlanner\WeddingsPlanner\ImportJSON\weddings.json");
            var jsonList = JsonConvert.DeserializeObject<IEnumerable<WeddingDto>>(json);
            Import.AddWeddingsInvitations(jsonList);
        }
    }
}
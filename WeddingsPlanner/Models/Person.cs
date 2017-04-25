using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace WeddingsPlanner.Models
{
    public class Person
    {
        public Person()
        {
            this.Invitations = new HashSet<Invitation>();
        }
        
        [Key]
        public int Id { get; set; }
        [MinLength(1), MaxLength(60)]
        public string FirstName { get; set; }
        public string MiddleNameSymbol { get; set; }
        [MinLength(2)]
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + MiddleNameSymbol + " " + LastName;
            }
        }
        public string Gender
        {
            get;set;
               
        }
        public DateTime Birthdate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Age
        {
            get
            {
                return 2017 - Birthdate.Year;
            }
        }
        public int? CashPresentId { get; set; }
        public int? GiftPresentId { get; set; }
        public virtual Cash CashPresent { get; set; }
        public virtual Gift GiftPresent { get; set; }
        public int? BridegroomWedddingId { get; set; }
        public virtual Wedding BridegroomWeddding { get; set; }
        public int? BrideWedddingId { get; set; }
        public virtual Wedding BrideWeddding { get; set; }
        public virtual ICollection<Invitation> Invitations { get; set; }

    }
}

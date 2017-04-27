using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WeddingsPlanner.Models
{
    public class Person
    {
        public Person()
        {
            this.Invitations = new HashSet<Invitation>();
            this.Bridegrooms = new HashSet<Wedding>();
            this.Brides = new HashSet<Wedding>();


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
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Age
        {
            get
            {
                return 2017 - Birthday.Year;
            }
        }
        public int? CashPresentId { get; set; }
        public int? GiftPresentId { get; set; }
        public virtual Cash CashPresent { get; set; }
        public virtual Gift GiftPresent { get; set; }
       
        public virtual ICollection<Invitation> Invitations { get; set; }
        [InverseProperty("Bride")]
        public virtual ICollection<Wedding> Brides { get; set; }
        [InverseProperty("Bridegroom")]
        public virtual ICollection<Wedding> Bridegrooms { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WeddingsPlanner.Models
{
    public class Gift
    {
        private string size;
        [Key]
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public virtual Person Owner { get; set; }
        public string Name { get; set; }
        public string Size
        {
            get
            {
                return this.size;
            }
            set
            {
                string checksize = value;
                if (checksize == "Small" || checksize == "Medium " || checksize == "Not Specified"|| checksize == "Large")
                {
                    this.size = value;
                }
                else
                {
                    throw new Exception("Invalid Present Size!");
                }
            }
        }
        public int InvitationId { get; set; }
        public virtual Invitation Invitation { get; set; }

    }
}

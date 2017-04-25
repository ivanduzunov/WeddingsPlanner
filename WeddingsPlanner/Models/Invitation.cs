﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace WeddingsPlanner.Models
{
    public class Invitation
    {
        [Key]
        public int Id { get; set; }
        public int GuestId { get; set; }
        public virtual Person Guest { get; set; }
        public int? CashId { get; set; }
        public int? GiftId { get; set; }
        public int WeddingId { get; set; }
        public virtual Cash Cash { get; set; }
        public virtual Gift Gift { get; set; }
        public virtual Wedding Wedding { get; set; }
        public string Attending { get; set; }
        public string Family { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace AuctionHouseCore.Models
{
    public partial class AhPerson
    {
        public Guid Id { get; set; }
        public string AspNetUserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Adress { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public virtual AspNetUsers AspNetUser { get; set; }
    }
}

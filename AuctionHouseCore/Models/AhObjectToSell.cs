using System;
using System.Collections.Generic;

namespace AuctionHouseCore.Models
{
    public partial class AhObjectToSell
    {
        public AhObjectToSell()
        {
            AhAuctions = new HashSet<AhAuctions>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string AddedBy { get; set; }
        public bool Sold { get; set; }
        public DateTime DateAdded { get; set; }
        public string ImagePath { get; set; }

        public virtual ICollection<AhAuctions> AhAuctions { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace AuctionHouseCore.Models
{
    public partial class AhShipmentType
    {
        public AhShipmentType()
        {
            AhAuctions = new HashSet<AhAuctions>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Time { get; set; }

        public virtual ICollection<AhAuctions> AhAuctions { get; set; }
    }
}

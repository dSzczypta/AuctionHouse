using System;
using System.Collections.Generic;

namespace AuctionHouseCore.Models
{
    public partial class AhPaymentMethod
    {
        public AhPaymentMethod()
        {
            AhAuctions = new HashSet<AhAuctions>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AhAuctions> AhAuctions { get; set; }
    }
}

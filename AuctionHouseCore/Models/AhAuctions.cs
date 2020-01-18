using System;
using System.Collections.Generic;

namespace AuctionHouseCore.Models
{
    public partial class AhAuctions
    {
        public Guid Id { get; set; }
        public Guid ObjectId { get; set; }
        public string UserName { get; set; }
        public decimal Price { get; set; }
        public Guid? PaymentMethod { get; set; }
        public Guid? ShipmentType { get; set; }
        public bool? IsConfirmed { get; set; }

        public virtual AhObjectToSell ObjectNavigation { get; set; }
        public virtual AhPaymentMethod PaymentMethodNavigation { get; set; }
        public virtual AhShipmentType ShipmentTypeNavigation { get; set; }
    }
}

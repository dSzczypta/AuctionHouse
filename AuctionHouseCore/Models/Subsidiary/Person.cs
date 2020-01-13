using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionHouseCore.Models.Subsidiary
{
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}

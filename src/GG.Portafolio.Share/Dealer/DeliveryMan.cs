using System;
using System.Collections.Generic;

namespace GG.Portafolio.Shared.Dealer
{
    public record DeliveryMan
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<DateTime> Dates { get; set; }

        public DeliveryMan()
        {}

        public DeliveryMan(int id, string name, IEnumerable<DateTime> dates)
        {
            Id = id;
            Name = name;
            Dates = dates;
        }

        public DeliveryMan(int id)
        {
            Id = id;
            Dates = new List<DateTime>();
        }

        public DeliveryMan GetDeliveryMan()
        {
            return this with { Id = Id + 1 , Name =  $"Repartidor {Id + 1}" };
        }
    }
}

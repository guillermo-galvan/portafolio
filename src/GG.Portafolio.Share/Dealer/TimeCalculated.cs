using System;

namespace GG.Portafolio.Shared.Dealer
{
    public record TimeCalculated(int Id, DateTime Time, byte Status, byte Distributor)
    {
        public TimeCalculated InclementTime(DateTime date)
        {
            DateTime now = Time.AddMinutes(30);
            return this with { Time = now, Id = Id + 1, Status = (byte)(date > now ? 0 : 1) };
        }
    }
}

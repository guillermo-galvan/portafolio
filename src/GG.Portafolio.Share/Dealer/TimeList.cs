using System;
using System.Collections.Generic;

namespace GG.Portafolio.Shared.Dealer
{
    public record TimeList(DateTime BaseDay, int MaxId, List<TimeCalculated> TimeCalculateds);

}

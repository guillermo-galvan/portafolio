using GG.Portafolio.Shared.Dealer;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GG.Portafolio.BusinessLogic.Dealer
{
    public class DealerManagement
    {
        private readonly ILogger<DealerManagement> _logger;

        public DealerManagement(ILogger<DealerManagement> logger)
        {
            _logger = logger;
        }

        public TimeList Get(DateTime date, int maxDate = 22)
        {
            try
            {
                DateTime time = new(date.Year, date.Month, date.Day, 20, 0, 0);

                if (date > time)
                {
                    time = time.AddDays(1);
                }

                time = new DateTime(time.Year, time.Month, time.Day, 9, 0, 0);

                bool esMayor = date > time;

                List<TimeCalculated> result = new()
                {
                    new TimeCalculated(1, time, (byte)(esMayor ? 0 : 1), 0)
                };

                while (result.Count <= maxDate)
                {
                    result.Add(result.Last().InclementTime(date));
                }

                return new TimeList(result.Last().Time, result.Max(x => x.Id), result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{name} {date}", MethodBase.GetCurrentMethod().Name, date);
                throw;
            }
        }

        public IEnumerable<DeliveryMan> GetDeliveryMan(int maxDeliveryMan = 9)
        {
            try
            {
                List<DeliveryMan> deliveries = new()
                {
                    new DeliveryMan(1, "Repartidor 1", new List<DateTime>())
                };

                while (deliveries.Count < maxDeliveryMan)
                {
                    deliveries.Add(deliveries.Last().GetDeliveryMan());
                }

                return deliveries;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{name}", MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }
    }
}

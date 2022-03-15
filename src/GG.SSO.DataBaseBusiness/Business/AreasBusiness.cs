using GG.SSO.DataBaseBusiness.Sentences;
using GG.SSO.Entity.Table.Sso;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace GG.SSO.DataBaseBusiness.Business
{
    public class AreasBusiness
    {
        private readonly AreasSentences _areasSentences;
        private readonly ILogger<AreasBusiness> _logger;

        public AreasBusiness(AreasSentences areasSentences, ILogger<AreasBusiness> logger)
        {
            _areasSentences = areasSentences;
            _logger = logger;
        }

        public void Insert(Areas areas)
        {
            try
            {
                areas.Insert();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {areas}",MethodBase.GetCurrentMethod().Name,areas);
                throw;
            }
        }

        public Areas Get(int areaId)
        {
            Areas result;
            try
            {
                result = Areas.Read(_areasSentences.AddCliteriById(areaId).GetCriteriaCollection())
                              .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {areaId}", MethodBase.GetCurrentMethod().Name,areaId);
                throw;
            }

            return result;
        }

        public IEnumerable<Areas> Get(IEnumerable<int> areasId)
        {
            try
            {
                return Areas.Read(_areasSentences.AddInCliteriById(areasId).GetCriteriaCollection());
                              
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {areaId}", MethodBase.GetCurrentMethod().Name,areasId);
                throw;
            }
        }

        public IEnumerable<Areas> Get()
        {
            try
            {
                return Areas.Read();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name}", MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }
    }
}

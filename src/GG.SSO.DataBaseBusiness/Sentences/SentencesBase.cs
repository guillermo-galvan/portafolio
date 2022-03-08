using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGPuntoYComa.SSO.DataBaseBusiness.Sentences
{
    public abstract class SentencesBase
    {
        protected readonly ICriteriaBuilder _criteriaBuilder;

        public SentencesBase(ICriteriaBuilder criteriaBuilder)
        {
            _criteriaBuilder = criteriaBuilder;
        }

        public ICriteriaCollection GetCriteriaCollection()
        {
            return _criteriaBuilder.GetCriteriaCollection();
        }
    }
}

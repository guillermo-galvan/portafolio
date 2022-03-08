using System.Data.CRUD;

namespace GG.Portafolio.DataBaseBusiness.Sentences
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

using GG.Portafolio.DataBaseBusiness.Sentences;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GG.Portafolio.DataBaseBusiness.Test.Sentences
{
    public class UserSentencesTest
    {
        [Fact]
        public void UserSentences_AddCliteriById_NotEmpty()
        {
            UserSentences sentences = new(new CriteriaBuilder());

            sentences.AddCliteriById(Guid.NewGuid().ToString(), LogicalOperator.AND);

            Assert.NotEmpty(sentences.GetCriteriaCollection().Criterias);
        }

        [Fact]
        public void UserSentences_AddCliteriByName_NotEmpty()
        {
            UserSentences sentences = new(new CriteriaBuilder());

            sentences.AddCliteriByName(Guid.NewGuid().ToString(), LogicalOperator.AND);

            Assert.NotEmpty(sentences.GetCriteriaCollection().Criterias);
        }

        [Fact]
        public void UserSentences_AddCliteriByEmail_NotEmpty()
        {
            UserSentences sentences = new(new CriteriaBuilder());

            sentences.AddCliteriByEmail(Guid.NewGuid().ToString(), LogicalOperator.AND);

            Assert.NotEmpty(sentences.GetCriteriaCollection().Criterias);
        }
    }
}

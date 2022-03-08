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
    public class BlogSentencesTest
    {
        [Fact]
        public void BlogSentences_AddCliteriById_NotEmpty()
        {
            BlogSentences sentences = new(new CriteriaBuilder());

            sentences.AddCliteriById(Guid.NewGuid().ToString(), LogicalOperator.AND);

            Assert.NotEmpty(sentences.GetCriteriaCollection().Criterias);
        }

        [Fact]
        public void BlogSentences_AddCliteriByUser_Id_NotEmpty()
        {
            BlogSentences sentences = new(new CriteriaBuilder());

            sentences.AddCliteriByUser_Id(Guid.NewGuid().ToString(), LogicalOperator.AND);

            Assert.NotEmpty(sentences.GetCriteriaCollection().Criterias);
        }

        [Fact]
        public void BlogSentences_AddCliteriByTitle_NotEmpty()
        {
            BlogSentences sentences = new(new CriteriaBuilder());

            sentences.AddCliteriByTitle(Guid.NewGuid().ToString(), LogicalOperator.AND);

            Assert.NotEmpty(sentences.GetCriteriaCollection().Criterias);
        }

        [Fact]
        public void BlogSentences_AddCliteriByIsActive_NotEmpty()
        {
            BlogSentences sentences = new(new CriteriaBuilder());

            sentences.AddCliteriByIsActive(true, LogicalOperator.AND);

            Assert.NotEmpty(sentences.GetCriteriaCollection().Criterias);
        }
    }
}

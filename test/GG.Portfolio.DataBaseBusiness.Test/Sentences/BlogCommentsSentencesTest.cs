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
    public class BlogCommentsSentencesTest
    {
        [Fact]
        public void BlogCommentsSentences_AddCliteriById_NotEmpty()
        {
            BlogCommentsSentences sentences = new (new CriteriaBuilder());

            sentences.AddCliteriById(1, LogicalOperator.AND);

            Assert.NotEmpty(sentences.GetCriteriaCollection().Criterias);
        }

        [Fact]
        public void BlogCommentsSentences_AddCliteriByBlog_Id_NotEmpty()
        {
            BlogCommentsSentences sentences = new(new CriteriaBuilder());

            sentences.AddCliteriByBlog_Id(Guid.NewGuid().ToString(), LogicalOperator.AND);

            Assert.NotEmpty(sentences.GetCriteriaCollection().Criterias);
        }

        [Fact]
        public void BlogCommentsSentences_AddCliteriByUser_Id_NotEmpty()
        {
            BlogCommentsSentences sentences = new(new CriteriaBuilder());
            sentences.AddCliteriByUser_Id(Guid.NewGuid().ToString(), LogicalOperator.AND);
            Assert.NotEmpty(sentences.GetCriteriaCollection().Criterias);
        }
    }
}

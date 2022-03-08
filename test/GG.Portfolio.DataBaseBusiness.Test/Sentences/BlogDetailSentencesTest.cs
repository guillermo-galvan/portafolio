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
    public class BlogDetailSentencesTest
    {
        [Fact]
        public void BlogDetailSentences_AddCliteriByBlog_Id_NotEmpty()
        {
            BlogDetailSentences sentences = new(new CriteriaBuilder());

            sentences.AddCliteriByBlog_Id(Guid.NewGuid().ToString(), LogicalOperator.AND);

            Assert.NotEmpty(sentences.GetCriteriaCollection().Criterias);
        }
    }
}

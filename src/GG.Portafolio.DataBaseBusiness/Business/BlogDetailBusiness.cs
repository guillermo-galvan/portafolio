using GG.Portafolio.DataBaseBusiness.Sentences;
using GG.Portafolio.Entity.Table.Blog;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;

namespace GG.Portafolio.DataBaseBusiness.Business
{
    public class BlogDetailBusiness
    {
        private readonly BlogDetailSentences _blogDetailSentences;
        private readonly ILogger<BlogDetailBusiness> _logger;

        public BlogDetailBusiness(BlogDetailSentences blogDetailSentences, ILogger<BlogDetailBusiness> logger)
        {
            _blogDetailSentences = blogDetailSentences;
            _logger = logger;
        }

        public BlogDetail Get(string blog_Id)
        {
            try
            {
                return BlogDetail.Read(_blogDetailSentences.AddCliteriByBlog_Id(blog_Id).GetCriteriaCollection()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name}", MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }
    }
}

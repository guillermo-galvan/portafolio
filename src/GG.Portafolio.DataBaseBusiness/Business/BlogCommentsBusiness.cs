using GG.Portafolio.DataBaseBusiness.Sentences;
using GG.Portafolio.Entity.Table.Blog;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace GG.Portafolio.DataBaseBusiness.Business
{
    public class BlogCommentsBusiness
    {
        private readonly BlogCommentsSentences _blogCommentsSentences;
        private readonly ILogger<BlogCommentsBusiness> _logger;

        public BlogCommentsBusiness(BlogCommentsSentences blogCommentsSentences, ILogger<BlogCommentsBusiness> logger)
        {
            _blogCommentsSentences = blogCommentsSentences;
            _logger = logger;
        }

        public IEnumerable<BlogComments> GetBlogComments(string blog_Id)
        {
            try
            {
                return BlogComments.Read(_blogCommentsSentences.AddCliteriByBlog_Id(blog_Id).GetCriteriaCollection());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name}", MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }
    }

}

using GG.Portafolio.Entity.Table.Blog;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GG.Portafolio.DataBaseBusiness.Sentences;

namespace GG.Portafolio.DataBaseBusiness.Business
{
    public class BlogBusiness
    {
        private readonly BlogSentences _blogSentences;
        private readonly ILogger<BlogBusiness> _logger;

        public BlogBusiness(ILogger<BlogBusiness> logger, BlogSentences blogSentences)
        {
            _logger = logger;
            _blogSentences = blogSentences;
        }

        public IEnumerable<Blog> GetBlogs()
        {
            try
            {
                return Blog.Read(_blogSentences.AddCliteriByIsActive(true).GetCriteriaCollection());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name}", MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        public Blog GetBlogById(string id)
        {
            try
            {
                return Blog.Read(_blogSentences.AddCliteriByIsActive(true)
                                               .AddCliteriById(id)
                                               .GetCriteriaCollection()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {id}", MethodBase.GetCurrentMethod().Name, id);
                throw;
            }
        }

        public Blog GetBlogByTitle(string title)
        {
            try
            {
                return Blog.Read(_blogSentences.AddCliteriByIsActive(true)
                                               .AddCliteriByTitle(title)
                                               .GetCriteriaCollection()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {title}", MethodBase.GetCurrentMethod().Name, title);
                throw;
            }
        }
    }
}

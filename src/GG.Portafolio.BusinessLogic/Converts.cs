using GG.Portafolio.Entity.Table.Blog;
using GG.Portafolio.Shared.Blog;
using System;
using BlogComments = GG.Portafolio.Shared.Blog.BlogComments;

namespace GG.Portafolio.BusinessLogic
{
    internal static class Converts
    {
        public static BlogResponse ToBlogResponse(Entity.Table.Blog.Blog blog)
        {
            return blog == null ? default : new BlogResponse { Id = blog.Id, Dsc = blog.Dsc, Title = blog.Title, CreateDate = blog.Create };
        }

        public static BlogContentReponse ToBlogContentReponse(Entity.Table.Blog.Blog blog)
        {
            return blog == null ? default : new BlogContentReponse
            {
                Id = blog.Id,
                Dsc = blog.Dsc,
                Title = blog.Title,
                CreateDate = blog.Create,
                EditDate = blog.Edit
            };
        }

        public static BlogContentWithCommentsReponse ToBlogContentWithCommentsReponse(Entity.Table.Blog.Blog blog)
        {
            return blog == null ? default : new BlogContentWithCommentsReponse
            {
                Id = blog.Id,
                Dsc = blog.Dsc,
                Title = blog.Title,
                CreateDate = blog.Create,
                EditDate = blog.Edit,
            };
        }

        public static Entity.Table.Blog.Blog ToBlog(BlogNewRequest blogRequest)
        {
            return new Entity.Table.Blog.Blog
            {
                Title = blogRequest.Title,
                Dsc = blogRequest.Dsc,
                User_Id = blogRequest.UserId,
            };
        }

        public static Entity.Table.Blog.Blog ToBlog(BlogEditRequest blogRequest)
        {
            return new Entity.Table.Blog.Blog
            {
                Id = blogRequest.Id,
                Title = blogRequest.Title,
                Dsc = blogRequest.Dsc,
                User_Id = blogRequest.UserId,
            };
        }

        public static BlogComments ToComment(Entity.Table.Blog.BlogComments blogComments, string userName)
        {
            return new BlogComments
            {
                BlogId = blogComments.Blog_Id,
                Content = blogComments.Comment,
                Date = new DateTime(blogComments.Create),
                Name = string.IsNullOrWhiteSpace(userName) ? "" : userName
            };
        }

        public static Entity.Table.Blog.BlogComments ToCommentEntity(BlogComments blogComments)
        {
            return new Entity.Table.Blog.BlogComments
            {
                Blog_Id = blogComments.BlogId,
                Comment = blogComments.Content,
                Create = blogComments.Date.Ticks,
                User_Id = blogComments.User_Id,
            };
        }
    }
}

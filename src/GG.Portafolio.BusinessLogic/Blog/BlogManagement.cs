using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using GG.Portafolio.DataBaseBusiness;
using System.Data.CRUD;
using GG.Portafolio.Shared.Blog;
using GG.Portafolio.Entity.Table.Blog;
using GG.Portafolio.DataBaseBusiness.Business;

namespace GG.Portafolio.BusinessLogic.Blog
{
    public class BlogManagement
    {
        private const string _error = "The Blog cannot be recovered.";
        private const string _titleIsAlready = "A blog with the title was already found.";
        private const string _cannotSave = "Cannot save the detail file.";
        private const string _blogNotFound = "Blog not found";

        private readonly ILogger<BlogManagement> _logger;
        private readonly BlogBusiness _blogBusiness;
        private readonly BlogDetailBusiness _blogDetailBusiness;
        private readonly BlogCommentsBusiness _blogCommentsBusiness;
        private readonly UserBusiness _userBusiness;

        public BlogManagement(ILogger<BlogManagement> logger, BlogBusiness blogBusiness, BlogDetailBusiness blogDetailBusiness, BlogCommentsBusiness blogCommentsBusiness,
            UserBusiness userBusiness)
        {
            _logger = logger;
            _blogBusiness = blogBusiness;
            _blogDetailBusiness = blogDetailBusiness;
            _blogCommentsBusiness = blogCommentsBusiness;
            _userBusiness = userBusiness;
        }

        private void ValidateTitle(Entity.Table.Blog.Blog blog, ICollection<string> errores)
        {
            var blogTmp = _blogBusiness.GetBlogByTitle(blog.Title);

            if (blogTmp != null)
            {
                errores.Add(_titleIsAlready);
            }
        }

        private void SaveBlog(Entity.Table.Blog.Blog blog, BlogNewRequest blogNewRequest, string rootPath, string webBase,
            OperationType operation, ICollection<string> errores)
        {
            string pathFiles = Path.Combine(rootPath, $"{blog.Create}");
            BlogDetail blogDetail = new() { Detail = blogNewRequest.Content, Blog_Id = blog.Id };
            IDataBaseConnection connection = ConnectionDataBaseCollection.Connections["MainConnection"];
            DatabaseOperationModel blogOperationMode = new()
            {
                Entity = blog,
                Operation = operation,
                AfterExecute = (entity) =>
                {
                    Entity.Table.Blog.Blog blog = (Entity.Table.Blog.Blog)entity;

                    try
                    {
                        if (!Directory.Exists(pathFiles))
                        {
                            Directory.CreateDirectory(pathFiles);
                        }

                        int countFile = Directory.GetFiles(pathFiles).Length;
                        blogNewRequest.ContentFiles?.ForEach(x =>
                        {
                            string extension = Path.GetExtension(x.Name);
                            blogDetail.Detail = blogDetail.Detail.Replace($"..{x.Url}", $"{webBase}/{blog.Create}/{countFile}{extension}");
                            blogDetail.Detail = blogDetail.Detail.Replace($"{x.Url}", $"{webBase}/{blog.Create}/{countFile}{extension}");
                            File.WriteAllBytes(Path.Combine(pathFiles, $"{countFile}{extension}"), x.File);
                            countFile++;
                        });

                    }
                    catch (Exception ex)
                    {
                        errores.Add(_cannotSave);
                        _logger.LogError(ex, "{name} {_cannot}", MethodBase.GetCurrentMethod().Name, _cannotSave);
                    }

                    return !errores.Any();
                }
            };
            DatabaseOperationModel blogDetailOperationMode = new()
            {
                Entity = blogDetail,
                Operation = operation
            };

            new DatabaseOperation().WithTransaction(connection, new List<DatabaseOperationModel>
                    {
                        blogOperationMode,
                        blogDetailOperationMode
                    }, _logger);
        }

        public IEnumerable<BlogResponse> GetBlogs()
        {
            try
            {
                return _blogBusiness.GetBlogs().Select(x => Converts.ToBlogResponse(x));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name}", MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        public BlogContentReponse GetBlogById(string id)
        {
            try
            {
                BlogContentReponse result = null;

                var blog = _blogBusiness.GetBlogById(id);

                if (blog != null)
                {
                    result = Converts.ToBlogContentReponse(blog);
                    result.Content = _blogDetailBusiness.Get(blog.Id)?.Detail;
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {id}", MethodBase.GetCurrentMethod().Name, id);
                throw;
            }
        }

        public BlogContentWithCommentsReponse GetBlogs(string title)
        {
            try
            {
                BlogContentWithCommentsReponse result = null;

                var blog = _blogBusiness.GetBlogByTitle(title);

                if (blog != null)
                {
                    result = Converts.ToBlogContentWithCommentsReponse(blog ?? throw new Exception(_error));
                    result.Content = _blogDetailBusiness.Get(blog.Id)?.Detail;
                    var comments = _blogCommentsBusiness.GetBlogComments(blog.Id);
                    List<Entity.Table.Blog.User> users = new();

                    foreach (var comment in comments.Where(x => !string.IsNullOrWhiteSpace(x.User_Id)).GroupBy(x => x.User_Id))
                    {
                        var user = _userBusiness.GetUserById(comment.Key);
                        if (user != null)
                        {
                            users.Add(user);
                        }
                    }

                    result.Comments = (from comment in comments
                                       join user in users on comment.User_Id equals user.Id into left
                                       from l in left.DefaultIfEmpty()
                                       select Converts.ToComment(comment, l?.Name)).ToList();
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {title}", MethodBase.GetCurrentMethod().Name, title);
                throw;
            }
        }

        public BlogOperationResponse CreateNewBlog(BlogNewRequest blogNewRequest, string rootPath, string webBase)
        {
            try
            {
                ICollection<string> errores = new List<string>();
                var blog = Converts.ToBlog(blogNewRequest);

                blog.Create = DateTime.Now.Ticks;
                blog.Edit = blog.Create;
                blog.IsActive = true;
                blog.Id = Guid.NewGuid().ToString();
                ValidateTitle(blog, errores);

                if (!errores.Any())
                {
                    SaveBlog(blog, blogNewRequest, rootPath, webBase, OperationType.Insert, errores);
                }

                return new BlogOperationResponse { Errores = errores };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {blogNewRequest}", MethodBase.GetCurrentMethod().Name, blogNewRequest);
                throw;
            }
        }

        public BlogOperationResponse EditBlog(BlogEditRequest blogEditRequest, string rootPath, string webBase)
        {
            try
            {
                ICollection<string> errores = new List<string>();

                var blog = Converts.ToBlog(blogEditRequest);
                var blogBack = _blogBusiness.GetBlogById(blog.Id);

                if (blogBack == null)
                {
                    errores.Add(_blogNotFound);
                }
                else if (blog.Title != blogBack.Title)
                {
                    ValidateTitle(blog, errores);
                }

                if (!errores.Any())
                {
                    blogBack.Title = blog.Title;
                    blogBack.Dsc = blog.Dsc;
                    blogBack.Edit = DateTime.Now.Ticks;
                    SaveBlog(blogBack, blogEditRequest, rootPath, webBase, OperationType.Update, errores);
                }

                return new BlogOperationResponse { Errores = errores };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {blogNewRequest}", MethodBase.GetCurrentMethod().Name, blogEditRequest);
                throw;
            }
        }

        public bool BlogDelete(string id)
        {
            try
            {
                var blogBack = _blogBusiness.GetBlogById(id);

                if (blogBack != null)
                {
                    IDataBaseConnection connection = ConnectionDataBaseCollection.Connections["MainConnection"];
                    blogBack.IsActive = false;

                    new DatabaseOperation().WithoutTransaction(connection, new List<DatabaseOperationModel> {
                        new DatabaseOperationModel
                        {
                            Entity = blogBack,
                            Operation = OperationType.Update
                        },
                    }, _logger);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {id}", MethodBase.GetCurrentMethod().Name, id);
                throw;
            }
        }

        public void CommentSave(Shared.Blog.BlogComments comment)
        {
            try
            {
                IDataBaseConnection connection = ConnectionDataBaseCollection.Connections["MainConnection"];

                new DatabaseOperation().WithTransaction(connection, new List<DatabaseOperationModel>
                    {
                        new DatabaseOperationModel
                        {
                            Entity = Converts.ToCommentEntity(comment),
                            Operation = OperationType.Insert
                        }
                    }, _logger);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {comment}", MethodBase.GetCurrentMethod().Name, comment);
                throw;
            }
        }
    }
}

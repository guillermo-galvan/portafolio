using GG.Portafolio.Shared.Blog;
using System;
using System.Collections.Generic;

namespace GG.Portafolio.Site.Test.Data
{
    internal class BlogCommentsTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new BlogComments
                {
                    BlogId = Guid.NewGuid().ToString(),
                    Content = "Content",
                    Date = DateTime.Now,
                    Name =  "Name"
                }, true, true };
            yield return new object[] { new BlogComments
                {
                    BlogId = string.Empty,
                    Content = "Content",
                    Date = DateTime.Now,
                    Name =  "Name"
                }, false, false };
            yield return new object[] { new BlogComments
                {
                    BlogId = Guid.NewGuid().ToString(),
                    Content = string.Empty,
                    Date = DateTime.Now,
                    Name =  "Name"
                }, true, false };
            yield return new object[] { new BlogComments
                {
                    BlogId = Guid.NewGuid().ToString(),
                    Content = "Content",
                    Date = DateTime.Now,
                    Name =  string.Empty
                }, true, false };
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    }
}

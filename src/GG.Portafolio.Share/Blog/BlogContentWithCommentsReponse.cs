using System.Collections.Generic;

namespace GG.Portafolio.Shared.Blog
{
    public class BlogContentWithCommentsReponse : BlogContentReponse
    {
        public IEnumerable<BlogComments> Comments { get; set; }
    }
}

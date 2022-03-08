using GG.Portafolio.Site.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GG.Portafolio.Site.Test.Data
{
    internal class ImageLoadTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new ImgageLoad
                {
                   FileContent = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Documents", "success_userinfo_response.json")),
                   FileName = "success_userinfo_response.json"
                }, true };
            yield return new object[] { new ImgageLoad
                {
                   FileContent = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Documents", "success_userinfo_response.json")),
                   FileName = string.Empty
                }, false };
            yield return new object[] { new ImgageLoad
                {
                   FileContent = Array.Empty<byte>(),
                   FileName = string.Empty
                }, false };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

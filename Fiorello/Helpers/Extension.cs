using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.Helpers
{
    public static class Extension
    {
        public static bool CheckFileSize(this IFormFile file, int kb )
        {
            return file.Length / 1024 <= kb;
        }
        public static bool CheckFileType(this IFormFile file,string type)
        {
            return file.ContentType.Contains(type);
        }
    }
}

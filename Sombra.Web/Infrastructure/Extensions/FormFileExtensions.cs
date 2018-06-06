using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Sombra.Web.Infrastructure.Extensions
{
    public static class FormFileExtensions
    {
        public static string ConvertToBase64(this IFormFile file)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            var base64 = $"data:{file.ContentType};charset=utf-8;base64, {Convert.ToBase64String(bytes)}";
            return base64;
        }
    }
}
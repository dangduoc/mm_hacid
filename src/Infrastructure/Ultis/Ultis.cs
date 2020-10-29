using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Infrastructure.Ultis
{
    public static class Ultis
    {
        /// <summary>
        /// Nhận các giá trị với cùng thuộc tính từ đối tượng B
        /// </summary>
        /// <param name="object_1">Đối tượng A</param>
        /// <param name="object_2">Đối tượng B</param>
        public static void ReflectValueFrom(this object object_1, object object_2)
        {
            foreach (var item in object_1.GetType().GetProperties())
            {
                var info2 = object_2.GetType().GetProperty(item.Name);
                if (info2 != null)
                {
                    if (!item.GetMethod.IsVirtual)
                        item.SetValue(object_1, info2.GetValue(object_2) ?? null);
                }
            }
        }

        public static bool IsValidImage(this IFormFile file)
        {
            if (file == null) return false;
            if (file.Length == 0) return false;
            if (file.ContentType.ToLower() != "image/jpg" &&
                    file.ContentType.ToLower() != "image/jpeg" &&
                    file.ContentType.ToLower() != "image/pjpeg" &&
                    file.ContentType.ToLower() != "image/gif" &&
                    file.ContentType.ToLower() != "image/x-png" &&
                    file.ContentType.ToLower() != "image/png")
            {
                return false;
            }

            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            if (Path.GetExtension(file.FileName).ToLower() != ".jpg"
                && Path.GetExtension(file.FileName).ToLower() != ".png"
                && Path.GetExtension(file.FileName).ToLower() != ".gif"
                && Path.GetExtension(file.FileName).ToLower() != ".jpeg")
            {
                return false;
            }
            return true;
        }
        public static bool IsValidDocument(this IFormFile file)
        {
            if (file == null) return false;
            if (file.Length == 0) return false;

            if (Path.GetExtension(file.FileName).ToLower() != ".docx"
                && Path.GetExtension(file.FileName).ToLower() != ".doc"
                && Path.GetExtension(file.FileName).ToLower() != ".xlsx"
                && Path.GetExtension(file.FileName).ToLower() != ".xls"
                && Path.GetExtension(file.FileName).ToLower() != ".pdf"
                )
            {
                return false;
            }
            return true;
        }
        public static string FixLocalUrlToDomainUrl(this string url)
        {
            url = url.Replace(@"\\", @"\");
            url = url.Replace(@"\", "/");
            return url;
        }
    }
}

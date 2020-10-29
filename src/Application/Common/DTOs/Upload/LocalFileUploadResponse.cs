using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.DTOs.Upload
{
    public class LocalFileUploadResponse
    {
        public string FileName { get; set; }
        public double FileSize { get; set; }
        public string FileUrl { get; set; }
        public DateTime AddedOnDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}

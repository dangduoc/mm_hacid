using Application.Common.DTOs.Upload;
using Application.Wrappers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ILocalUploadService
    {
        Response<List<LocalFileUploadResponse>> GetImagesFromFolder(string folderPath);
        Response<List<LocalFileUploadResponse>> GetDocumentsFromFolder(string folderPath);
        Task<Response<List<LocalFileUploadResponse>>> UploadImages(List<IFormFile> files, string folder, int? width, int? height, bool keepFileName = false);
        Task<Response<LocalFileUploadResponse>> UploadImage(IFormFile file, string folder, int? width, int? height, bool keepFileName = false);
        Task<Response<LocalFileUploadResponse>>  UploadImage(IFormFile file, string folder, string fileName, int? width = null, int? height = null);
        Task<Response<LocalFileUploadResponse>> UploadDocument(IFormFile file, string folder, bool keepFileName = false);
        void DeleteFile(string filePath);
        Task<object> UploadForCkEditor(IFormFile fileToUpload, string folder, string host);


    }
}

using Application;
using Application.Common.DTOs.Upload;
using Application.Common.Interfaces;
using Application.Wrappers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Ultis;
using Application.Common.Exceptions;

namespace BaseProjectWebRazor.Services
{
    public class LocalUploadService : ILocalUploadService
    {
        private readonly string RootImageUploadFolder;
        private readonly string RootDocumentUploadFolder;
        private readonly IWebHostEnvironment _env;
        public LocalUploadService(IWebHostEnvironment env)
        {
            _env = env;
            RootImageUploadFolder = Path.Combine(env.WebRootPath, Constants.UPLOAD_IMAGES_FOLDER);
            RootDocumentUploadFolder = Path.Combine(env.WebRootPath, Constants.UPLOAD_DOCUMENTS_FOLDER);
        }


        public void DeleteFile(string filePath)
        {
            if (filePath.StartsWith('/')) filePath=filePath.Remove(0, 1);
            string path = Path.Combine(_env.WebRootPath, filePath);
            if (!File.Exists(path))
            {
                throw new NotFoundException("File", filePath);
            }
            File.Delete(path);
        }
    
       
        public async Task<Response<LocalFileUploadResponse>> UploadImage(IFormFile file, string folder, int? width, int? height, bool keepFileName = false)
        {
            try
            {

                string path = RootImageUploadFolder;
                if (!string.IsNullOrEmpty(folder))
                {
                    path = Path.Combine(path, folder);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                }

                if (!file.IsValidImage())
                {
                    return new Response<LocalFileUploadResponse>(false, "Invalid File Uploaded", null);
                }

                string newFileName = file.FileName;
                //check dir
                if (!keepFileName)
                {
                    newFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName).ToLower();
                }
                string url = Path.Combine(folder, newFileName).FixLocalUrlToDomainUrl();

                string fullPath = Path.Combine(path, newFileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    return new Response<LocalFileUploadResponse>(true, "", new LocalFileUploadResponse
                    {
                        FileName = file.FileName,
                        FileSize = file.Length,
                        FileUrl = url,
                        AddedOnDate = DateTime.Now,
                        LastModifiedDate = DateTime.Now
                    });
                }

            }
            catch (Exception ex)
            {
                return new Response<LocalFileUploadResponse>(false, ex.ToString(), null);
            }
        }
        public async Task<Response<LocalFileUploadResponse>> UploadImage(IFormFile file, string folder, string fileName, int? width = null, int? height = null)
        {
            try
            {
                if (string.IsNullOrEmpty(folder)) return new Response<LocalFileUploadResponse>(false, "", null);

               
                string path = RootImageUploadFolder;
                path = Path.Combine(path, folder);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }


                if (!file.IsValidImage())
                {
                    return new Response<LocalFileUploadResponse>(false, "Invalid File Uploaded", null);
                }
                if (string.IsNullOrEmpty(fileName)) fileName = file.FileName;
                else
                {
                    fileName += Path.GetExtension(file.FileName).ToLower();
                }
               

                string url = Path.Combine(folder, fileName).FixLocalUrlToDomainUrl();

                string fullPath = Path.Combine(path, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    return new Response<LocalFileUploadResponse>(true, "", new LocalFileUploadResponse
                    {
                        FileName = file.FileName,
                        FileSize = file.Length,
                        FileUrl = url,
                        AddedOnDate = DateTime.Now,
                        LastModifiedDate = DateTime.Now
                    });
                }

            }
            catch (Exception ex)
            {
                return new Response<LocalFileUploadResponse>(false, ex.ToString(), null);
            }
        }

        public async Task<Response<List<LocalFileUploadResponse>>> UploadImages(List<IFormFile> files, string folder, int? width, int? height, bool keepFileName = false)
        {
            try
            {

                string path = RootImageUploadFolder;
                List<LocalFileUploadResponse> filesUploaded = new List<LocalFileUploadResponse>();
                if (!string.IsNullOrEmpty(folder))
                {
                    path = Path.Combine(path, folder);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                }

                foreach (var file in files)
                {
                    if (!file.IsValidImage())
                    {
                        continue;
                    }

                    string newFileName = file.FileName;
                    //check dir
                    if (!keepFileName)
                    {
                        newFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName).ToLower();
                    }
                    string url = Path.Combine(folder, newFileName).FixLocalUrlToDomainUrl();

                    string fullPath = Path.Combine(path, newFileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        filesUploaded.Add(new LocalFileUploadResponse
                        {
                            FileName = file.FileName,
                            FileSize = file.Length,
                            FileUrl = url,
                            LastModifiedDate = DateTime.Now
                        });
                    }
                }
                return new Response<List<LocalFileUploadResponse>>(true, "", filesUploaded);
            }
            catch (Exception ex)
            {
                return new Response<List<LocalFileUploadResponse>>(false, ex.ToString(), null);
            }
        }

        public async Task<Response<LocalFileUploadResponse>> UploadDocument(IFormFile file, string folder, bool keepFileName = false)
        {
            try
            {

                string path = RootDocumentUploadFolder;
                if (!string.IsNullOrEmpty(folder))
                {
                    path = Path.Combine(path, folder);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                }

                if (!file.IsValidDocument())
                {
                    return new Response<LocalFileUploadResponse>(false, "Invalid File Uploaded", null);
                }

                string newFileName = file.FileName;
                //check dir
                if (!keepFileName)
                {
                    newFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName).ToLower();
                }
                string url = Path.Combine(folder, newFileName).FixLocalUrlToDomainUrl();

                string fullPath = Path.Combine(path, newFileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    return new Response<LocalFileUploadResponse>(true, "", new LocalFileUploadResponse
                    {
                        FileName = file.FileName,
                        FileSize = file.Length,
                        FileUrl = url,
                        AddedOnDate = DateTime.Now,
                        LastModifiedDate = DateTime.Now
                    });
                }

            }
            catch (Exception ex)
            {
                return new Response<LocalFileUploadResponse>(false, ex.ToString(), null);
            }
        }

        public Response<List<LocalFileUploadResponse>> GetImagesFromFolder(string folderPath)
        {
            try
            {
                List<LocalFileUploadResponse> files = new List<LocalFileUploadResponse>();
                string path = Path.Combine(RootImageUploadFolder, folderPath);
                DirectoryInfo d = new DirectoryInfo(path);
                if (d.Exists)
                {
                    string[] Extensions = { "*.png", "*.jpg", "*.gif", "*.jpeg" };
                    foreach (var ext in Extensions)
                    {
                        FileInfo[] Files = d.GetFiles(ext);
                        foreach (FileInfo file in Files)
                        {
                            string url = Path.Combine(folderPath, file.Name).FixLocalUrlToDomainUrl();

                            files.Add(new LocalFileUploadResponse
                            {
                                FileUrl = url,
                                FileName = file.Name,
                                FileSize = file.Length,
                                AddedOnDate = file.CreationTime,
                                LastModifiedDate = file.LastWriteTime
                            });
                        }
                    }
                }
           
                return new Response<List<LocalFileUploadResponse>>(true, "", files);
            }
            catch (Exception ex)
            {
                return new Response<List<LocalFileUploadResponse>>(false, ex.ToString(), null);
            }
        }

        public Response<List<LocalFileUploadResponse>> GetDocumentsFromFolder(string folderPath)
        {
            try
            {
                List<LocalFileUploadResponse> files = new List<LocalFileUploadResponse>();
                string path = Path.Combine(RootDocumentUploadFolder, folderPath);
                DirectoryInfo d = new DirectoryInfo(path);
                if (d.Exists)
                {
                    string[] Extensions = { "*.doc", "*.docx", "*.xls", "*.xlsx", "*.pdf" };
                    foreach (var ext in Extensions)
                    {
                        FileInfo[] Files = d.GetFiles(ext);
                        foreach (FileInfo file in Files)
                        {
                            string url = Path.Combine(folderPath, file.Name).FixLocalUrlToDomainUrl();

                            files.Add(new LocalFileUploadResponse
                            {
                                FileUrl = url,
                                FileName = file.Name,
                                FileSize = file.Length,
                                AddedOnDate = file.CreationTime,
                                LastModifiedDate = file.LastWriteTime
                            });
                        }
                    }
                }
                else throw new NotFoundException("Folder", folderPath);
                return new Response<List<LocalFileUploadResponse>>(true, "", files);
            }
            catch (Exception ex)
            {
                return new Response<List<LocalFileUploadResponse>>(false, ex.ToString(), null);
            }
        }

        public async Task<object> UploadForCkEditor(IFormFile fileToUpload, string folder, string host)
        {
            try
            {

                folder = Path.Combine(folder, DateTime.Now.ToString("dd-MM-yyyy"));
                if (fileToUpload != null)
                {
                    if (fileToUpload.Length > 0)
                    {
                        int MaxContentLength = 1024 * 1024 * 5; //Size = 1 MB  
                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = fileToUpload.FileName.Substring(fileToUpload.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {
                            return new { error = new { message = "Please Upload image of type .jpg, .gif, .png." } };
                        }
                        if (fileToUpload.Length > MaxContentLength)
                        {
                            return new { error = new { message = "Please Upload a file upto 5 mb." } };
                        }
                        else
                        {
                            string path = Path.Combine(RootImageUploadFolder, folder.TrimEnd('/'));
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            string guidFileName = Guid.NewGuid().ToString() + ext;
                            string fullPath = Path.Combine(path, guidFileName);
                            string url = folder + "/" + guidFileName;
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                await fileToUpload.CopyToAsync(stream);
                                return new { url = "http://" + host + "/upload/images/" + url };
                            }
                        }

                    }
                    return new { error = new { message = "File size equals zero" } };
                }

                else
                {
                    return new { error = new { message = "File upload not found!" } };
                }

            }
            catch (Exception ex)
            {
                return new { error = new { message = ex.ToString() } };
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Application.Wrappers;

namespace API.Services
{
    //public interface IUploadService
    //{
    //    Task<object> UploadForCkEditor(IFormFile fileToUpload, string folder,string host);
    //    Task<Response<List<string>>> UploadFiles(List<IFormFile> files, IList<string> exts, int? maxLength, string rootPath, string folder);
    //    Task<Response<string>> ReplaceFileImage(string oldFile, IFormFile newFile, string rootPath);
    //    Response<int> DeleteFiles(List<string> files);
    //    Response<int> DeleteImage(string file);
    //    Response<List<FileResponseModel>> GetFilesFromFolder(string folderPath);
    //    Task<Response<List<FileResponseModel>>> UploadImages(List<IFormFile> files, string folder);
    //}
    //public class UploadService : IUploadService
    //{
    //    private readonly UploadSettings uploadSettings;
    //    public UploadService(IOptions<UploadSettings> _uploadSettings, IWebHostEnvironment env)
    //    {
    //        uploadSettings = _uploadSettings.Value;
    //        uploadSettings.ImageFolder = Path.Combine(env.WebRootPath, uploadSettings.ImageFolder);


    //    }
    //    public Response<int> DeleteFiles(List<string> files)
    //    {
    //        try
    //        {
    //            foreach (var file in files)
    //            {
    //                if (File.Exists(file))
    //                {
    //                    return new Response<int>(0, "Tệp không tồn tại: " + file, 0);
    //                }

    //            }
    //            foreach (var file in files)
    //            {
    //                File.Delete(file);
    //            }
    //            return new Response<int>(1, "", 1);
    //        }
    //        catch (Exception ex)
    //        {
    //            return new Response<int>(-1, ex.ToString(), -1);
    //        }
    //    }
    //    public Response<int> DeleteImage(string file)
    //    {
    //        try
    //        {
    //            string path = Path.Combine(uploadSettings.ImageFolder, file);
    //            if (!File.Exists(path))
    //            {
    //                return new Response<int>(0, "Tệp không tồn tại: " + path, 0);
    //            }
    //            File.Delete(path);
    //            return new Response<int>(1, "", 1);
    //        }
    //        catch (Exception ex)
    //        {
    //            return new Response<int>(-1, ex.ToString(), -1);
    //        }
    //    }

    //    public Response<List<FileResponseModel>> GetFilesFromFolder(string folderPath)
    //    {
    //        try
    //        {
    //            List<FileResponseModel> files = new List<FileResponseModel>();
    //            string path = Path.Combine(uploadSettings.ImageFolder, folderPath);
    //            DirectoryInfo d = new DirectoryInfo(path);
    //            if (d.Exists)
    //            {
    //                string[] Extensions = { "*.png", "*.jpg" };
    //                foreach (var ext in Extensions)
    //                {
    //                    FileInfo[] Files = d.GetFiles(ext);
    //                    foreach (FileInfo file in Files)
    //                    {
    //                        string url = Path.Combine(folderPath, file.Name);
    //                        url = url.Replace(@"\\", @"\");
    //                        url = url.Replace(@"\", "/");
    //                        files.Add(new FileResponseModel
    //                        {
    //                            FileUrl = url,
    //                            FileName = file.Name,
    //                            FileSize = file.Length,
    //                            LastModifiedDate = file.LastWriteTime
    //                        });
    //                    }
    //                }
    //            }
    //            return new Response<List<FileResponseModel>>(1, "", files, files.Count);
    //        }
    //        catch (Exception ex)
    //        {
    //            return new Response<List<FileResponseModel>>(-1, ex.ToString(), null);
    //        }
    //    }
    //    public async Task<Response<List<FileResponseModel>>> UploadImages(List<IFormFile> files, string folder)
    //    {
    //        try
    //        {
    //            List<FileResponseModel> filesUploaded = new List<FileResponseModel>();
    //            string path = Path.Combine(uploadSettings.ImageFolder, folder);
    //            foreach (var file in files)
    //            {
    //                IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".png" };
    //                var ext = file.FileName.Substring(file.FileName.LastIndexOf('.'));
    //                var extension = ext.ToLower();
    //                if (!AllowedFileExtensions.Contains(extension))
    //                {
    //                    continue;
    //                }
    //                if (file.Length == 0) continue;
    //                if (!Directory.Exists(path))
    //                {
    //                    Directory.CreateDirectory(path);
    //                }
    //                string fullPath = Path.Combine(path, file.FileName);
    //                if (File.Exists(fullPath))
    //                {
    //                    continue;
    //                }
    //                using (var stream = new FileStream(fullPath, FileMode.Create))
    //                {
    //                    string url = Path.Combine(folder, file.FileName);
    //                    url = url.Replace(@"\\", @"\");
    //                    url = url.Replace(@"\", "/");
    //                    await file.CopyToAsync(stream);
    //                    filesUploaded.Add(new FileResponseModel
    //                    {
    //                        FileName = file.FileName,
    //                        FileSize = file.Length,
    //                        FileUrl = url,
    //                        LastModifiedDate = DateTime.Now
    //                    });
    //                }
    //            }
    //            return new Response<List<FileResponseModel>>(1, "", filesUploaded, filesUploaded.Count);
    //        }
    //        catch (Exception ex)
    //        {
    //            return new Response<List<FileResponseModel>>(-1, ex.ToString(), null);
    //        }
    //    }

    //    public async Task<Response<string>> ReplaceFileImage(string oldFile, IFormFile newFile, string rootPath)
    //    {
    //        var ext = newFile.FileName.Substring(newFile.FileName.LastIndexOf('.'));
    //        var extension = ext.ToLower();
    //        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };

    //        if (!AllowedFileExtensions.Contains(extension))
    //        {
    //            return new Response<string>(0, "Please Upload image of type .jpg,.gif,.png.", null);
    //        }
    //        string newFileName = Guid.NewGuid().ToString() + ext;

    //        string currentFilePath = Path.Combine(rootPath, oldFile);
    //        if (currentFilePath.StartsWith(@"\"))
    //        {
    //            currentFilePath = currentFilePath.Substring(currentFilePath.IndexOf(@"\") + 1);
    //        }
    //        if (File.Exists(currentFilePath))
    //        {
    //            string folder = Path.GetDirectoryName(currentFilePath);
    //            string newFilePath = Path.Combine(folder, newFileName);
    //            string returnUrl = newFilePath.Replace(rootPath, "");
    //            if (returnUrl.StartsWith(@"\"))
    //            {
    //                returnUrl = returnUrl.Substring(returnUrl.IndexOf(@"\") + 1);
    //            }
    //            using (var stream = new FileStream(newFilePath, FileMode.Create))
    //            {
    //                await newFile.CopyToAsync(stream);
    //                File.Delete(currentFilePath);
    //                return new Response<string>(1, "", returnUrl);
    //            }
    //        }
    //        return new Response<string>(0, "", "File doesn't exisit!");
    //    }


    //    public async Task<Response<List<string>>> UploadFiles(List<IFormFile> files, IList<string> exts, int? maxLength, string rootPath, string folder)
    //    {
    //        try
    //        {
    //            List<string> filesUploaded = new List<string>();
    //            string path = Path.Combine(rootPath, folder);
    //            //check allowed exts
    //            foreach (var file in files)
    //            {
    //                var ext = file.FileName.Substring(file.FileName.LastIndexOf('.'));
    //                var extension = ext.ToLower();

    //                if (ext == ".pdf")
    //                {
    //                    string fullPath = Path.Combine(path, file.FileName);
    //                    if (File.Exists(fullPath))
    //                    {
    //                        return new Response<List<string>>(0, "Tệp " + file.FileName + " đã tồn tại", null);
    //                    }
    //                }
    //                if (exts.Count > 0)
    //                {
    //                    if (!exts.Contains(extension))
    //                    {

    //                        return new Response<List<string>>(0, "Định dạng không đúng", null);
    //                    }
    //                }


    //            }
    //            foreach (var file in files)
    //            {
    //                var ext = file.FileName.Substring(file.FileName.LastIndexOf('.'));
    //                var extension = ext.ToLower();
    //                //check dir
    //                if (!Directory.Exists(path))
    //                {
    //                    Directory.CreateDirectory(path);
    //                }

    //                string newFileName = Guid.NewGuid().ToString() + ext;

    //                if (ext == ".pdf")
    //                {
    //                    newFileName = file.FileName;
    //                }
    //                string url = Path.Combine(folder, newFileName);
    //                url = url.Replace(@"\\", @"\");
    //                url = url.Replace(@"\", "/");
    //                string fullPath = Path.Combine(path, newFileName);
    //                using (var stream = new FileStream(fullPath, FileMode.Create))
    //                {
    //                    await file.CopyToAsync(stream);
    //                    filesUploaded.Add(url);
    //                }
    //            }
    //            return new Response<List<string>>(1, "", filesUploaded, filesUploaded.Count);
    //        }
    //        catch (Exception ex)
    //        {
    //            return new Response<List<string>>(-1, ex.ToString(), null);
    //        }
    //    }
    //    public async Task<object> UploadForCkEditor(IFormFile fileToUpload, string folder, string host)
    //    {
    //        try
    //        {

    //            folder = Path.Combine(folder, DateTime.Now.ToString("dd-MM-yyyy"));
    //            if (fileToUpload != null)
    //            {
    //                if (fileToUpload.Length > 0)
    //                {
    //                    int MaxContentLength = 1024 * 1024 * 5; //Size = 1 MB  
    //                    IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
    //                    var ext = fileToUpload.FileName.Substring(fileToUpload.FileName.LastIndexOf('.'));
    //                    var extension = ext.ToLower();
    //                    if (!AllowedFileExtensions.Contains(extension))
    //                    {
    //                        return new { error = new { message = "Please Upload image of type .jpg, .gif, .png." } };
    //                    }
    //                    if (fileToUpload.Length > MaxContentLength)
    //                    {
    //                        return new { error = new { message = "Please Upload a file upto 5 mb." } };
    //                    }
    //                    else
    //                    {
    //                        string path = Path.Combine(uploadSettings.ImageFolder, folder.TrimEnd('/'));
    //                        if (!Directory.Exists(path))
    //                        {
    //                            Directory.CreateDirectory(path);
    //                        }
    //                        string guidFileName = Guid.NewGuid().ToString() + ext;
    //                        string fullPath = Path.Combine(path, guidFileName);
    //                        string url = folder + "/" + guidFileName;
    //                        using (var stream = new FileStream(fullPath, FileMode.Create))
    //                        {
    //                            await fileToUpload.CopyToAsync(stream);
    //                            return new { url = "http://"+host + "/upload/images/" + url };
    //                        }
    //                    }

    //                }
    //                return new { error = new { message = "File size equals zero" } };
    //            }

    //            else
    //            {
    //                return new { error = new { message = "File upload not found!" } };
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            return new { error = new { message = ex.ToString() } };
    //        }
    //    }

    //}
    //public class FileResponseModel
    //{
    //    public string FileName { get; set; }
    //    public double FileSize { get; set; }
    //    public string FileUrl { get; set; }
    //    public DateTime LastModifiedDate { get; set; }
    //}
}

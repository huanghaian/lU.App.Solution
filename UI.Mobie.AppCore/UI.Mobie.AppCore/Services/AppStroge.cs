using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UI.Mobie.BasicCore;

namespace UI.Mobie.AppCore.Services
{
    public class AppStroge : IAppStorage
    {
        private string _path;
        public AppStroge(string path)
        {
            _path = path ?? throw new ArgumentNullException(nameof(path));
            var chars = Path.GetInvalidPathChars();

            if (path.IndexOfAny(chars) != -1)
                throw new ArgumentException("路径字符串不正确。");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(_path + path);
        }

        public void DeleteDirectory(string path)
        {
            Directory.Delete(_path + path);
        }

        public void DeleteFile(string filePath)
        {
            File.Delete(_path + filePath);
        }

        public bool ExisitsDirectory(string path)
        {
            return Directory.Exists(path);
        }

        public bool ExistsFile(string filePath)
        {
            return File.Exists(_path + filePath);
        }

        public Stream CreateFile(string filePath)
        {
            return File.Create(_path + filePath);
        }

        public FileInfo GetFileInfo(string filePath)
        {
            return new FileInfo(_path + filePath);
        }

        public Stream GetFileStream(string filePath)
        {
            return File.Open(_path + filePath, FileMode.Open, FileAccess.ReadWrite);
        }
    }
}

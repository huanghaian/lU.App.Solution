using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UI.Mobie.BasicCore
{
    public interface IAppStorage
    {
        bool ExisitsDirectory(string path);
        void CreateDirectory(string path);
        void DeleteDirectory(string path);
        FileInfo GetFileInfo(string filePath);
        Stream GetFileStream(string filePath);
        bool ExistsFile(string filePath);
        Stream CreateFile(string filePath);
        void DeleteFile(string filePath);
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace searcher.Model
{
    public class FileInfoModel
    {
        public FileInfoModel(string filePath)
        {
            fileInfo = new FileInfo(filePath);
        }

        private FileInfo fileInfo;

        public string Name => fileInfo.Name;
        public string Path => fileInfo.FullName;

        public string Size => fileInfo.Length.ToString();
    }
}

using System;
using System.IO;

namespace Calamari.Terraform.Helpers
{
    public class DisposableDirectory : IDisposable
    {
        public DisposableDirectory()
        {
            DirectoryName = Create();
        }
        
        public string DirectoryName
        {
            get;
            private set;
        }
        
        string Create()
        {
            var tempDirectory = Path.GetTempFileName();
            // .NET ensures the name is unique, but we want a folder
            File.Delete(tempDirectory);
            // Create a folder instead
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            
            try
            {
                if (!string.IsNullOrWhiteSpace(DirectoryName) && Directory.Exists(DirectoryName))
                {
                    Directory.Delete(DirectoryName, true);
                }
            }
            catch
            {
                // ignore exceptions, we do the best we can to clean up, but if we leave
                // some files in temp then so be it.
            }
            finally 
            {
                DirectoryName = null;
            }
        }
    }
}
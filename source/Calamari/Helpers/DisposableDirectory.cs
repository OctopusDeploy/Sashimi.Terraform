using System;
using System.IO;

namespace Calamari.Terraform.Helpers
{
    public class DisposableDirectory : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TempStorage"/> class.
        /// </summary>
        /// <param name="path">The path to use as temp storage.</param>
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
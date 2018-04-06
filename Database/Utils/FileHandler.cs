using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Database.Utils
{

   public class FileHandler : IDisposable
    {
        private string _dir, _filename;
        private bool disposed = false;
        private SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        private FileStream fs;
        public string Dir { get => _dir; set => _dir = value; }
        public string Filename { get => _filename; set => _filename = value; }

        public FileHandler(string dir, string filename)
        {
            Dir = dir;
            Filename = filename;

            
            
        }

        public void Write(string text)
        {
            using (fs = new FileStream(Dir + "/" + Filename, FileMode.Append, FileAccess.Write))
            {
                StreamWriter writer = new StreamWriter(fs);
                writer.WriteLine(text);
                writer.Close();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            //Free managed resources
            if (disposing)
            {
                handle.Dispose();  
            }

            disposed = true;
        }
    }
}

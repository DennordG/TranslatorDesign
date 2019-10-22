using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslatorDesign.Helpers
{
    public class InputFileForValidation
    {
        public InputFileForValidation(string filePath)
        {
            FilePath = filePath;
            GetContent();
        }

        public string[] FileContent;

        public string FilePath;

        private void GetContent()
        {
            try
            {
                if (FileExist())
                {
                    FileContent = System.IO.File.ReadAllLines(FilePath);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("The content of the file could not be read: " + ex.InnerException.Message);
            }
        }

        private bool FileExist()
        {
            return System.IO.File.Exists(FilePath);
        }
    }
}

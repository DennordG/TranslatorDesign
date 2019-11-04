using System;
using System.IO;
using System.Linq;

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
                if (FileExists())
                {
                    FileContent = File.ReadAllLines(FilePath);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("The content of the file could not be read: " + ex.InnerException.Message);
            }
        }

        private bool FileExists()
        {
            return File.Exists(FilePath);
        }
    }
}

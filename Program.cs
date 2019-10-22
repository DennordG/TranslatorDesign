﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using TranslatorDesign.Helpers;
using TranslatorDesign.Tokenizer;

namespace TranslatorDesign
{
	public class Program
	{
        public static void Main(string[] args)
		{
            NameValueCollection appSettings;
            string pathInputFile = String.Empty;
            IList<Token> tokens = new List<Token>();

            try
            {
                appSettings = ConfigurationManager.AppSettings;
                pathInputFile = appSettings["InputFilePath"];            
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }

            if (pathInputFile != String.Empty)
            {
                var inputFile = new InputFileForValidation(pathInputFile);
                if(inputFile.FileContent != null)
                {
                    var tokenizer = new Tokenizer.Tokenizer(new ReservedRegexProvider());
                    tokens = tokenizer.Tokenize(inputFile.FileContent);
                }
            }
        }
	}
}

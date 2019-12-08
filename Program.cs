using System;
using System.Configuration;
using System.Linq;
using TranslatorDesign.Helpers;
using TranslatorDesign.Syntax;
using TranslatorDesign.Tokenizer;
using TranslatorDesign.Tokenizer.RegexProvider;

namespace TranslatorDesign
{
	public class Program
	{
		public static void Main(string[] args)
		{
			string pathInputFile;

			try
			{
				var appSettings = ConfigurationManager.AppSettings;
				pathInputFile = appSettings["InputFilePath"];
			}
			catch (ConfigurationErrorsException)
			{
				Console.WriteLine("Error reading app settings");
				return;
			}

			var inputFile = new InputFileForValidation(pathInputFile);
			if (inputFile.FileContent == null)
			{
				Console.WriteLine("File content could not be read (file may not exist)");
				return;
			}

			var tokenizer = new Tokenizer.Tokenizer(
				new ReservedRegexProvider(),
				new OperatorRegexProvider(),
				new SyntaxOperatorRegexProvider()
			);

			var tokens = tokenizer.Tokenize(inputFile.FileContent);

			if (tokens.Any(t => t.TokenType == TokenType.Invalid))
			{
				Console.WriteLine("Invalid tokens detected!");
				return;
			}

			var parser = new Parser(new Grammar(new GrammarRulesProvider()));
			var (couldParse, syntaxTree) = parser.Parse(tokens);

			if (!couldParse)
			{
				Console.WriteLine("Syntax errors!");
				return;
			}

			syntaxTree.Print();
            Console.ReadKey();
        }
	}
}

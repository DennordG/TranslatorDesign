using System;
using System.Linq;

namespace TranslatorDesign.Tokenizer
{
	public class ReservedRegexProvider
	{
		private readonly string[] _patterns;

		public ReservedRegexProvider()
		{
			_patterns = new[]
			{
				"int", "bool", "void", // data types
				"true", "false", // boolean values
				"if", "else", // conditional
				"while", // loops
				"return", // return
				"cin", "cout" // input/output
			};
		}

		public string GetPattern()
		{
			return string.Join("|", _patterns.Select(WrapPattern));
		}

		private string WrapPattern(string pattern)
		{
			return $"^{pattern}";
		}
	}
}

using System.Diagnostics;
using System.Linq;

namespace TranslatorDesign.Tokenizer
{
	[DebuggerDisplay("{TokenType} : {Value}")]
	public class Token
	{
		public Token(TokenType tokenType)
		{
			TokenType = tokenType;
		}

		public Token(TokenType tokenType, string value)
			: this(tokenType)
		{
			Value = value;
		}

		public TokenType TokenType { get; }
		public string Value { get; set; }

		public bool IsPunctuation()
		{
			return Value.All(char.IsPunctuation);
		}
	}
}

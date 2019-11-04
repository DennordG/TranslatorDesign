using System.Diagnostics;

namespace TranslatorDesign.Tokenizer
{
    [DebuggerDisplay("{TokenType} : {Value}")]
    public class Token
	{
		public Token(TokenType tokenType)
		{
			TokenType = tokenType;
			Value = string.Empty;
		}

		public Token(TokenType tokenType, string value)
		{
			TokenType = tokenType;
			Value = value;
		}

		public TokenType TokenType { get; }
		public string Value { get; set; }

		public Token Clone()
		{
			return new Token(TokenType, Value);
		}
	}
}

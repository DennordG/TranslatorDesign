using System.Collections.Generic;
using TranslatorDesign.Tokenizer;

namespace TranslatorDesign.Syntax
{
	public class TokenTypeFragment : IGrammarFragment
	{
		private readonly TokenType _tokenType;

		public TokenTypeFragment(TokenType tokenType)
		{
			_tokenType = tokenType;
		}

		public bool IsValid(Stack<Token> tokens)
		{
			if (_tokenType == tokens.Peek().TokenType)
			{
				tokens.Pop();
				return true;
			}

			return false;
		}
	}
}

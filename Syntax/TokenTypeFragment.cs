using System.Collections.Generic;
using System.Diagnostics;
using TranslatorDesign.Tokenizer;

namespace TranslatorDesign.Syntax
{
	[DebuggerDisplay("{_tokenType}")]
	public class TokenTypeFragment : IGrammarFragment
	{
		private readonly TokenType _tokenType;

		public TokenTypeFragment(TokenType tokenType)
		{
			_tokenType = tokenType;
		}

		public bool Validate(Stack<Token> tokens, SyntaxNode syntaxNode)
		{
			if (tokens.Count > 0 && _tokenType == tokens.Peek().TokenType)
			{
				syntaxNode.AddChild(new SyntaxNode(tokens.Pop().TokenType));

				return true;
			}

			return false;
		}
	}
}

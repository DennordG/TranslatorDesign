using System.Collections.Generic;
using System.Diagnostics;
using TranslatorDesign.Tokenizer;

namespace TranslatorDesign.Syntax
{
	[DebuggerDisplay("{" + nameof(_tokenType) + "}")]
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
				var token = tokens.Pop();
				syntaxNode.AddChild(new SyntaxNode(token.Value));

				return true;
			}

			return false;
		}
	}
}

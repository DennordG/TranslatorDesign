using System.Collections.Generic;
using System.Diagnostics;
using TranslatorDesign.Tokenizer;

namespace TranslatorDesign.Syntax
{
    [DebuggerDisplay("{" + nameof(_expectedValue) + "}")]
    public class ValueFragment : IGrammarFragment
    {
        private readonly string _expectedValue;

        public ValueFragment(string expectedValue)
        {
            _expectedValue = expectedValue;
        }

        public bool Validate(Stack<Token> tokens, SyntaxNode syntaxNode)
        {
            if (tokens.Count > 0 && _expectedValue == tokens.Peek().Value)
            {
				var token = tokens.Pop();
				if (!token.IsPunctuation())
				{
					syntaxNode.AddChild(new SyntaxNode(token.Value));
				}

                return true;
            }

            return false;
        }
    }
}

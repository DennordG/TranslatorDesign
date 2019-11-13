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

        public bool Validate(Stack<Token> tokens)
        {
            try
            {
                if (_tokenType == tokens.Peek().TokenType)
                {
                    tokens.Pop();
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}

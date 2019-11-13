using System.Collections.Generic;
using System.Diagnostics;
using TranslatorDesign.Tokenizer;

namespace TranslatorDesign.Syntax
{
    [DebuggerDisplay("{_expectedValue}")]
    public class ValueFragment : IGrammarFragment
    {
        private readonly string _expectedValue;

        public ValueFragment(string expectedValue)
        {
            _expectedValue = expectedValue;
        }

        public bool Validate(Stack<Token> tokens)
        {
            try
            {
                if (_expectedValue == tokens.Peek().Value)
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

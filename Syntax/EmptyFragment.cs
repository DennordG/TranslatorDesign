using System.Collections.Generic;
using TranslatorDesign.Tokenizer;

namespace TranslatorDesign.Syntax
{
    public class EmptyFragment : IGrammarFragment
    {
        public bool Validate(Stack<Token> tokens, SyntaxNode syntaxNode)
        {
            return true;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using TranslatorDesign.Tokenizer;

namespace TranslatorDesign.Syntax
{
    public class Parser
    {
        private Grammar _grammar;

        public Parser(Grammar grammar)
        {
            _grammar = grammar;
        }

        public bool Parse(IEnumerable<Token> tokens)
        {
            var tokenStack = new Stack<Token>(tokens.Reverse());

            return _grammar.Validate(tokenStack);
        }
    }
}

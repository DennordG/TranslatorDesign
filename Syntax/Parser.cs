using System.Collections.Generic;
using System.Linq;
using TranslatorDesign.Tokenizer;

namespace TranslatorDesign.Syntax
{
    public class Parser
    {
        private readonly Grammar _grammar;

        public Parser(Grammar grammar)
        {
            _grammar = grammar;
        }

        public bool Parse(IEnumerable<Token> tokens, out SyntaxTree syntaxTree)
		{
			syntaxTree = new SyntaxTree(new SyntaxNode(GrammarType.Program));
			var tokenStack = new Stack<Token>(tokens.Reverse());

			return _grammar.Validate(tokenStack, syntaxTree);
		}
    }
}

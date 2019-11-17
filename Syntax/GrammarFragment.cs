using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TranslatorDesign.Tokenizer;

namespace TranslatorDesign.Syntax
{
    [DebuggerDisplay("{_grammarFragments}")]
    public class GrammarFragment : IGrammarFragment
	{
		private readonly IEnumerable<IGrammarFragment> _grammarFragments;

		public GrammarFragment(IEnumerable<IGrammarFragment> grammarFragments)
		{
			_grammarFragments = grammarFragments;
		}

        public bool Validate(Stack<Token> tokens, SyntaxNode syntaxNode)
        {
            var tokensCopy = new Stack<Token>(tokens.Reverse());

            if (_grammarFragments.Any(f => !f.Validate(tokensCopy, syntaxNode)))
            {
                return false;
            }

            while (tokens.Count > tokensCopy.Count)
            {
                tokens.Pop();
            }

            return true;
        }
    }
}

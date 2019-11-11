using System.Collections.Generic;
using System.Linq;
using TranslatorDesign.Tokenizer;

namespace TranslatorDesign.Syntax
{
	public class GrammarFragment : IGrammarFragment
	{
		private readonly IEnumerable<IGrammarFragment> _grammarFragments;

		public GrammarFragment(IEnumerable<IGrammarFragment> grammarFragments)
		{
			_grammarFragments = grammarFragments;
		}

		public bool IsValid(Stack<Token> tokens)
		{
			Stack<Token> tokensCopy = new Stack<Token>(tokens.Reverse());

			foreach (var fragment in _grammarFragments)
			{
				if (tokensCopy.Count == 0 || !fragment.IsValid(tokensCopy))
				{
					return false;
				}
			}

			while (tokens.Count > tokensCopy.Count)
			{
				tokens.Pop();
			}

			return true;
		}
	}
}

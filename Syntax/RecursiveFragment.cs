using System;
using System.Collections.Generic;
using System.Linq;
using TranslatorDesign.Tokenizer;

namespace TranslatorDesign.Syntax
{
	public class RecursiveFragment : IGrammarFragment
	{
		private readonly GrammarType _grammarType;
		private readonly Func<IEnumerable<IGrammarFragment>> _grammarFragmentsProvider;

		public RecursiveFragment(GrammarType grammarType, Func<IEnumerable<IGrammarFragment>> action)
		{
			_grammarType = grammarType;
			_grammarFragmentsProvider = action;
		}

		public bool IsValid(Stack<Token> tokens)
		{
			Parser.CurrentDepth[_grammarType]++;

			Stack<Token> tokensCopy = new Stack<Token>(tokens.Reverse());
			var foundResult = false;

			foreach (var fragment in _grammarFragmentsProvider())
			{
				if (Parser.CurrentDepth[_grammarType] > Parser.MaxDepth
					|| tokensCopy.Count == 0
					|| !fragment.IsValid(tokensCopy))
				{
					tokensCopy = new Stack<Token>(tokens.Reverse());
				}
				else
				{
					foundResult = true;
					break;
				}
			}

			if (tokens.Count != tokensCopy.Count)
			{
				foundResult = true;
			}

			while (tokens.Count > tokensCopy.Count)
			{
				tokens.Pop();
			}

			Parser.CurrentDepth[_grammarType]--;
			return foundResult;
		}
	}
}

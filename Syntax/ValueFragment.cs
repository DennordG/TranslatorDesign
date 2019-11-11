using System.Collections.Generic;
using TranslatorDesign.Tokenizer;

namespace TranslatorDesign.Syntax
{
	public class ValueFragment : IGrammarFragment
	{
		private readonly string _expectedValue;

		public ValueFragment(string expectedValue)
		{
			_expectedValue = expectedValue;
		}

		public bool IsValid(Stack<Token> tokens)
		{
			if (_expectedValue == tokens.Peek().Value)
			{
				tokens.Pop();
				return true;
			}

			return false;
		}
	}
}

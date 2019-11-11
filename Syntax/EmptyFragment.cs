using System.Collections.Generic;
using TranslatorDesign.Tokenizer;

namespace TranslatorDesign.Syntax
{
	public class EmptyFragment : IGrammarFragment
	{
		public bool IsValid(Stack<Token> tokens)
		{
			return true;
		}
	}
}

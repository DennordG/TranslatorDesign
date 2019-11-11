using System.Collections.Generic;
using TranslatorDesign.Tokenizer;

namespace TranslatorDesign.Syntax
{
	public interface IGrammarFragment
	{
		bool IsValid(Stack<Token> tokens);
	}
}

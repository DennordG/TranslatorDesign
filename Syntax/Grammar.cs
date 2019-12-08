using System.Collections.Generic;
using System.Linq;
using TranslatorDesign.Tokenizer;

namespace TranslatorDesign.Syntax
{
	public class Grammar
    {
	    private readonly IEnumerable<IGrammarFragment> _mainRuleSet;

	    public Grammar(IGrammarRulesProvider grammarRulesProvider)
        {
	        _mainRuleSet = grammarRulesProvider.GetStartingGrammarRules();
        }

        public bool Validate(Stack<Token> tokenStack, SyntaxTree syntaxTree)
        {
			return _mainRuleSet.Any(r => r.Validate(tokenStack, syntaxTree.Root) && tokenStack.Count == 0);
        }
    }
}

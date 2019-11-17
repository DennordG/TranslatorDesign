using System.Collections.Generic;
using System.Linq;
using TranslatorDesign.Tokenizer;

namespace TranslatorDesign.Syntax
{
	public class Grammar
    {
        private readonly IDictionary<GrammarType, IEnumerable<IGrammarFragment>> _grammarRules;
        private readonly GrammarType _mainPoint;

        public Grammar(IGrammarRulesProvider grammarRulesProvider)
        {
            _grammarRules = grammarRulesProvider.GetRules();
            _mainPoint = grammarRulesProvider.GetMainGrammarType();
        }

        public bool Validate(Stack<Token> tokenStack, SyntaxTree syntaxTree)
        {
            var ruleSet = _grammarRules[_mainPoint];

			return ruleSet.Any(r => r.Validate(tokenStack, syntaxTree.Root) && tokenStack.Count == 0);
        }
    }
}

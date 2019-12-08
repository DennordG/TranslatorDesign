using System.Collections.Generic;

namespace TranslatorDesign.Syntax
{
    public interface IGrammarRulesProvider
    {
        IEnumerable<IGrammarFragment> GetStartingGrammarRules();
    }
}

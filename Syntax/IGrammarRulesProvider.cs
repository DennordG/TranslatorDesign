using System.Collections.Generic;

namespace TranslatorDesign.Syntax
{
    public interface IGrammarRulesProvider
    {
        IDictionary<GrammarType, IEnumerable<IGrammarFragment>> GetRules();

        GrammarType GetMainGrammarType();
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TranslatorDesign.Tokenizer;

namespace TranslatorDesign.Syntax
{
    [DebuggerDisplay("{_grammarType}")]
    public class RecursiveFragment : IGrammarFragment
    {
        private readonly GrammarType _grammarType;
        private readonly Func<IEnumerable<IGrammarFragment>> _grammarFragmentsProvider;

        public RecursiveFragment(GrammarType grammarType, Func<IEnumerable<IGrammarFragment>> action)
        {
            _grammarType = grammarType;
            _grammarFragmentsProvider = action;
        }

        public bool Validate(Stack<Token> tokens)
        {
            var tokensCopy = new Stack<Token>(tokens.Reverse());
            var foundResult = false;

            foreach (var fragment in _grammarFragmentsProvider())
            {
                if (!fragment.Validate(tokensCopy))
                {
                    tokensCopy = new Stack<Token>(tokens.Reverse());
                }
                else
                {
                    foundResult = true;
                    break;
                }
            }

            if (foundResult)
            {
                while (tokens.Count > tokensCopy.Count)
                {
                    tokens.Pop();
                }
            }

            return foundResult;
        }
    }
}

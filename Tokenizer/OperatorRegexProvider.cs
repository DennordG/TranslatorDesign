using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslatorDesign.Tokenizer
{
    public class OperatorRegexProvider
    {
        public static string DefaultWrap(string pattern)
        {
            return $@"^{pattern}\b[\n\s\t]*";
        }
    }
}

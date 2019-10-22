using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslatorDesign.Tokenizer
{
    public class OperatorRegexProvider
    {
        private readonly string _pattern;

        public OperatorRegexProvider()
        {
            _pattern = @"^==|!=|<=|>=|<<|>>|\+|-|\*|\/|&&|\|\||<|>|!";
        }

        public string GetPattern()
        {
            return string.Join("|", _pattern);
        }
    }
}

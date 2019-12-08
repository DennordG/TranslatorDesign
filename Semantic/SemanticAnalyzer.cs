using TranslatorDesign.Syntax;

namespace TranslatorDesign.Semantic
{
	public class SemanticAnalyzer
	{
		private readonly NameAnalyzer _nameAnalyzer;

		public SemanticAnalyzer()
		{
			_nameAnalyzer = new NameAnalyzer();
		}

		public void ValidateAndThrow(SyntaxTree syntaxTree)
		{
			_nameAnalyzer.PerformNameAnalysis(syntaxTree);
		}
	}
}

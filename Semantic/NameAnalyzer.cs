using System;
using System.Linq;
using TranslatorDesign.Syntax;

namespace TranslatorDesign.Semantic
{
	public class NameAnalyzer
	{
		private readonly SymbolTable _symbolTable;

		public NameAnalyzer()
		{
			_symbolTable = new SymbolTable();
		}

		public void PerformNameAnalysis(SyntaxTree syntaxTree)
		{
			foreach (var node in syntaxTree.Traverse())
			{
				if (node.GrammarType == GrammarType.IdDecl)
				{
					var identifier = node.Children.First();

					if (!_symbolTable.ExistsAtDepth(identifier.Depth, identifier.Value))
					{
						_symbolTable.AddDecl(identifier.Depth, identifier.Value);
					}
					else
					{
						throw new Exception($"Found multiple declarations of	'{identifier.Value}'.");
					}
				}
				else if (node.GrammarType == GrammarType.IdUse)
				{
					var identifier = node.Children.First();

					if (!_symbolTable.Exists(identifier.Depth, identifier.Value))
					{
						throw new Exception($"Found undeclared identifier '{identifier.Value}'.");
					}
				}
			}
		}
	}
}
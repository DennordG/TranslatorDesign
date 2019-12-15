using System;
using System.Collections.Generic;
using System.Linq;
using TranslatorDesign.Syntax;

namespace TranslatorDesign.Semantic
{
	public class NameAnalyzer
	{
		private readonly SymbolTable _symbolTable;

		private static readonly GrammarType?[] DeclParentTypes =
		{
			GrammarType.Block, GrammarType.Parameters, GrammarType.Program
		};

		public NameAnalyzer()
		{
			_symbolTable = new SymbolTable();
		}

		public void PerformNameAnalysis(SyntaxTree syntaxTree)
		{
			var exceptions = new List<Exception>();

			foreach (var node in syntaxTree.Traverse())
			{
				if (node.GrammarType == GrammarType.IdDecl)
				{
					var identifier = node.Children.First();
					var depth = GetDepthOfDeclParent(identifier) ?? identifier.Depth;

					if (!_symbolTable.ExistsAtDepth(depth, identifier.Value))
					{
						_symbolTable.AddDecl(depth, identifier.Value);
					}
					else
					{
						exceptions.Add(new Exception($"Found multiple declarations of '{identifier.Value}'."));
					}
				}
				else if (node.GrammarType == GrammarType.IdUse)
				{
					var identifier = node.Children.First();

					if (!_symbolTable.Exists(identifier.Depth, identifier.Value))
					{
						exceptions.Add(new Exception($"Found undeclared identifier '{identifier.Value}'."));
					}
				}
				else if (node.GrammarType != null)
				{
					_symbolTable.Clear(node.Depth + 1);
				}
			}

			if (exceptions.Count > 0)
			{
				throw new AggregateException(exceptions);
			}
		}

		private static int? GetDepthOfDeclParent(SyntaxNode identifier)
		{
			while (identifier != null && !IsDeclParentType(identifier.GrammarType))
			{
				identifier = identifier.Parent;
			}

			return identifier?.Depth;
		}

		private static bool IsDeclParentType(GrammarType? grammarType)
		{
			return DeclParentTypes.Contains(grammarType);
		}
	}
}
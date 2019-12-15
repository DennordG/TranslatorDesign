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

		public NameAnalyzer(SymbolTable symbolTable)
		{
			_symbolTable = symbolTable;
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

					var symbolInfo = _symbolTable.GetDeclByIdAtDepth(depth, identifier.Value);
					if (symbolInfo == null)
					{
						var mainDecl = identifier.Parent.Parent;
						var declInfo = GetDeclInfo(mainDecl);
						var parameters = GetParameters(mainDecl);

						_symbolTable.AddDecl(depth, declInfo, parameters);
					}
					else
					{
						exceptions.Add(new Exception($"Found multiple declarations of '{identifier.Value}'."));
					}
				}
				else if (node.GrammarType == GrammarType.IdUse)
				{
					var identifier = node.Children.First();

					var symbolInfo = _symbolTable.GetDeclById(identifier.Depth, identifier.Value);
					if (symbolInfo == null)
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

		private static IList<DeclarationInfo> GetParameters(SyntaxNode decl)
		{
			IList<DeclarationInfo> parameters = null;

			var mainParameters = decl.Children.FirstOrDefault(c => c.GrammarType == GrammarType.Parameters);
			if (mainParameters != null)
			{
				var formalsList = mainParameters.Children.First().Children;
				parameters = formalsList.Select(GetDeclInfo).ToList();
			}

			return parameters;
		}

		private static DeclarationInfo GetDeclInfo(SyntaxNode decl)
		{
			var declType = decl.Children.FirstOrDefault(c => c.GrammarType == GrammarType.Type)?.Children.First().Value;
			var declId = decl.Children.FirstOrDefault(c => c.GrammarType == GrammarType.IdDecl)?.Children.First().Value;

			return new DeclarationInfo(declType, declId);
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
using System;
using System.Collections.Generic;
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
			var exceptions = new List<Exception>();

			try
			{
				_nameAnalyzer.PerformNameAnalysis(syntaxTree);
			}
			catch (AggregateException aggException)
			{
				exceptions.AddRange(aggException.InnerExceptions);
			}

			if (exceptions.Count > 0)
			{
				throw new AggregateException(exceptions);
			}
		}
	}
}

using System.Collections.Generic;
using TranslatorDesign.Tokenizer;

namespace TranslatorDesign.Syntax
{
	public class Grammar
	{
		public IDictionary<GrammarType, IEnumerable<IGrammarFragment>> GrammarRules { get; }

		public Grammar()
		{
			GrammarRules = new Dictionary<GrammarType, IEnumerable<IGrammarFragment>>
			{
				{ GrammarType.Program, CreateProgramGrammar() },
				{ GrammarType.VarDecl, CreateVarDeclGrammar() },
				{ GrammarType.FnDecl, CreateFnDeclGrammar() },
				{ GrammarType.Parameters, CreateParametersGrammar() },
				{ GrammarType.FormalsList, CreateFormalsListGrammar() },
				{ GrammarType.FormalDecl, CreateFormalDeclGrammar() },
				{ GrammarType.Block, CreateBlockGrammar() },
				{ GrammarType.DeclList, CreateDeclListGrammar() },
				{ GrammarType.StmtList, CreateStmtListGrammar() },
				{ GrammarType.Stmt, CreateStmtGrammar() },
				{ GrammarType.Exp, CreateExpGrammar() },
				{ GrammarType.Atom, CreateAtomGrammar() },
				{ GrammarType.FnCallExpr, CreateFnCallExprGrammar() },
				{ GrammarType.FnCallStmt, CreateFnCallStmtGrammar() },
				{ GrammarType.ActualList, CreateActualListGrammar() },
				{ GrammarType.SubscriptExpr, CreateSubscriptExprGrammar() },
				{ GrammarType.Type, CreateTypeGrammar() },
				{ GrammarType.Id, CreateIdGrammar() },
			};
		}

		#region Grammar creation rules
		private IEnumerable<IGrammarFragment> CreateProgramSecondGrammar()
		{
			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new RecursiveFragment(GrammarType.FnDecl, CreateFnDeclGrammar),
				new RecursiveFragment(GrammarType.Program, CreateProgramSecondGrammar)
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new RecursiveFragment(GrammarType.VarDecl, CreateVarDeclGrammar),
				new RecursiveFragment(GrammarType.Program, CreateProgramSecondGrammar)
			});		

			yield return new EmptyFragment();
		}

		private IEnumerable<IGrammarFragment> CreateProgramGrammar()
		{
			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new RecursiveFragment(GrammarType.Program, CreateProgramSecondGrammar)
			});
		}

		private IEnumerable<IGrammarFragment> CreateVarDeclGrammar()
		{
			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new RecursiveFragment(GrammarType.Type, CreateTypeGrammar),
				new RecursiveFragment(GrammarType.Id, CreateIdGrammar),
				new ValueFragment(";")
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new RecursiveFragment(GrammarType.Type, CreateTypeGrammar),
				new RecursiveFragment(GrammarType.Id, CreateIdGrammar),
				new ValueFragment("["),
				new TokenTypeFragment(TokenType.Integer),
				new ValueFragment("]"),
				new ValueFragment(";")
			});
		}

		private IEnumerable<IGrammarFragment> CreateTypeGrammar()
		{
			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment("int")
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment("bool")
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment("void")
			});
		}

		private IEnumerable<IGrammarFragment> CreateFnDeclGrammar()
		{
			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new RecursiveFragment(GrammarType.Type, CreateTypeGrammar),
				new RecursiveFragment(GrammarType.Id, CreateIdGrammar),
				new RecursiveFragment(GrammarType.Parameters, CreateParametersGrammar),
				new RecursiveFragment(GrammarType.Block, CreateBlockGrammar)
			});
		}

		private IEnumerable<IGrammarFragment> CreateParametersGrammar()
		{
			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment("("),
				new ValueFragment(")")
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment("("),
				new RecursiveFragment(GrammarType.FormalsList, CreateFormalsListGrammar),
				new ValueFragment(")")
			});
		}

		private IEnumerable<IGrammarFragment> CreateFormalsListSecondGrammar()
		{
			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment(","),
				new RecursiveFragment(GrammarType.FormalDecl, CreateFormalDeclGrammar),
				new RecursiveFragment(GrammarType.FormalsList, CreateFormalsListSecondGrammar)
			});

			yield return new EmptyFragment();
		}

		private IEnumerable<IGrammarFragment> CreateFormalsListGrammar()
		{
			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new RecursiveFragment(GrammarType.FormalDecl, CreateFormalDeclGrammar),
				new RecursiveFragment(GrammarType.FormalsList, CreateFormalsListSecondGrammar)
			});
		}

		private IEnumerable<IGrammarFragment> CreateFormalDeclGrammar()
		{
			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new RecursiveFragment(GrammarType.Type, CreateTypeGrammar),
				new RecursiveFragment(GrammarType.Id, CreateIdGrammar)
			});
		}

		private IEnumerable<IGrammarFragment> CreateBlockGrammar()
		{
			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment("{"),
				new RecursiveFragment(GrammarType.DeclList, CreateDeclListGrammar),
				new RecursiveFragment(GrammarType.StmtList, CreateStmtListGrammar),
				new ValueFragment("}")
			});
		}

		private IEnumerable<IGrammarFragment> CreateDeclListSecondGrammar()
		{
			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new RecursiveFragment(GrammarType.VarDecl, CreateVarDeclGrammar),
				new RecursiveFragment(GrammarType.DeclList, CreateDeclListSecondGrammar),
			});

			yield return new EmptyFragment();
		}

		private IEnumerable<IGrammarFragment> CreateDeclListGrammar()
		{
			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new RecursiveFragment(GrammarType.DeclList, CreateDeclListSecondGrammar)
			});
		}

		private IEnumerable<IGrammarFragment> CreateStmtListSecondGrammar()
		{
			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new RecursiveFragment(GrammarType.Stmt, CreateStmtGrammar),
				new RecursiveFragment(GrammarType.StmtList, CreateStmtListSecondGrammar),
			});

			yield return new EmptyFragment();
		}

		private IEnumerable<IGrammarFragment> CreateStmtListGrammar()
		{
			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new RecursiveFragment(GrammarType.StmtList, CreateStmtListSecondGrammar)
			});
		}

		private IEnumerable<IGrammarFragment> CreateStmtGrammar()
		{
			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment("cin"),
				new ValueFragment(">>"),
				new RecursiveFragment(GrammarType.Id, CreateIdGrammar),
				new ValueFragment(";")
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment("cin"),
				new ValueFragment(">>"),
				new RecursiveFragment(GrammarType.Id, CreateIdGrammar),
				new ValueFragment("["),
				new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
				new ValueFragment("]"),
				new ValueFragment(";")
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment("cout"),
				new ValueFragment("<<"),
				new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
				new ValueFragment(";")
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new RecursiveFragment(GrammarType.SubscriptExpr, CreateSubscriptExprGrammar),
				new ValueFragment("="),
				new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
				new ValueFragment(";")
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new RecursiveFragment(GrammarType.Id, CreateIdGrammar),
				new ValueFragment("="),
				new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
				new ValueFragment(";")
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment("if"),
				new ValueFragment("("),
				new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
				new ValueFragment(")"),
				new RecursiveFragment(GrammarType.Block, CreateBlockGrammar)
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment("if"),
				new ValueFragment("("),
				new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
				new ValueFragment(")"),
				new RecursiveFragment(GrammarType.Block, CreateBlockGrammar),
				new ValueFragment("else"),
				new RecursiveFragment(GrammarType.Block, CreateBlockGrammar)
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment("while"),
				new ValueFragment("("),
				new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
				new ValueFragment(")"),
				new RecursiveFragment(GrammarType.Block, CreateBlockGrammar)
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment("return"),
				new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
				new ValueFragment(";")
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment("return"),
				new ValueFragment(";")
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new RecursiveFragment(GrammarType.FnCallStmt, CreateFnCallStmtGrammar),
				new ValueFragment(";")
			});
		}

		private IEnumerable<IGrammarFragment> CreateExpSecondGrammar() 
		{
			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment("+"),
				new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
				new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar),
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment("-"),
				new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
				new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar),
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment("*"),
				new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
				new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar),
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment("/"),
				new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
				new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar),
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment("&&"),
				new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
				new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar),
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment("||"),
				new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
				new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar),
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment("=="),
				new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
				new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar),
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment("!="),
				new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
				new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar),
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment("<"),
				new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
				new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar),
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment(">"),
				new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
				new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar),
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment("<="),
				new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
				new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar),
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment(">="),
				new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
				new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar),
			});

			yield return new EmptyFragment();
		}

		private IEnumerable<IGrammarFragment> CreateExpGrammar()
		{
			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment("!"),
				new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
				new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar)
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment("-"),
				new RecursiveFragment(GrammarType.Atom, CreateAtomGrammar),
				new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar)
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new RecursiveFragment(GrammarType.Atom, CreateAtomGrammar),
				new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar)
			});
		}

		private IEnumerable<IGrammarFragment> CreateAtomGrammar()
		{
			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new TokenTypeFragment(TokenType.Integer)
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new TokenTypeFragment(TokenType.String)
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment("true")
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment("false")
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment("("),
				new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
				new ValueFragment(")")
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new RecursiveFragment(GrammarType.FnCallExpr, CreateFnCallExprGrammar)
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new RecursiveFragment(GrammarType.SubscriptExpr, CreateSubscriptExprGrammar)
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new RecursiveFragment(GrammarType.Id, CreateIdGrammar)
			});
		}

		private IEnumerable<IGrammarFragment> CreateFnCallExprGrammar()
		{
			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new RecursiveFragment(GrammarType.Id, CreateIdGrammar),
				new ValueFragment("("),
				new ValueFragment(")")
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new RecursiveFragment(GrammarType.Id, CreateIdGrammar),
				new ValueFragment("("),
				new RecursiveFragment(GrammarType.ActualList, CreateActualListGrammar),
				new ValueFragment(")")
			});
		}

		private IEnumerable<IGrammarFragment> CreateFnCallStmtGrammar()
		{
			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new RecursiveFragment(GrammarType.Id, CreateIdGrammar),
				new ValueFragment("("),
				new ValueFragment(")")
			});

			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new RecursiveFragment(GrammarType.Id, CreateIdGrammar),
				new ValueFragment("("),
				new RecursiveFragment(GrammarType.ActualList, CreateActualListGrammar),
				new ValueFragment(")")
			});
		}

		private IEnumerable<IGrammarFragment> CreateActualListSecondGrammar()
		{
			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new ValueFragment(","),
				new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
				new RecursiveFragment(GrammarType.ActualList, CreateActualListSecondGrammar)
			});

			yield return new EmptyFragment();
		}

		private IEnumerable<IGrammarFragment> CreateActualListGrammar()
		{
			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
				new RecursiveFragment(GrammarType.ActualList, CreateActualListSecondGrammar)
			});
		}

		private IEnumerable<IGrammarFragment> CreateSubscriptExprGrammar()
		{
			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new RecursiveFragment(GrammarType.Id, CreateIdGrammar),
				new ValueFragment("["),
				new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
				new ValueFragment("[")
			});
		}

		private IEnumerable<IGrammarFragment> CreateIdGrammar()
		{
			yield return new GrammarFragment(new List<IGrammarFragment>
			{
				new TokenTypeFragment(TokenType.Identifier)
			});
		}
		#endregion
	}
}

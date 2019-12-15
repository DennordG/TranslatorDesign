using System.Collections.Generic;
using TranslatorDesign.Tokenizer;

namespace TranslatorDesign.Syntax
{
	public class GrammarRulesProvider : IGrammarRulesProvider
	{
		public IEnumerable<IGrammarFragment> GetStartingGrammarRules()
		{
			return CreateProgramGrammar();
		}

		#region Grammar creation rules
		private IEnumerable<IGrammarFragment> CreateProgramGrammar()
		{
			return new List<IGrammarFragment>
			{
				new GrammarFragment(new List<IGrammarFragment>
				{
					new RecursiveFragment(GrammarType.FnDecl, CreateFnDeclGrammar),
					new RecursiveFragment(GrammarType.Program, CreateProgramGrammar, addNewNode: false)
				}),
				new GrammarFragment(new List<IGrammarFragment>
				{
					new RecursiveFragment(GrammarType.VarDecl, CreateVarDeclGrammar),
					new RecursiveFragment(GrammarType.Program, CreateProgramGrammar, addNewNode: false)
				}),
				new EmptyFragment()
			};
		}

		private IEnumerable<IGrammarFragment> CreateVarDeclGrammar()
		{
			return new List<IGrammarFragment>
			{
				new GrammarFragment(new List<IGrammarFragment>
				{
					new RecursiveFragment(GrammarType.Type, CreateTypeGrammar),
					new RecursiveFragment(GrammarType.IdDecl, CreateIdGrammar),
					new ValueFragment(";")
				}),
				new GrammarFragment(new List<IGrammarFragment>
				{
					new RecursiveFragment(GrammarType.Type, CreateTypeGrammar),
					new RecursiveFragment(GrammarType.IdDecl, CreateIdGrammar),
					new ValueFragment("["),
					new TokenTypeFragment(TokenType.Integer),
					new ValueFragment("]"),
					new ValueFragment(";")
				})
			};
		}

		private IEnumerable<IGrammarFragment> CreateTypeGrammar()
		{
			return new List<IGrammarFragment>
			{
				new ValueFragment("int"),
				new ValueFragment("bool"),
				new ValueFragment("void")
			};
		}

		private IEnumerable<IGrammarFragment> CreateFnDeclGrammar()
		{
			return new List<IGrammarFragment>
			{
				new GrammarFragment(new List<IGrammarFragment>
				{
					new RecursiveFragment(GrammarType.Type, CreateTypeGrammar),
					new RecursiveFragment(GrammarType.IdDecl, CreateIdGrammar),
					new RecursiveFragment(GrammarType.Parameters, CreateParametersGrammar),
					new RecursiveFragment(GrammarType.Block, CreateBlockGrammar)
				})
			};
		}

		private IEnumerable<IGrammarFragment> CreateParametersGrammar()
		{
			return new List<IGrammarFragment>
			{
				new GrammarFragment(new List<IGrammarFragment>
				{
					new ValueFragment("("),
					new ValueFragment(")")
				}),
				new GrammarFragment(new List<IGrammarFragment>
				{
					new ValueFragment("("),
					new RecursiveFragment(GrammarType.FormalsList, CreateFormalsListGrammar),
					new ValueFragment(")")
				})
			};
		}

		private IEnumerable<IGrammarFragment> CreateFormalsListGrammar()
		{
			return new List<IGrammarFragment>
			{
				new GrammarFragment(new List<IGrammarFragment>
				{
					new RecursiveFragment(GrammarType.FormalDecl, CreateFormalDeclGrammar),
					new ValueFragment(","),
					new RecursiveFragment(GrammarType.FormalDecl, CreateFormalDeclGrammar)
				}),
				new GrammarFragment(new List<IGrammarFragment>
				{
					new RecursiveFragment(GrammarType.FormalDecl, CreateFormalDeclGrammar)
				})
			};
		}

		private IEnumerable<IGrammarFragment> CreateFormalDeclGrammar()
		{
			return new List<IGrammarFragment>
			{
				new GrammarFragment(new List<IGrammarFragment>
				{
					new RecursiveFragment(GrammarType.Type, CreateTypeGrammar),
					new RecursiveFragment(GrammarType.IdDecl, CreateIdGrammar)
				})
			};
		}

		private IEnumerable<IGrammarFragment> CreateBlockGrammar()
		{
			return new List<IGrammarFragment>
			{
				new GrammarFragment(new List<IGrammarFragment>
				{
					new ValueFragment("{"),
					new RecursiveFragment(GrammarType.DeclList, CreateDeclListGrammar),
					new RecursiveFragment(GrammarType.StmtList, CreateStmtListGrammar),
					new ValueFragment("}")
				})
			};
		}

		private IEnumerable<IGrammarFragment> CreateDeclListGrammar()
		{
			return new List<IGrammarFragment>
			{
				new GrammarFragment(new List<IGrammarFragment>
				{
					new RecursiveFragment(GrammarType.VarDecl, CreateVarDeclGrammar),
					new RecursiveFragment(GrammarType.DeclList, CreateDeclListGrammar, addNewNode: false),
				}),
				new EmptyFragment()
			};
		}

		private IEnumerable<IGrammarFragment> CreateStmtListGrammar()
		{
			return new List<IGrammarFragment>
			{
				new GrammarFragment(new List<IGrammarFragment>
				{
					new RecursiveFragment(GrammarType.Stmt, CreateStmtGrammar),
					new RecursiveFragment(GrammarType.StmtList, CreateStmtListGrammar, addNewNode: false),
				}),
				new EmptyFragment()
			};
		}

		private IEnumerable<IGrammarFragment> CreateStmtGrammar()
		{
			return new List<IGrammarFragment>
			{
				new GrammarFragment(new List<IGrammarFragment>
				{
					new ValueFragment("cin"),
					new ValueFragment(">>"),
					new RecursiveFragment(GrammarType.IdUse, CreateIdGrammar),
					new ValueFragment(";")
				}),
				new GrammarFragment(new List<IGrammarFragment>
				{
					new ValueFragment("cin"),
					new ValueFragment(">>"),
					new RecursiveFragment(GrammarType.IdUse, CreateIdGrammar),
					new ValueFragment("["),
					new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
					new ValueFragment("]"),
					new ValueFragment(";")
				}),
				new GrammarFragment(new List<IGrammarFragment>
				{
					new ValueFragment("cout"),
					new ValueFragment("<<"),
					new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
					new ValueFragment(";")
				}),
				new GrammarFragment(new List<IGrammarFragment>
				{
					new RecursiveFragment(GrammarType.SubscriptExpr, CreateSubscriptExprGrammar),
					new ValueFragment("="),
					new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
					new ValueFragment(";")
				}),
				new GrammarFragment(new List<IGrammarFragment>
				{
					new RecursiveFragment(GrammarType.IdUse, CreateIdGrammar),
					new ValueFragment("="),
					new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
					new ValueFragment(";")
				}),
				new GrammarFragment(new List<IGrammarFragment>
				{
					new ValueFragment("if"),
					new ValueFragment("("),
					new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
					new ValueFragment(")"),
					new RecursiveFragment(GrammarType.Block, CreateBlockGrammar)
				}),
				new GrammarFragment(new List<IGrammarFragment>
				{
					new ValueFragment("if"),
					new ValueFragment("("),
					new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
					new ValueFragment(")"),
					new RecursiveFragment(GrammarType.Block, CreateBlockGrammar),
					new ValueFragment("else"),
					new RecursiveFragment(GrammarType.Block, CreateBlockGrammar)
				}),
				new GrammarFragment(new List<IGrammarFragment>
				{
					new ValueFragment("while"),
					new ValueFragment("("),
					new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
					new ValueFragment(")"),
					new RecursiveFragment(GrammarType.Block, CreateBlockGrammar)
				}),
				new GrammarFragment(new List<IGrammarFragment>
				{
					new ValueFragment("return"),
					new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
					new ValueFragment(";")
				}),
				new GrammarFragment(new List<IGrammarFragment>
				{
					new ValueFragment("return"),
					new ValueFragment(";")
				}),
				new GrammarFragment(new List<IGrammarFragment>
				{
					new RecursiveFragment(GrammarType.FnCallStmt, CreateFnCallStmtGrammar),
					new ValueFragment(";")
				})
			};
		}

		private IEnumerable<IGrammarFragment> CreateOrOrOperatorGrammar()
		{
			return new List<IGrammarFragment>
			{
				new ValueFragment("||"),
			};
		}

		private IEnumerable<IGrammarFragment> CreateAndAndOperatorGrammar()
		{
			return new List<IGrammarFragment>
			{
				new ValueFragment("&&"),
			};
		}

		private IEnumerable<IGrammarFragment> CreateComparisonOperatorGrammar()
		{
			return new List<IGrammarFragment>
			{
				new ValueFragment("=="),
				new ValueFragment("!="),
				new ValueFragment("<"),
				new ValueFragment(">"),
				new ValueFragment("<="),
				new ValueFragment(">="),
			};
		}

		private IEnumerable<IGrammarFragment> CreatePlusMinusOperatorGrammar()
		{
			return new List<IGrammarFragment>
			{
				new ValueFragment("+"),
				new ValueFragment("-"),
			};
		}

		private IEnumerable<IGrammarFragment> CreateMultDivOperatorGrammar()
		{
			return new List<IGrammarFragment>
			{
				new ValueFragment("*"),
				new ValueFragment("/"),
			};
		}

		private IEnumerable<IGrammarFragment> CreateUnaryOperatorGrammar()
		{
			return new List<IGrammarFragment>
			{
				new ValueFragment("!"),
				new ValueFragment("-"),
			};
		}

		private IEnumerable<IGrammarFragment> CreateOperatorsGrammar()
		{
			return new List<IGrammarFragment>
			{
				new RecursiveFragment(GrammarType.LogicOrOp, CreateOrOrOperatorGrammar, addNewNode: false),
				new RecursiveFragment(GrammarType.LogicAndOp, CreateAndAndOperatorGrammar, addNewNode: false),
				new RecursiveFragment(GrammarType.ComparisonOp, CreateComparisonOperatorGrammar, addNewNode: false),
				new RecursiveFragment(GrammarType.PlusMinusOp, CreatePlusMinusOperatorGrammar, addNewNode: false),
				new RecursiveFragment(GrammarType.MultDivOp, CreateMultDivOperatorGrammar, addNewNode: false),
				new RecursiveFragment(GrammarType.UnaryOp, CreateUnaryOperatorGrammar, addNewNode: false),
			};
		}

		private IEnumerable<IGrammarFragment> CreateExp1Grammar()
		{
			return new List<IGrammarFragment>
			{
				new GrammarFragment(new List<IGrammarFragment>
				{
					new RecursiveFragment(GrammarType.Exp, CreateExp2Grammar),
					new RecursiveFragment(GrammarType.LogicOrOp, CreateOrOrOperatorGrammar, addNewNode: false),
					new RecursiveFragment(GrammarType.Exp, CreateExp2Grammar)
				}),
				new RecursiveFragment(GrammarType.Exp, CreateExp2Grammar, addNewNode: false)
			};
		}

		private IEnumerable<IGrammarFragment> CreateExp2Grammar()
		{
			return new List<IGrammarFragment>
			{
				new GrammarFragment(new List<IGrammarFragment>
				{
					new RecursiveFragment(GrammarType.Exp, CreateExp3Grammar),
					new RecursiveFragment(GrammarType.LogicAndOp, CreateAndAndOperatorGrammar, addNewNode: false),
					new RecursiveFragment(GrammarType.Exp, CreateExp3Grammar),
				}),
				new RecursiveFragment(GrammarType.Exp, CreateExp3Grammar, addNewNode: false)
			};
		}

		private IEnumerable<IGrammarFragment> CreateExp3Grammar()
		{
			return new List<IGrammarFragment>
			{
				new GrammarFragment(new List<IGrammarFragment>
				{
					new RecursiveFragment(GrammarType.Exp, CreateExp4Grammar),
					new RecursiveFragment(GrammarType.ComparisonOp, CreateComparisonOperatorGrammar, addNewNode: false),
					new RecursiveFragment(GrammarType.Exp, CreateExp4Grammar),
				}),
				new RecursiveFragment(GrammarType.Exp, CreateExp4Grammar, addNewNode: false)
			};
		}

		private IEnumerable<IGrammarFragment> CreateExp4Grammar()
		{
			return new List<IGrammarFragment>
			{
				new GrammarFragment(new List<IGrammarFragment>
				{
					new RecursiveFragment(GrammarType.Exp, CreateExp5Grammar),
					new RecursiveFragment(GrammarType.PlusMinusOp, CreatePlusMinusOperatorGrammar, addNewNode: false),
					new RecursiveFragment(GrammarType.Exp, CreateExp5Grammar),
				}),
				new RecursiveFragment(GrammarType.Exp, CreateExp5Grammar, addNewNode: false)
			};
		}

		private IEnumerable<IGrammarFragment> CreateExp5Grammar()
		{
			return new List<IGrammarFragment>
			{
				new GrammarFragment(new List<IGrammarFragment>
				{
					new RecursiveFragment(GrammarType.Exp, CreateExp6Grammar),
					new RecursiveFragment(GrammarType.MultDivOp, CreateMultDivOperatorGrammar, addNewNode: false),
					new RecursiveFragment(GrammarType.Exp, CreateExp6Grammar),
				}),
				new RecursiveFragment(GrammarType.Exp, CreateExp6Grammar, addNewNode: false)
			};
		}

		private IEnumerable<IGrammarFragment> CreateExp6Grammar()
		{
			return new List<IGrammarFragment>
			{
				new GrammarFragment(new List<IGrammarFragment>
				{
					new RecursiveFragment(GrammarType.UnaryOp, CreateUnaryOperatorGrammar, addNewNode: false),
					new RecursiveFragment(GrammarType.Atom, CreateAtomGrammar),
				}),
				new RecursiveFragment(GrammarType.Atom, CreateAtomGrammar, addNewNode: false)
			};
		}

		private IEnumerable<IGrammarFragment> CreateExpSecondGrammar()
		{
			return new List<IGrammarFragment>
			{
				new GrammarFragment(new List<IGrammarFragment>
				{
					new RecursiveFragment(GrammarType.Exp, CreateOperatorsGrammar, addNewNode: false),
					new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
					new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar, addNewNode: false),
				}),
				new EmptyFragment()
			};
		}

		private IEnumerable<IGrammarFragment> CreateExpGrammar()
		{
			return new List<IGrammarFragment>
			{
				new GrammarFragment(new List<IGrammarFragment>
				{
					new RecursiveFragment(GrammarType.Exp, CreateExp1Grammar),
					new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar, addNewNode: false),
				})
			};
		}

		private IEnumerable<IGrammarFragment> CreateAtomGrammar()
		{
			return new List<IGrammarFragment>
			{
				new TokenTypeFragment(TokenType.Integer),
				new TokenTypeFragment(TokenType.String),
				new ValueFragment("true"),
				new ValueFragment("false"),
				new GrammarFragment(new List<IGrammarFragment>
				{
					new ValueFragment("("),
					new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
					new ValueFragment(")")
				}),
				new RecursiveFragment(GrammarType.FnCallExpr, CreateFnCallExprGrammar),
				new RecursiveFragment(GrammarType.SubscriptExpr, CreateSubscriptExprGrammar),
				new RecursiveFragment(GrammarType.IdUse, CreateIdGrammar)
			};
		}

		private IEnumerable<IGrammarFragment> CreateFnCallExprGrammar()
		{
			return new List<IGrammarFragment>
			{
				new GrammarFragment(new List<IGrammarFragment>
				{
					new RecursiveFragment(GrammarType.IdUse, CreateIdGrammar),
					new ValueFragment("("),
					new ValueFragment(")")
				}),
				new GrammarFragment(new List<IGrammarFragment>
				{
					new RecursiveFragment(GrammarType.IdUse, CreateIdGrammar),
					new ValueFragment("("),
					new RecursiveFragment(GrammarType.ActualList, CreateActualListGrammar),
					new ValueFragment(")")
				})
			};
		}

		private IEnumerable<IGrammarFragment> CreateFnCallStmtGrammar()
		{
			return new List<IGrammarFragment>
			{
				new GrammarFragment(new List<IGrammarFragment>
				{
					new RecursiveFragment(GrammarType.IdUse, CreateIdGrammar),
					new ValueFragment("("),
					new ValueFragment(")")
				}),
				new GrammarFragment(new List<IGrammarFragment>
				{
					new RecursiveFragment(GrammarType.IdUse, CreateIdGrammar),
					new ValueFragment("("),
					new RecursiveFragment(GrammarType.ActualList, CreateActualListGrammar),
					new ValueFragment(")")
				})
			};
		}

		private IEnumerable<IGrammarFragment> CreateActualListGrammar()
		{
			return new List<IGrammarFragment>
			{
				new GrammarFragment(new List<IGrammarFragment>
				{
					new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
					new ValueFragment(","),
					new RecursiveFragment(GrammarType.Exp, CreateExpGrammar)
				}),
				new GrammarFragment(new List<IGrammarFragment>
				{
					new RecursiveFragment(GrammarType.Exp, CreateExpGrammar)
				})
			};
		}

		private IEnumerable<IGrammarFragment> CreateSubscriptExprGrammar()
		{
			return new List<IGrammarFragment>
			{
				new GrammarFragment(new List<IGrammarFragment>
				{
					new RecursiveFragment(GrammarType.IdUse, CreateIdGrammar),
					new ValueFragment("["),
					new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
					new ValueFragment("]")
				})
			};
		}

		private IEnumerable<IGrammarFragment> CreateIdGrammar()
		{
			return new List<IGrammarFragment>
			{
				new TokenTypeFragment(TokenType.Identifier)
			};
		}
		#endregion
	}
}

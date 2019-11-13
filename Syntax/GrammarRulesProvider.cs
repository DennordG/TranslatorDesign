using System.Collections.Generic;
using TranslatorDesign.Tokenizer;

namespace TranslatorDesign.Syntax
{
    public class GrammarRulesProvider : IGrammarRulesProvider
    {
        public IDictionary<GrammarType, IEnumerable<IGrammarFragment>> GetRules()
        {
            return new Dictionary<GrammarType, IEnumerable<IGrammarFragment>>
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

        public GrammarType GetMainGrammarType()
        {
            return GrammarType.Program;
        }


        #region Grammar creation rules
        private IEnumerable<IGrammarFragment> CreateProgramSecondGrammar()
        {
            return new List<IGrammarFragment>
            {
                new GrammarFragment(new List<IGrammarFragment>
                {
                    new RecursiveFragment(GrammarType.FnDecl, CreateFnDeclGrammar),
                    new RecursiveFragment(GrammarType.Program, CreateProgramSecondGrammar)
                }),
                new GrammarFragment(new List<IGrammarFragment>
                {
                    new RecursiveFragment(GrammarType.VarDecl, CreateVarDeclGrammar),
                    new RecursiveFragment(GrammarType.Program, CreateProgramSecondGrammar)
                }),
                new EmptyFragment()
            };
        }

        private IEnumerable<IGrammarFragment> CreateProgramGrammar()
        {
            return new List<IGrammarFragment>
            {
                new RecursiveFragment(GrammarType.Program, CreateProgramSecondGrammar)
            };
        }

        private IEnumerable<IGrammarFragment> CreateVarDeclGrammar()
        {
            return new List<IGrammarFragment>
            {
                new GrammarFragment(new List<IGrammarFragment>
                {
                    new RecursiveFragment(GrammarType.Type, CreateTypeGrammar),
                    new RecursiveFragment(GrammarType.Id, CreateIdGrammar),
                    new ValueFragment(";")
                }),
                new GrammarFragment(new List<IGrammarFragment>
                {
                    new RecursiveFragment(GrammarType.Type, CreateTypeGrammar),
                    new RecursiveFragment(GrammarType.Id, CreateIdGrammar),
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
                    new RecursiveFragment(GrammarType.Id, CreateIdGrammar),
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

        private IEnumerable<IGrammarFragment> CreateFormalsListSecondGrammar()
        {
            return new List<IGrammarFragment>
            {
                new GrammarFragment(new List<IGrammarFragment>
                {
                    new ValueFragment(","),
                    new RecursiveFragment(GrammarType.FormalDecl, CreateFormalDeclGrammar),
                    new RecursiveFragment(GrammarType.FormalsList, CreateFormalsListSecondGrammar)
                }),
                new EmptyFragment()
            };
        }

        private IEnumerable<IGrammarFragment> CreateFormalsListGrammar()
        {
            return new List<IGrammarFragment>
            {
                new GrammarFragment(new List<IGrammarFragment>
                {
                    new RecursiveFragment(GrammarType.FormalDecl, CreateFormalDeclGrammar),
                    new RecursiveFragment(GrammarType.FormalsList, CreateFormalsListSecondGrammar)
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
                    new RecursiveFragment(GrammarType.Id, CreateIdGrammar)
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

        private IEnumerable<IGrammarFragment> CreateDeclListSecondGrammar()
        {
            return new List<IGrammarFragment>
            {
                new GrammarFragment(new List<IGrammarFragment>
                {
                    new RecursiveFragment(GrammarType.VarDecl, CreateVarDeclGrammar),
                    new RecursiveFragment(GrammarType.DeclList, CreateDeclListSecondGrammar),
                }),
                new EmptyFragment()
            };
        }

        private IEnumerable<IGrammarFragment> CreateDeclListGrammar()
        {
            return new List<IGrammarFragment>
            {
                new RecursiveFragment(GrammarType.DeclList, CreateDeclListSecondGrammar)
            };
        }

        private IEnumerable<IGrammarFragment> CreateStmtListSecondGrammar()
        {
            return new List<IGrammarFragment>
            {
                new GrammarFragment(new List<IGrammarFragment>
                {
                    new RecursiveFragment(GrammarType.Stmt, CreateStmtGrammar),
                    new RecursiveFragment(GrammarType.StmtList, CreateStmtListSecondGrammar),
                }),
                new EmptyFragment()
            };
        }

        private IEnumerable<IGrammarFragment> CreateStmtListGrammar()
        {
            return new List<IGrammarFragment>
            {
                new RecursiveFragment(GrammarType.StmtList, CreateStmtListSecondGrammar)
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
                    new RecursiveFragment(GrammarType.Id, CreateIdGrammar),
                    new ValueFragment(";")
                }),
                new GrammarFragment(new List<IGrammarFragment>
                {
                    new ValueFragment("cin"),
                    new ValueFragment(">>"),
                    new RecursiveFragment(GrammarType.Id, CreateIdGrammar),
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
                    new RecursiveFragment(GrammarType.Id, CreateIdGrammar),
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

        private IEnumerable<IGrammarFragment> CreateExpSecondGrammar()
        {
            return new List<IGrammarFragment>
            {
                new GrammarFragment(new List<IGrammarFragment>
                {
                    new ValueFragment("+"),
                    new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
                    new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar),
                }),
                new GrammarFragment(new List<IGrammarFragment>
                {
                    new ValueFragment("-"),
                    new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
                    new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar),
                }),
                new GrammarFragment(new List<IGrammarFragment>
                {
                    new ValueFragment("*"),
                    new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
                    new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar),
                }),
                new GrammarFragment(new List<IGrammarFragment>
                {
                    new ValueFragment("/"),
                    new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
                    new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar),
                }),
                new GrammarFragment(new List<IGrammarFragment>
                {
                    new ValueFragment("&&"),
                    new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
                    new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar),
                }),
                new GrammarFragment(new List<IGrammarFragment>
                {
                    new ValueFragment("||"),
                    new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
                    new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar),
                }),
                new GrammarFragment(new List<IGrammarFragment>
                {
                    new ValueFragment("=="),
                    new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
                    new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar),
                }),
                new GrammarFragment(new List<IGrammarFragment>
                {
                    new ValueFragment("!="),
                    new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
                    new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar),
                }),
                new GrammarFragment(new List<IGrammarFragment>
                {
                    new ValueFragment("<"),
                    new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
                    new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar),
                }),
                new GrammarFragment(new List<IGrammarFragment>
                {
                    new ValueFragment(">"),
                    new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
                    new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar),
                }),
                new GrammarFragment(new List<IGrammarFragment>
                {
                    new ValueFragment("<="),
                    new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
                    new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar),
                }),
                new GrammarFragment(new List<IGrammarFragment>
                {
                    new ValueFragment(">="),
                    new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
                    new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar),
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
                    new ValueFragment("!"),
                    new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
                    new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar)
                }),
                new GrammarFragment(new List<IGrammarFragment>
                {
                    new ValueFragment("-"),
                    new RecursiveFragment(GrammarType.Atom, CreateAtomGrammar),
                    new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar)
                }),
                new GrammarFragment(new List<IGrammarFragment>
                {
                    new RecursiveFragment(GrammarType.Atom, CreateAtomGrammar),
                    new RecursiveFragment(GrammarType.Exp, CreateExpSecondGrammar)
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
                new RecursiveFragment(GrammarType.Id, CreateIdGrammar)
            };
        }

        private IEnumerable<IGrammarFragment> CreateFnCallExprGrammar()
        {
            return new List<IGrammarFragment>
            {
                new GrammarFragment(new List<IGrammarFragment>
                {
                    new RecursiveFragment(GrammarType.Id, CreateIdGrammar),
                    new ValueFragment("("),
                    new ValueFragment(")")
                }),
                new GrammarFragment(new List<IGrammarFragment>
                {
                    new RecursiveFragment(GrammarType.Id, CreateIdGrammar),
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
                    new RecursiveFragment(GrammarType.Id, CreateIdGrammar),
                    new ValueFragment("("),
                    new ValueFragment(")")
                }),
                new GrammarFragment(new List<IGrammarFragment>
                {
                    new RecursiveFragment(GrammarType.Id, CreateIdGrammar),
                    new ValueFragment("("),
                    new RecursiveFragment(GrammarType.ActualList, CreateActualListGrammar),
                    new ValueFragment(")")
                })
            };
        }

        private IEnumerable<IGrammarFragment> CreateActualListSecondGrammar()
        {
            return new List<IGrammarFragment>
            {
                new GrammarFragment(new List<IGrammarFragment>
                {
                    new ValueFragment(","),
                    new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
                    new RecursiveFragment(GrammarType.ActualList, CreateActualListSecondGrammar)
                }),
                new EmptyFragment()
            };
        }

        private IEnumerable<IGrammarFragment> CreateActualListGrammar()
        {
            return new List<IGrammarFragment>
            {
                new GrammarFragment(new List<IGrammarFragment>
                {
                    new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
                    new RecursiveFragment(GrammarType.ActualList, CreateActualListSecondGrammar)
                })
            };
        }

        private IEnumerable<IGrammarFragment> CreateSubscriptExprGrammar()
        {
            return new List<IGrammarFragment>
            {
                new GrammarFragment(new List<IGrammarFragment>
                {
                    new RecursiveFragment(GrammarType.Id, CreateIdGrammar),
                    new ValueFragment("["),
                    new RecursiveFragment(GrammarType.Exp, CreateExpGrammar),
                    new ValueFragment("[")
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

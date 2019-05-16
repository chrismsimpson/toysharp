using System;
using System.Collections.Generic;
using System.Linq;
using Toy.AST;

namespace Toy {

    public class Parser {

        public Lexer Lexer { get; private set; }

        public Parser(Lexer lexer) {

            this.Lexer = lexer;
        }

        /// Public

        public Module ParseModule() {

            this.Lexer.GetNextToken(); // prime the lexer

            // Parse functions one at a time and accumulate in this vector.

            var functions = new List<Function>();

            var f = this.ParseDefinition();

            while (f != null) {

                functions.Add(f);

                if (this.Lexer.CurrentToken == Token.EndOfFile) {

                    break;
                }

                f = this.ParseDefinition();
            }

            if (this.Lexer.CurrentToken != Token.EndOfFile) {

                throw new Exception("At end of module");
            }

            return new Module(functions);
        }

        /// Private

        private int getTokenPrecedence() {

            if (!this.Lexer.CurrentToken.IsAscii()) {

                return -1;
            }

            switch (this.Lexer.CurrentToken) {

            case Token.Minus:
                return 20;

            case Token.Plus:
                return 20;

            case Token.Asterisk: // Mulitply
                return 40;
            
            default:
                return -1;
            }
        }

        private ReturnExpression ParseReturnExpression() {

            var location = Lexer.LastLocation;

            this.Lexer.Consume(Token.Return);

            Node expression = null;

            if (this.Lexer.CurrentToken != Token.Semicolon) {

                expression = this.ParseExpression();

                if (expression == null) {

                    return null;
                }
            }

            return new ReturnExpression(location, expression);
        }

        private Number ParseNumber() {

            var location = this.Lexer.LastLocation;
            
            var result = new Number(location, this.Lexer.Value);

            this.Lexer.Consume(Token.Number);
            
            return result;
        }

        private Node ParseTensorLiteral() {

            return null;
        }

        private Node ParseParenthesis() {

            this.Lexer.GetNextToken(); // eat (.

            var e = this.ParseExpression();

            if (e == null) {

                return null;
            }
            
            if (this.Lexer.CurrentToken != Token.ParenthesClose) {

                throw new Exception("to close expression with parentheses");
            }
            
            this.Lexer.Consume(Token.ParenthesClose);
            
            return e;
        }

        private Node ParseIdentifier() {

            var name = this.Lexer.Identifier;

            var location = this.Lexer.LastLocation;

            this.Lexer.GetNextToken(); // eat identifier.

            if (this.Lexer.CurrentToken !=  Token.ParenthesOpen) { // Simple variable ref.
            
                return new Variable(location, name);
            }

            // This is a function call.

            this.Lexer.Consume(Token.ParenthesOpen);

            var arguments = new List<Node>();

            if (this.Lexer.CurrentToken != Token.ParenthesClose) {

                while (true) {

                    var arg = this.ParseExpression();

                    if (arg != null) {

                        arguments.Add(arg);
                    }
                    else {

                        return null;
                    }
                    
                    if (this.Lexer.CurrentToken == Token.ParenthesClose) {

                        break;
                    }

                    if (this.Lexer.CurrentToken != Token.Comma) {

                        throw new Exception(", or ) in argument list");
                    }

                    this.Lexer.GetNextToken();
                }
            }
            
            this.Lexer.Consume(Token.ParenthesClose);
            
            // It can be a builtin call to print

            if (name == "print") {

                if (arguments.Count() != 1) {

                    throw new Exception("<single arg> as argument to print()");
                }

                return new PrintExpression(location, arguments.First());
            }

            // Call to a user-defined function

            return new CallExpression(location, name, arguments);
        }

        private Node ParsePrimary() {

            switch (this.Lexer.CurrentToken) {
            
            case Token.Identifier:
                return this.ParseIdentifier();
            
            case Token.Number:
                return this.ParseNumber();
            
            case Token.ParenthesOpen:
                return this.ParseParenthesis();
            
            case Token.SquareBracketOpen:
                return this.ParseTensorLiteral();
            
            case Token.Semicolon:
                return null;
            
            case Token.BracketClose:
                return null;

            default:
                throw new Exception("unknown token '" + this.Lexer.CurrentToken + "' when expecting an expression\n");
            }
        }

        private Node ParseBinOpRHS(int exprPrecedence, Node lhs) {

            while (true) {

                var tokenPredence = this.getTokenPrecedence();

                // If this is a binop that binds at least as tightly as the current binop,
                // consume it, otherwise we are done.

                if (tokenPredence < exprPrecedence) {

                    return lhs;
                }

                // Okay, we know this is a binop.

                var binOp = this.Lexer.CurrentToken;
                this.Lexer.Consume(binOp);
                var location = this.Lexer.LastLocation;

                // Parse the primary expression after the binary operator.
                
                var rhs = this.ParsePrimary();

                if (rhs == null) {

                    throw new Exception("expression to complete binary operator");
                }

                // If BinOp binds less tightly with RHS than the operator after RHS, let
                // the pending operator take RHS as its LHS.

                var nextPrec = this.getTokenPrecedence();

                if (tokenPredence < nextPrec) {

                    rhs = this.ParseBinOpRHS(tokenPredence + 1, rhs);

                    if (rhs == null) {

                        return null;
                    }
                }

                lhs = new BinaryExpression(location, (char) binOp, lhs, rhs);
            }
        }

        private Node ParseExpression() {

            var lhs = this.ParsePrimary();

            if (lhs == null) {

                return null;
            }

            return this.ParseBinOpRHS(0, lhs);
        }

        private VarType? ParseType() {

            if (this.Lexer.CurrentToken != Token.LessThan) {

                throw new Exception("< to begin type");
            }

            this.Lexer.GetNextToken(); // eat <

            var type = new VarType { Shape = new List<double>() };

            while (this.Lexer.CurrentToken == Token.Number) {
                
                type.Shape.Add(this.Lexer.Value);

                this.Lexer.GetNextToken();
            
                if (this.Lexer.CurrentToken == Token.Comma) {

                    this.Lexer.GetNextToken();
                }
            }
            
            if (this.Lexer.CurrentToken != Token.GreaterThan) {

                throw new Exception("> to end type");
            }

            this.Lexer.GetNextToken(); // eat >

            return type;
        }

        private Node ParseDeclaration() {

            if (this.Lexer.CurrentToken != Token.Var) {

                throw new Exception("var to begin declaration");
            }

            var location = this.Lexer.LastLocation;

            this.Lexer.GetNextToken(); // eat var

            if (this.Lexer.CurrentToken != Token.Identifier) {

                throw new Exception("identified after 'var' declaration");
            }

            var id = this.Lexer.Identifier;

            this.Lexer.GetNextToken(); // eat id

            VarType? type = null; // Type is optional, it can be inferred

            if (this.Lexer.CurrentToken == Token.LessThan) {

                type = this.ParseType();

                if (type == null) {

                    return null;
                }
            }

            if (type == null) {

                type = new VarType { Shape = new List<double>() };
            }

            this.Lexer.Consume(Token.Equals);
            
            var e = this.ParseExpression();

            return new VariableDeclaration(location, id, type.Value, e);
        }

        private IEnumerable<Node> ParseBlock() {

            if (this.Lexer.CurrentToken != Token.BracketOpen) {

                throw new Exception("{ to begin block");
            }
            
            this.Lexer.Consume(Token.BracketOpen);
    
            var expressions = new List<Node>();

            // Ignore empty expressions: swallow sequences of semicolons.

            while (this.Lexer.CurrentToken == Token.Semicolon) {

                this.Lexer.Consume(Token.Semicolon);
            }

            while (this.Lexer.CurrentToken != Token.BracketClose && this.Lexer.CurrentToken != Token.EndOfFile) {

                if (this.Lexer.CurrentToken == Token.Var) {

                    var variableDeclaration = this.ParseDeclaration();

                    if (variableDeclaration == null) {

                        return null;
                    }

                    expressions.Add(variableDeclaration);
                } 
                else if (this.Lexer.CurrentToken == Token.Return) {

                    var returnExpression = this.ParseReturnExpression();

                    if (returnExpression == null) {

                        return null;
                    }
                    
                    expressions.Add(returnExpression);
                }
                else {
                    
                    var expression = this.ParseExpression();

                    if (expression == null) {
                    
                        return null;
                    }
                    
                    expressions.Add(expression);
                }

                // Ensure that elements are separated by a semicolon.

                if (this.Lexer.CurrentToken != Token.BracketClose) {

                    throw new Exception("; after expression");
                }

                // Ignore empty expressions: swallow sequences of semicolons.

                while (this.Lexer.CurrentToken == Token.Semicolon) {

                    this.Lexer.Consume(Token.Semicolon);
                }
            }

            if (this.Lexer.CurrentToken != Token.BracketClose) {

                throw new Exception("} to close block");
            }

            this.Lexer.Consume(Token.BracketClose);

            return expressions;
        }

        private Prototype ParsePrototype() {

            var location = this.Lexer.LastLocation;

            this.Lexer.Consume(Token.Def);

            if (this.Lexer.CurrentToken != Token.Identifier) {

                throw new Exception("function name in prototype");
            }

            var functionName = this.Lexer.Identifier;
            
            this.Lexer.Consume(Token.Identifier);

            if (this.Lexer.CurrentToken != Token.ParenthesOpen) {

                throw new Exception("( in prototype");
            }

            this.Lexer.Consume(Token.ParenthesOpen);
            
            var arguments = new List<Variable>();

            if (this.Lexer.CurrentToken != Token.ParenthesOpen) {

                do {

                    var name = this.Lexer.Identifier;

                    var l2 = this.Lexer.LastLocation;
                    
                    this.Lexer.Consume(Token.Identifier);

                    var d = new Variable(l2, name);

                    arguments.Add(d);
                     
                    if (this.Lexer.CurrentToken != Token.Comma) {

                        break;
                    }
                    
                    this.Lexer.Consume(Token.Comma);
                    
                    if (this.Lexer.CurrentToken != Token.Identifier) {

                        throw new Exception("identifier, after ',' in function parameter list");
                    }
                }
                while (true);
            }

            if (this.Lexer.CurrentToken != Token.ParenthesOpen) {

                throw new Exception("} to end function prototype");
            }

            // success.

            this.Lexer.Consume(Token.ParenthesClose);

            return new Prototype(location, functionName, arguments);
        }

        private Function ParseDefinition() {

            var prototype = this.ParsePrototype();
            
            if (prototype == null) {
                
                return null;
            }
      
            var block = this.ParseBlock();

            if (block == null) {

                return null;
            }

            return new Function(prototype, block);
        }
    }
}
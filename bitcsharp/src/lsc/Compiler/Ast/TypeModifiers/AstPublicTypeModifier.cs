﻿using System;
using System.Text;
using LLVMSharp.Compiler.CocoR;

namespace LLVMSharp.Compiler.Ast
{
    public class AstPublicTypeModifier : AstTypeModifier
    {
        public AstPublicTypeModifier(
            string path, int lineNumber, int columnNumber)
            : base(path, lineNumber, columnNumber) { }

        public AstPublicTypeModifier(IParser parser) : base(parser) { }
        public AstPublicTypeModifier(IParser parser, bool useLookAhead) : base(parser, useLookAhead) { }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("--AstPublicTypeModifier--{0}{0}");

            sb.Append("Src: {1}{0}");
            sb.Append("Ln: {2}{0}");
            sb.Append("Col: {3}{0}{0}");

            return string.Format(sb.ToString(), Environment.NewLine, base.Path, base.LineNumber, base.ColumnNumber);
        }
    }
}
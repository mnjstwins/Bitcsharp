﻿using System;
using System.Text;
using LLVMSharp.Compiler.CocoR;

namespace LLVMSharp.Compiler.Ast
{
    public class AstPrivateTypeModifier : AstTypeModifier
    {
        public AstPrivateTypeModifier(
            string path, int lineNumber, int columnNumber)
            : base(path, lineNumber, columnNumber) { }

        public AstPrivateTypeModifier(IParser parser) : base(parser) { }
        public AstPrivateTypeModifier(IParser parser, bool useLookAhead) : base(parser, useLookAhead) { }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("--AstPrivateTypeModifier--{0}{0}");

            sb.Append("Src: {1}{0}");
            sb.Append("Ln: {2}{0}");
            sb.Append("Col: {3}{0}{0}");

            return string.Format(sb.ToString(), Environment.NewLine, base.Path, base.LineNumber, base.ColumnNumber);
        }
    }
}

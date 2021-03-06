﻿using LLVMSharp.Compiler.Ast;
using LLVMSharp.Compiler.CodeGenerators.LLVM;

namespace LLVMSharp.Compiler.CodeGenerators
{
    public partial class LLVMSharpCodeGenerator
    {
        public override void EmitCode(AstPreIncrement astPreIncrement)
        {            
            AstVariableReference varRef = (AstVariableReference)astPreIncrement.AstExpression;
            astPreIncrement.AstExpression.EmitCode(this);
            int loadedTempCount = TempCount++;
            switch (astPreIncrement.AssociatedType)
            {
                case "System.Int32":
                    GeneratePreIncrementInt32(varRef, loadedTempCount);
                    break;
                case "System.Single":
                    GeneratePreIncrementSingle(varRef, loadedTempCount);
                    break;
            }           
            
        }

        private void GeneratePreIncrementSingle(AstVariableReference varRef, int loadedTempCount)
        {
           if (varRef.MemberRefCollection.Count == 0)
            {
                Alloca allocaTemp = new Alloca(LLVMModule)
                {
                    Type = "float",
                    Result = "%" + TempCount
                };
                WriteLine(2, allocaTemp.EmitCode());
                Store storeTemp = new Store(LLVMModule)
                {
                    Type = "float",
                    Value = "%" + loadedTempCount,
                    Pointer = allocaTemp.Result
                };
                WriteLine(2, storeTemp.EmitCode());
                ++TempCount;
              
                Sub sub = new Sub(LLVMModule)
                {
                    Result = "%" + (TempCount),
                    Type = "float",
                    Operand1 = "%" + loadedTempCount,
                    Operand2 = "1.0"
                };
                WriteLine(2, sub.EmitCode());
                ++TempCount;

                Store s = new Store(LLVMModule)
                {
                    Type = "float",
                    Value = sub.Result
                };
                
                s.Pointer = "%l_" + varRef.VariableName;
                WriteLine(2, s.EmitCode());              

                Load l = new Load(LLVMModule)
                {
                    Result = "%" + TempCount,
                    Type = "float",
                    Pointer =  s.Pointer
                };

                WriteLine(2, l.EmitCode());
              
            }
            else // varRef.MemberRefCollection.Count > 0
            {
                Alloca allocaTemp = new Alloca(LLVMModule)
                {
                    Type = "float",
                    Result = "%" + TempCount
                };
                WriteLine(2, allocaTemp.EmitCode());
                WriteCommentLine(loadedTempCount);

                Alloca a = new Alloca(LLVMModule)
                {
                    Type = "float*",
                    Result = "%" + (TempCount + 1)
                };
                WriteLine(2, a.EmitCode());
                ++TempCount;

                Store s = new Store(LLVMModule)
                {
                    Value = "%" + (loadedTempCount - 1),
                    Pointer = "%" + TempCount,
                    Type = "float*"
                };
                WriteLine(2, s.EmitCode());
                ++TempCount;

                Load loadPtr = new Load(LLVMModule)
                {
                    Result = "%" + TempCount,
                    Type = "float*",
                    Pointer = a.Result
                };
                WriteLine(2, loadPtr.EmitCode());

                Store storeTemp = new Store(LLVMModule)
                {
                    Type = "float",
                    Value = "%" + loadedTempCount,
                    Pointer = allocaTemp.Result
                };
                WriteLine(2, storeTemp.EmitCode());

                Add add = new Add(LLVMModule)
                {
                    Result = "%" + (TempCount + 1),
                    Type = "float",
                    Operand1 = "%" + loadedTempCount,
                    Operand2 = "1.0"
                };
                WriteLine(2, add.EmitCode());
                ++TempCount;

                Store storeActual = new Store(LLVMModule)
                {
                    Value = add.Result,
                    Pointer = loadPtr.Result,
                    Type = "float"
                };
                WriteLine(2, storeActual.EmitCode());
                ++TempCount;

                Load l = new Load(LLVMModule)
                {
                    Result = "%" + TempCount,
                    Type = "float",
                    Pointer = storeActual.Pointer
                };
                WriteLine(2, l.EmitCode());
            }            
        }

        private void GeneratePreIncrementInt32(AstVariableReference varRef, int loadedTempCount)
        {
            if (varRef.MemberRefCollection.Count == 0)
            {
                Alloca allocaTemp = new Alloca(LLVMModule)
                {
                    Type = "i32",
                    Result = "%" + TempCount
                };
                WriteLine(2, allocaTemp.EmitCode());
                Store storeTemp = new Store(LLVMModule)
                {
                    Type = "i32",
                    Value = "%" + loadedTempCount,
                    Pointer = allocaTemp.Result
                };
                WriteLine(2, storeTemp.EmitCode());
                ++TempCount;

                Add add = new Add(LLVMModule)
                {
                    Result = "%" + (TempCount),
                    Type = "i32",
                    Operand1 = "%" + loadedTempCount,
                    Operand2 = "1"
                };
                WriteLine(2, add.EmitCode());
                ++TempCount;

                Store s = new Store(LLVMModule)
                {
                    Type = "i32",
                    Value = add.Result
                };

                s.Pointer = "%l_" + varRef.VariableName;
                WriteLine(2, s.EmitCode());

                Load l = new Load(LLVMModule)
                {
                    Result = "%" + TempCount,
                    Type = "i32",
                    Pointer = s.Pointer
                };

                WriteLine(2, l.EmitCode());

            }
            else // varRef.MemberRefCollection.Count > 0
            {
                Alloca allocaTemp = new Alloca(LLVMModule)
                {
                    Type = "i32",
                    Result = "%" + TempCount
                };
                WriteLine(2, allocaTemp.EmitCode());
                WriteCommentLine(loadedTempCount);

                Alloca a = new Alloca(LLVMModule)
                {
                    Type = "i32*",
                    Result = "%" + (TempCount + 1)
                };
                WriteLine(2, a.EmitCode());
                ++TempCount;

                Store s = new Store(LLVMModule)
                {
                    Value = "%" + (loadedTempCount - 1),
                    Pointer = "%" + TempCount,
                    Type = "i32*"
                };
                WriteLine(2, s.EmitCode());
                ++TempCount;

                Load loadPtr = new Load(LLVMModule)
                {
                    Result = "%" + TempCount,
                    Type = "i32*",
                    Pointer = a.Result
                };
                WriteLine(2, loadPtr.EmitCode());

                Store storeTemp = new Store(LLVMModule)
                {
                    Type = "i32",
                    Value = "%" + loadedTempCount,
                    Pointer = allocaTemp.Result
                };
                WriteLine(2, storeTemp.EmitCode());

                Add add = new Add(LLVMModule)
                {
                    Result = "%" + (TempCount + 1),
                    Type = "i32",
                    Operand1 = "%" + loadedTempCount,
                    Operand2 = "1"
                };
                WriteLine(2, add.EmitCode());
                ++TempCount;

                Store storeActual = new Store(LLVMModule)
                {
                    Value = add.Result,
                    Pointer = loadPtr.Result,
                    Type = "i32"
                };
                WriteLine(2, storeActual.EmitCode());
                ++TempCount;

                Load l = new Load(LLVMModule)
                {
                    Result = "%" + TempCount,
                    Type = "i32",
                    Pointer = storeActual.Pointer
                };
                WriteLine(2, l.EmitCode());
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjWast
{
    internal class ObjWastFuncContext : ObjWastTranspilerContext, IObjWastCallable
    {
        private bool parseBody;
        private string identifier;
        private List<ObjWastVariable> variables;
        private ObjWastCallableBodyContext body;

        public ObjWastModuleContext Module { get; private set; }
        public OwType ResultType { get; set; }

        public ObjWastFuncContext(ObjWastModuleContext module)
        {
            Module = module;

            variables = new List<ObjWastVariable>();
            body = new ObjWastCallableBodyContext(this);
        }

        public void AddVariable(ObjWastVariable variable)
        {
            variables.Add(variable);
        }

        internal override ObjWastTranspilerContext GetContext(string statement, ObjWastTranspiler transpiler)
        {
            if (!parseBody)
            {
                if (statement == "param")
                    return new ObjWastParamContext(this);
                else if (statement == "result")
                    return new ObjWastResultContext(this);
                else if (statement == "local")
                    return new ObjWastLocalContext(this);
                else
                    parseBody = true;
            }

            return body.GetContext(statement, transpiler);
        }

        internal override void Parse(ObjWastTranspiler transpiler)
        {
            // ex. (func $test (param i32) (result i32) (local i32) ... )

            if (identifier == null && !parseBody)
            {
                identifier = transpiler.ParseIdentifier();
                return;
            }

            parseBody = true;
            body.Parse(transpiler);
        }

        internal override string ToWast()
        {
            StringBuilder output = new StringBuilder();

            output.AppendLine("(func");
            bool param = true;

            output.Append("    ");

            foreach(var variable in variables)
            {
                if (variable is ObjWastLocal && param)
                {
                    param = false;

                    if (ResultType != null)
                        output.AppendLine($"(result {ResultType.Name})");
                }

                if (param && !(variable is ObjWastParam))
                    throw new Exception();

                if (!param && !(variable is ObjWastLocal))
                    throw new Exception();

                output.AppendLine(variable.ToWast());
            }

            output.Append(body.ToWast());
            output.AppendLine(")");

            return output.ToString();
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;

namespace ObjWast
{
    internal class ObjWastMethodContext : ObjWastTranspilerContext, IObjWastCallable
    {
        private ObjWastStructContext @struct;
        private bool parseBody;
        private List<ObjWastVariable> variables;

        private string identifier;
        private OwMemberAccess access;

        public ObjWastModuleContext Module { get { return @struct.Module; } }
        public OwType ResultType { get; set; }

        public ObjWastMethodContext(ObjWastStructContext @struct)
        {
            this.@struct = @struct;
        }

        public void AddVariable(ObjWastVariable variable)
        {

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



            return null;
        }

        internal override void Parse(ObjWastTranspiler transpiler)
        {
            // ex. (func private $test (param i32) (result i32) (local i32) ... )

            if (identifier == null && !parseBody)
            {
                string name = transpiler.ParseName();
                access = OwMemberAccess.Private;

                if (!string.IsNullOrWhiteSpace(name))
                {
                    var tempAccess = transpiler.ParseAccess(name);

                    if (tempAccess == null)
                        throw new Exception();

                    access = tempAccess.Value;
                }

                identifier = transpiler.ParseIdentifier();
                return;
            }

            parseBody = true;
        }

        internal override string ToWast()
        {
            return null;
        }
    }
}
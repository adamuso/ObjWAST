using System;
using System.IO;

namespace ObjWast
{
    internal class ObjWastFieldContext : ObjWastTranspilerContext
    {
        private ObjWastStructContext @struct;

        public ObjWastFieldContext(ObjWastStructContext @struct)
        {
            this.@struct = @struct;
        }

        internal override ObjWastTranspilerContext GetContext(string statement, ObjWastTranspiler transpiler)
        {
            return null;
        }

        internal override void Parse(ObjWastTranspiler transpiler)
        {
            string identifier = null;
            OwMemberAccess access = OwMemberAccess.Private;
            OwType type;

            string name = transpiler.ParseName();

            if (string.IsNullOrWhiteSpace(name))
            {
                // ex. (field $test i32)

                identifier = transpiler.ParseIdentifier();

                if (identifier == null)
                    throw new Exception();

                type = transpiler.ParseType(@struct.Module);
            }
            else
            {
                // ex. (field i32)
                // ex. (field private i32)
                // ex. (field private $test i32)

                OwMemberAccess? tempAccess = transpiler.ParseAccess(name);

                if (tempAccess == null)
                {
                    // ex. (field i32)
                    type = transpiler.ParseType(@struct.Module, name);
                }
                else
                {
                    // ex. (field private i32)
                    // ex. (field private $test i32)

                    access = tempAccess.Value;
                    identifier = transpiler.ParseIdentifier();
                    type = transpiler.ParseType(@struct.Module);
                }
            }

            @struct.DefineField(access, identifier, type);
        }

        internal override string ToWast()
        {
            return null;
        }
    }
}
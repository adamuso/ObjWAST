using System;
using System.IO;
using System.Text;

namespace ObjWast
{
    internal class ObjWastStructContext : ObjWastTranspilerContext
    {
        private OwType type;

        public ObjWastModuleContext Module { get; private set; }

        public ObjWastStructContext(ObjWastModuleContext module)
        {
            Module = module;
        }

        internal override ObjWastTranspilerContext GetContext(string statement, ObjWastTranspiler transpiler)
        {
            if (statement == "field")
                return new ObjWastFieldContext(this);
            else if (statement == "func")
                return new ObjWastMethodContext(this);

            return null;
        }

        internal override void Parse(ObjWastTranspiler transpiler)
        {
            string name = transpiler.ParseName();

            type = Module.DefineType(name);
        }

        internal void DefineField(OwMemberAccess access, string identifier, OwType fieldType)
        {
            type.AddField(access, identifier, fieldType);
        }

        internal void DefineMethod(OwMemberAccess access, string identifier, OwType resultType, OwParameterInfo[] parameters)
        {
            type.AddMethod(access, identifier, resultType, parameters);
        }

        internal override string ToWast()
        {
            StringBuilder builder = new StringBuilder();

            foreach(var child in Children)
            {
                var wast = child.ToWast();

                if(wast != null)
                    builder.AppendLine(wast);
            }

            return builder.ToString();
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ObjWast
{
    internal class ObjWastModuleContext : ObjWastTranspilerContext
    {
        private readonly List<OwType> definedTypes;

        public ObjWastModuleContext()
        {
            definedTypes = new List<OwType>()
            {
                new OwType("i32") { IsPrimitive = true },
                new OwType("i64") { IsPrimitive = true },
                new OwType("f32") { IsPrimitive = true },
                new OwType("f64") { IsPrimitive = true }
            };
        }

        internal override ObjWastTranspilerContext GetContext(string statement, ObjWastTranspiler transpiler)
        {
            if (statement == "struct")
                return Create(() => new ObjWastStructContext(this));
            else if (statement == "func")
                return Create(() => new ObjWastFuncContext(this));

            return null;
        }

        internal override void Parse(ObjWastTranspiler transpiler)
        {

        }

        internal override string ToWast()
        {
            return $"(module" +
                $"{Environment.NewLine}" +
                $"{string.Join(Environment.NewLine + "    ", Children.Select(child => child.ToWast()))}" +
                $")";
        }

        internal OwType DefineType(string name)
        {
            var type = new OwType(name);
            definedTypes.Add(type);
            return type;
        }

        internal OwType ResolveType(string name)
        {
            return definedTypes.Find(type => type.Name == name);
        }
    }
}
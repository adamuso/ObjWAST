using System;
using System.Collections.Generic;
using System.IO;

namespace ObjWast
{
    internal abstract class ObjWastTranspilerContext
    {
        private List<ObjWastTranspilerContext> children;

        protected IReadOnlyList<ObjWastTranspilerContext> Children => children;

        public ObjWastTranspilerContext()
        {
            children = new List<ObjWastTranspilerContext>();
        }

        internal abstract ObjWastTranspilerContext GetContext(string statement, ObjWastTranspiler transpiler);

        internal abstract void Parse(ObjWastTranspiler transpiler);

        internal abstract string ToWast();

        protected T Create<T>(Func<T> creator) where T : ObjWastTranspilerContext
        {
            var context = creator();
            children.Add(context);
            return context;
        }
    }
}
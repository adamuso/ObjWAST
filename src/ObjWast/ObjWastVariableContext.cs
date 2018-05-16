namespace ObjWast
{
    internal abstract class ObjWastVariableContext : ObjWastTranspilerContext
    {
        public IObjWastCallable Callable { get; }

        protected ObjWastVariableContext(IObjWastCallable callable)
        {
            Callable = callable;
        }

        internal override void Parse(ObjWastTranspiler transpiler)
        {
            // ex. (local $i i32)
            // ex. (local i32)
            // ex. (local i32 i64 f32)
            string identifier = transpiler.ParseIdentifier();
            OwType type = transpiler.ParseType(Callable.Module);

            if (identifier != null)
                Callable.AddVariable(CreateVariable(identifier, type));
            else
            {
                Callable.AddVariable(CreateVariable(type));

                string name = transpiler.ParseName();

                while (!string.IsNullOrWhiteSpace(name))
                {
                    type = transpiler.ParseType(Callable.Module, name);
                    Callable.AddVariable(CreateVariable(type));

                    name = transpiler.ParseName();
                }
            }
        }

        internal override string ToWast()
        {
            return null;
        }

        protected abstract ObjWastVariable CreateVariable(OwType type);
        protected abstract ObjWastVariable CreateVariable(string identifier, OwType type);
    }
}
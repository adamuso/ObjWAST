namespace ObjWast
{
    internal class ObjWastLocalContext : ObjWastVariableContext
    {
        public ObjWastLocalContext(IObjWastCallable callable)
            : base(callable)
        {

        }

        internal override ObjWastTranspilerContext GetContext(string statement, ObjWastTranspiler transpiler)
        {
            return null;
        }

        protected override ObjWastVariable CreateVariable(OwType type)
        {
            return new ObjWastLocal(type);
        }

        protected override ObjWastVariable CreateVariable(string identifier, OwType type)
        {
            return new ObjWastLocal(identifier, type);
        }
    }
}
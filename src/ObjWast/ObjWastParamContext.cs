namespace ObjWast
{
    internal class ObjWastParamContext : ObjWastVariableContext
    {
        public ObjWastParamContext(IObjWastCallable callable)
            : base(callable)
        {

        }

        internal override ObjWastTranspilerContext GetContext(string statement, ObjWastTranspiler transpiler)
        {
            return null;
        }

        protected override ObjWastVariable CreateVariable(OwType type)
        {
            return new ObjWastParam(type);
        }

        protected override ObjWastVariable CreateVariable(string identifier, OwType type)
        {
            return new ObjWastParam(identifier, type);
        }
    }
}
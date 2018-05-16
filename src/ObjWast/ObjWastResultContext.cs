namespace ObjWast
{
    internal class ObjWastResultContext : ObjWastTranspilerContext
    {
        private IObjWastCallable callable;

        public ObjWastResultContext(IObjWastCallable callable)
        {
            this.callable = callable;
        }

        internal override ObjWastTranspilerContext GetContext(string statement, ObjWastTranspiler transpiler)
        {
            return null;
        }

        internal override void Parse(ObjWastTranspiler transpiler)
        {
            var type = transpiler.ParseType(callable.Module);

            callable.ResultType = type;
        }

        internal override string ToWast()
        {
            return null;
        }
    }
}
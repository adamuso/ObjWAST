namespace ObjWast
{
    internal interface IObjWastCallable
    {
        ObjWastModuleContext Module { get; }
        OwType ResultType { get; set; }

        void AddVariable(ObjWastVariable variable);
    }
}
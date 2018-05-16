namespace ObjWast
{
    internal abstract class ObjWastVariable
    {
        public string Identifier { get; private set; }
        public OwType Type { get; private set; }

        public ObjWastVariable(OwType type)
        {
            Type = type;
        }

        public ObjWastVariable(string identifier, OwType type)
        {
            Identifier = identifier;
            Type = type;
        }

        internal abstract string ToWast();
    }
}
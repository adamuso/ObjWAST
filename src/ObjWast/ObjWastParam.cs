namespace ObjWast
{
    internal class ObjWastParam : ObjWastVariable
    {
        public ObjWastParam(OwType type) 
            : base(type)
        {
        }

        public ObjWastParam(string identifier, OwType type) 
            : base(identifier, type)
        {
        }

        internal override string ToWast()
        {
            string identifier = string.IsNullOrWhiteSpace(Identifier) ? "" : "$" + Identifier + " ";

            return $"(param {identifier}{Type.Name})";
        }
    }
}
using System.Linq;

namespace ObjWast
{
    internal class ObjWastLocal : ObjWastVariable
    {
        public ObjWastLocal(OwType type)
            : base(type)
        {

        }
        public ObjWastLocal(string identifier, OwType type) 
            : base(identifier, type)
        {
        }

        internal override string ToWast()
        {
            string identifier = string.IsNullOrWhiteSpace(Identifier) ? "" : "$" + Identifier + " ";

            if (Type.IsPrimitive)
                return $"(local {identifier}{Type.Name})";
            else
                return $"(local {FlattenType(Type)})";
        }

        private string FlattenType(OwType type)
        {
            if (type.IsPrimitive)
                return type.Name;

            return string.Join(" ", Type.GetFields().Select(field => FlattenType(field.FieldType)));
        }
    }
}
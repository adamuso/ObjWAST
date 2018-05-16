using System;

namespace ObjWast
{
    internal class ObjWastIdentifier
    {
        public string Identifier { get; }

        public ObjWastIdentifier(string identifier)
        {
            Identifier = identifier;
        }

        internal string ToWast()
        {
            return $"${Identifier}";
        }
    }
}
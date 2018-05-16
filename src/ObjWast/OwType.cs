using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjWast
{
    internal class OwType
    {
        private List<OwMemberInfo> members;

        public string Name { get; private set; }

        public bool IsPrimitive { get; set; }

        public OwType(string name)
        {
            Name = name;

            members = new List<OwMemberInfo>();
        }

        internal void AddField(OwMemberAccess access, string identifier, OwType type)
        {
            members.Add(new OwFieldInfo(this, access, identifier, type));
        }

        internal void AddMethod(OwMemberAccess access, string identifier, OwType resultType, OwParameterInfo[] parameters)
        {
            members.Add(new OwMethodInfo(this, access, identifier, resultType, parameters));
        }

        internal OwFieldInfo[] GetFields()
        {
            return members.OfType<OwFieldInfo>().ToArray();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
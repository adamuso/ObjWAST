namespace ObjWast
{
    internal class OwMemberInfo
    {
        public OwType DeclaringType { get; }
        public OwMemberAccess Access { get; }
        public string Identifier { get; }

        public OwMemberInfo(OwType declaringType, OwMemberAccess access, string identifier)
        {
            DeclaringType = declaringType;
            Access = access;
            Identifier = identifier;
        }
    }
}
namespace ObjWast
{
    internal class OwFieldInfo : OwMemberInfo
    {
        public OwType FieldType { get; }

        public OwFieldInfo(OwType declaringType, OwMemberAccess access, string identifier, OwType fieldType) 
            : base(declaringType, access, identifier)
        {
            FieldType = fieldType;
        }
    }
}
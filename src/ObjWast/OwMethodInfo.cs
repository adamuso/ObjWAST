namespace ObjWast
{
    internal class OwMethodInfo : OwMemberInfo
    {
        public OwType ResultType { get; }
        public OwParameterInfo[] Parameters { get; }

        public OwMethodInfo(OwType declaringType, OwMemberAccess access, string identifier, OwType resultType, 
            OwParameterInfo[] parameters)
            : base(declaringType, access, identifier)
        {
            ResultType = resultType;
            Parameters = parameters;
        }
    }
}
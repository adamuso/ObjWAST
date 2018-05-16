using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ObjWast
{
    internal class ObjWastTranspiler
    {
        private StreamReader reader;
        private List<OwType> definedTypes;

        public ObjWastTranspiler()
        {
            definedTypes = new List<OwType>();
        }

        #region OOP Language

        private OwType DefineType(string name)
        {
            var type = new OwType(name);
            definedTypes.Add(type);
            return type;
        }

        private OwType ResolveType(string name)
        {
            return definedTypes.FirstOrDefault(t => t.Name == name);
        }

        #endregion

        #region Basic WASM

        public void Parse(string file)
        {
            reader = new StreamReader(File.OpenRead(file));

            SkipSpaces();

            ParseModule();
        }

        private string ParseStatementStart()
        {
            if(Peek() != '(')
                throw new Exception();

            Read();
            
            string identifier = ParseName();

            return identifier;
        }

        private void ParseStatementEnd()
        {
            SkipSpaces();

            if(Peek() != ')')
                throw new Exception();

            Read();
        }

        private void ParseModule()
        {
            Stack<ObjWastTranspilerContext> contextStack = new Stack<ObjWastTranspilerContext>();
            var context = new ObjWastModuleContext();
            contextStack.Push(context);

            string module = ParseStatementStart();

            if (module != "module")
                throw new Exception();

            while (contextStack.Count > 0)
            {
                if (PeekSignificant() == '(')
                {
                    string statement = ParseStatementStart();

                    var nextContext = contextStack.Peek()?.GetContext(statement, this);

                    contextStack.Push(nextContext);
                }
                else if (PeekSignificant() == ')')
                {
                    ParseStatementEnd();
                    contextStack.Pop();
                }
                else
                {
                    if (contextStack.Peek() == null)
                        while (PeekSignificant() != ')' && Peek() != '(')
                            Read();
                    else
                        contextStack.Peek().Parse(this);
                }
            }

            string wast = context.ToWast();
        }

        #endregion

        #region Classes

        public OwMemberAccess? ParseAccess(string name = null)
        {
            name = name ?? ParseName();

            switch(name)
            {
                case "private":
                    return OwMemberAccess.Private;
                case "protected":
                    return OwMemberAccess.Protected;
                case "public":
                    return OwMemberAccess.Public;
            }

            return null;
        }

        public OwType ParseType(ObjWastModuleContext module, string name = null)
        {
            name = name ?? ParseName();

            return module.ResolveType(name);
        }

        #endregion

        #region Basic parsing

        public long ParseLong()
        {
            SkipSpaces();

            StringBuilder output = new StringBuilder();

            while (char.IsDigit(Peek()))
                output.Append(Read());

            return long.Parse(output.ToString());
        }

        public string ParseIdentifier()
        {
            SkipSpaces();

            StringBuilder output = new StringBuilder();

            if(Peek() != '$')
                return null;

            Read();

            if(char.IsLetter(Peek()) || Peek() == '_')
                while(char.IsLetterOrDigit(Peek()) || Peek() == '_')
                    output.Append(Read());

            return output.ToString();
        }

        public string ParseName()
        {
            SkipSpaces();

            StringBuilder output = new StringBuilder();

            if(char.IsLetter(Peek()) || Peek() == '_')
                while(char.IsLetterOrDigit(Peek()) || Peek() == '_')
                    output.Append(Read());
        
            return output.ToString();
        }

        private void SkipTo(char c)
        {
            while(Peek() != 'c')
                Read();
        }

        public char PeekSignificant()
        {
            SkipSpaces();
            return Peek();
        }

        private void SkipSpaces()
        {
            while(char.IsWhiteSpace(Peek()))
                Read();
        }

        private char Peek()
        {
            int c = reader.Peek();
        
            if(c >= 0)
                return (char)c;

            return '\0';
        }

        public char Read()
        {
            int c = reader.Read();

            if(c >= 0)
                return (char)c;

            return '\0';
        }

        #endregion
    }
}
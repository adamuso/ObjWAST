using System;
using System.Collections.Generic;
using System.Text;

namespace ObjWast
{
    internal class ObjWastInstructionContext : ObjWastTranspilerContext, IObjWastInstructionContainerContext
    {
        private readonly IObjWastInstructionContainerContext container;
        private readonly string instructionName;
        private List<object> arguments;
        private List<ObjWastInstruction> instructions;

        public bool HasChildren { get { return Children.Count > 0; } }

        public ObjWastInstructionContext(IObjWastInstructionContainerContext container, string instructionName)
        {
            this.container = container;
            this.instructionName = instructionName;

            arguments = new List<object>();
            instructions = new List<ObjWastInstruction>(); 
        }

        public void AddInstruction(ObjWastInstruction instruction)
        {
            instructions.Add(instruction);
        }

        internal override ObjWastTranspilerContext GetContext(string statement, ObjWastTranspiler transpiler)
        {
            string instructionName = statement;

            if (transpiler.PeekSignificant() == '.')
            {
                instructionName += transpiler.Read();
                instructionName += transpiler.ParseName();
            }

            return Create(() => new ObjWastInstructionContext(this, instructionName));
        }

        internal override void Parse(ObjWastTranspiler transpiler)
        {
            if (char.IsDigit(transpiler.PeekSignificant()))
            {
                long number = transpiler.ParseLong();
                arguments.Add(number);
            }
            else if(transpiler.PeekSignificant() == '$')
            {
                string identifier = transpiler.ParseIdentifier();
                arguments.Add(new ObjWastIdentifier(identifier));
            }
        }

        internal override string ToWast()
        {
            StringBuilder output = new StringBuilder();
            
            output.Append(instructionName);

            foreach(var argument in arguments)
            {
                output.Append(" ");

                if (argument is IConvertible convertible)
                    output.Append(convertible.ToString());
                else if (argument is ObjWastIdentifier identifier)
                    output.Append(identifier.ToWast());
                else
                    throw new Exception();
            }

            foreach(var child in Children)
            {
                output.Append("(" + child.ToWast() + ")");
            }
            
            return output.ToString();
        }
    }
}
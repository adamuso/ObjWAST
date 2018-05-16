using System.Collections.Generic;
using System.Text;

namespace ObjWast
{
    internal class ObjWastCallableBodyContext : ObjWastTranspilerContext, IObjWastInstructionContainerContext
    {
        private readonly IObjWastCallable callable;
        private readonly List<ObjWastInstruction> instructions;

        public ObjWastCallableBodyContext(IObjWastCallable callable)
        {
            this.callable = callable;

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
            string instructionName = transpiler.ParseName();

            if (transpiler.PeekSignificant() == '.')
            {
                instructionName += transpiler.Read();
                instructionName += transpiler.ParseName();
            }

            var context = Create(() => new ObjWastInstructionContext(this, instructionName));
            context.Parse(transpiler);
        }

        internal override string ToWast()
        {
            StringBuilder output = new StringBuilder();

            foreach (var child in Children)
            {
                if (child is ObjWastInstructionContext instruction)
                {
                    if (instruction.HasChildren)
                        output.Append("(");

                    if(instruction.HasChildren)
                        output.Append(instruction.ToWast());
                    else
                        output.AppendLine(instruction.ToWast());

                    if(instruction.HasChildren)
                        output.AppendLine(")");
                }
            }

            return output.ToString();
        }
    }
}
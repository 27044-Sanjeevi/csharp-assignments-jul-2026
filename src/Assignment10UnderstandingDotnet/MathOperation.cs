using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment10UnderstandingDotnet
{
    internal class MathOperation
    {
        public int FirstNumber { get; set; }

        public int SecondNumber { get; set; }

        public MathOperator Operator { get; set; }

        public int Calculate()
        {
            return this.Operator switch
            {
                MathOperator.Addition =>
                    MathUtils.Add(this.FirstNumber, this.SecondNumber),
                MathOperator.Subtraction =>
                    MathUtils.Subtract(this.FirstNumber, this.SecondNumber),
                MathOperator.Multiplication =>
                    MathUtils.Multiply(this.FirstNumber, this.SecondNumber),
                MathOperator.Division =>
                    MathUtils.Divide(this.FirstNumber, this.SecondNumber),
                _ =>
                    throw new InvalidOperationException("Invalid operator"),
            };
        }
    }
}

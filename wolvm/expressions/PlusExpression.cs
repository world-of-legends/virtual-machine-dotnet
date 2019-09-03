﻿using System;
using System.Collections.Generic;
using System.Text;

namespace wolvm
{
    public class PlusExpression : VMExpression
    {
        public Value ParseExpression(params Value[] args)
        {
            double sum = 0.0;
            foreach (Value val in args)
            {
                wolDouble vald = (wolDouble) val.type;
                sum += vald.value;
            }
            Console.WriteLine(sum);
            return new Value(VirtualMachine.wolDouble, sum.ToString());
        }
    }
}
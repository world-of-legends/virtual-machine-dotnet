﻿using System;
using System.Collections.Generic;
using System.Text;

namespace wolvm
{
    public class RunExpression : VMExpression
    {
        public Value ParseExpression(params Value[] args)
        {
            return Value.VoidValue;
        }
    }
}
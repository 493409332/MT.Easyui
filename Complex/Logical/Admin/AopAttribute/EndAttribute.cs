﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MtAop.BaseAttribute;
using MtAop.Context;

namespace Complex.Logical.Admin.AopAttribute
{
    public class EndAttribute : BaseEndAttribute
    {
        public override InvokeContext Action(InvokeContext context)
        {
          //  Console.WriteLine("log start!");
            return context;
        }
    }
}
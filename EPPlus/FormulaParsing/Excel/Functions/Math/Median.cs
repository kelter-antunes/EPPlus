﻿/* Copyright (C) 2011  Jan Källman
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.

 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
 * See the GNU Lesser General Public License for more details.
 *
 * The GNU Lesser General Public License can be viewed at http://www.opensource.org/licenses/lgpl-license.php
 * If you unfamiliar with this license or have questions about it, here is an http://www.gnu.org/licenses/gpl-faq.html
 *
 * All code and executables are provided "as is" with no warranty either express or implied. 
 * The author accepts no liability for any damage or loss of business that this product may cause.
 *
 * Code change notes:
 * 
 * Author							Change						Date
 *******************************************************************************
 * Mats Alm   		                Added		                2015-01-10
 *******************************************************************************/
using OfficeOpenXml.FormulaParsing.ExpressionGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfficeOpenXml.FormulaParsing.Excel.Functions.Math
{
    public class Median : ExcelFunction
    {
        public override CompileResult Execute(IEnumerable<FunctionArgument> arguments, ParsingContext context)
        {
            var nums = ArgsToDoubleEnumerable(arguments, context);
            var arr = nums.ToArray();
            Array.Sort(arr);
            ThrowExcelErrorValueExceptionIf(() => arr.Length == 0, eErrorType.Num);
            double result;
            if (arr.Length % 2 == 1)
            {
                result = arr[arr.Length / 2];
            }
            else
            {
                var startIndex = arr.Length / 2 - 1;
                result = (arr[startIndex] + arr[startIndex + 1]) / 2d;
            }
            return CreateResult(result, DataType.Decimal);
        }
    }
}

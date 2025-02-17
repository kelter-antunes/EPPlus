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
 * Mats Alm   		                Added		                2013-12-03
 *******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using OfficeOpenXml.FormulaParsing.Utilities;
using OfficeOpenXml.FormulaParsing.Exceptions;
using util = OfficeOpenXml.Utils;

namespace OfficeOpenXml.FormulaParsing.Excel.Functions
{
    public class DoubleArgumentParser : ArgumentParser
    {
        public override object Parse(object obj)
        {
            Require.That(obj).Named("argument").IsNotNull();
            if (obj is ExcelDataProvider.IRangeInfo)
            {
                var r = ((ExcelDataProvider.IRangeInfo)obj).FirstOrDefault();
                return r == null ? 0 : r.ValueDouble;
            }
            if (obj is double) return obj;
            if (obj.IsNumeric()) return util.ConvertUtil.GetValueDouble(obj);
            var str = obj != null ? obj.ToString() : string.Empty;
            try
            {
                double d;
                if (double.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out d))
                    return d;

                return System.DateTime.Parse(str, CultureInfo.CurrentCulture, DateTimeStyles.None).ToOADate();
            }
            catch// (Exception e)
            {
                throw new ExcelErrorValueException(ExcelErrorValue.Create(eErrorType.Value));
            }
        }
    }
}

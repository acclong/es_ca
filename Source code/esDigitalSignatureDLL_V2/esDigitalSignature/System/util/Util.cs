using System.Globalization;
/*
 * $Id: Util.cs 748 2014-05-19 09:15:01Z asubach $
 * 
 *
 * This file is part of the iText project.
 * Copyright (c) 1998-2014 iText Group NV
 * Authors: Bruno Lowagie, Paulo Soares, et al.
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License version 3
 * as published by the Free Software Foundation with the addition of the
 * following permission added to Section 15 as permitted in Section 7(a):
 * FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
 * ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
 * OF THIRD PARTY RIGHTS
 *
 * This program is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
 * or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU Affero General Public License for more details.
 * You should have received a copy of the GNU Affero General Public License
 * along with this program; if not, see http://www.gnu.org/licenses or write to
 * the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
 * Boston, MA, 02110-1301 USA, or download the license from the following URL:
 * http://itextpdf.com/terms-of-use/
 *
 * The interactive user interfaces in modified source and object code versions
 * of this program must display Appropriate Legal Notices, as required under
 * Section 5 of the GNU Affero General Public License.
 *
 * In accordance with Section 7(b) of the GNU Affero General Public License,
 * you must retain the producer line in every PDF that is created or manipulated
 * using iText.
 *
 * You can be released from the requirements of the license by purchasing
 * a commercial license. Buying such a license is mandatory as soon as you
 * develop commercial activities involving the iText software without
 * disclosing the source code of your own applications.
 * These activities include: offering paid services to customers as an ASP,
 * serving PDFs on the fly in a web application, shipping iText with a closed
 * source product.
 *
 * For more information, please contact iText Software Corp. at this
 * address: sales@itextpdf.com
 */

namespace System.util {
    /// <summary>
    /// Summary description for Util.
    /// </summary>
    internal static class Util
    {
        public static int USR(int op1, int op2) {        
            if (op2 < 1) {
                return op1;
            } else {
                return unchecked((int)((uint)op1 >> op2));
            }
        }

        public static bool EqualsIgnoreCase(string s1, string s2) {
            return CultureInfo.InvariantCulture.CompareInfo.Compare(s1, s2, CompareOptions.IgnoreCase) == 0;
        }

        public static int CompareToIgnoreCase(string s1, string s2) {
            return CultureInfo.InvariantCulture.CompareInfo.Compare(s1, s2, CompareOptions.IgnoreCase);
        }

        public static CultureInfo GetStandartEnUSLocale() {
            CultureInfo locale = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            //                          en-US                        Invariant
            //=====================     ==================           ==================
            //Currency Symbol           $                            �
            //Currency                  $123456.78                   �123456.78
            //Short Date                1/11/2012                    01/11/2012
            //Time                      10:36:52 PM                  22:36:52
            //Metric                    No                           Yes
            //Long Date                 Wednesday, January 11, 2012  Wednesday, 11 January, 2012
            //Year Month                January, 2012                2012 January
            locale.NumberFormat.CurrencySymbol = "$";
            locale.DateTimeFormat.ShortDatePattern = "M/d/yyyy";
            locale.DateTimeFormat.ShortTimePattern = "h:mm tt";
            locale.DateTimeFormat.LongDatePattern = "dddd, MMMM dd, yyyy";
            locale.DateTimeFormat.YearMonthPattern = "MMMM, yyyy";
            return locale;
        }

        public static int GetArrayHashCode<T>(T[] a) {
            if (a == null)
                return 0;

            int result = 1;
            
            foreach (T element in a) {
                result = 31*result + (element == null ? 0 : element.GetHashCode());
            }

            return result;
        }

        public static bool ArraysAreEqual<T>(T[] a, T[] b) {
            if (a == b)
                return true;

            if (a == null || b == null)
                return false;

            int length = a.Length;
            if (b.Length != length)
                return false;

            for (int i = 0; i < length; i++) {
                Object o1 = a[i];
                Object o2 = b[i];
                if (!(o1 == null ? o2 == null : o1.Equals(o2)))
                    return false;
            }

            return true;
        }
    }
}

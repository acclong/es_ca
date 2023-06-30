using System.Collections.Generic;

/*
 * $Id: IElement.cs 744 2014-05-15 17:11:29Z rafhens $
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
 * a covered work must retain the producer line in every PDF that is created
 * or manipulated using iText.
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
namespace esDigitalSignature.iTextSharp.text {
    /// <summary>
    /// Interface for a text element.
    /// </summary>
    /// <seealso cref="T:esDigitalSignature.iTextSharp.text.Anchor"/>
    /// <seealso cref="T:esDigitalSignature.iTextSharp.text.Cell"/>
    /// <seealso cref="T:esDigitalSignature.iTextSharp.text.Chapter"/>
    /// <seealso cref="T:esDigitalSignature.iTextSharp.text.Chunk"/>
    /// <seealso cref="T:esDigitalSignature.iTextSharp.text.Gif"/>
    /// <seealso cref="T:esDigitalSignature.iTextSharp.text.Graphic"/>
    /// <seealso cref="T:esDigitalSignature.iTextSharp.text.Header"/>
    /// <seealso cref="T:esDigitalSignature.iTextSharp.text.Image"/>
    /// <seealso cref="T:esDigitalSignature.iTextSharp.text.Jpeg"/>
    /// <seealso cref="T:esDigitalSignature.iTextSharp.text.List"/>
    /// <seealso cref="T:esDigitalSignature.iTextSharp.text.ListItem"/>
    /// <seealso cref="T:esDigitalSignature.iTextSharp.text.Meta"/>
    /// <seealso cref="T:esDigitalSignature.iTextSharp.text.Paragraph"/>
    /// <seealso cref="T:esDigitalSignature.iTextSharp.text.Phrase"/>
    /// <seealso cref="T:esDigitalSignature.iTextSharp.text.Rectangle"/>
    /// <seealso cref="T:esDigitalSignature.iTextSharp.text.Row"/>
    /// <seealso cref="T:esDigitalSignature.iTextSharp.text.Section"/>
    /// <seealso cref="T:esDigitalSignature.iTextSharp.text.Table"/>
    public interface IElement {

        // methods
    
        /// <summary>
        /// Processes the element by adding it (or the different parts) to an
        /// IElementListener.
        /// </summary>
        /// <param name="listener">an IElementListener</param>
        /// <returns>true if the element was processed successfully</returns>
        bool Process(IElementListener listener);
    
        /// <summary>
        /// Gets the type of the text element.
        /// </summary>
        /// <value>a type</value>
        int Type {
            get;
        }
    
        /**
        * Checks if this element is a content object.
        * If not, it's a metadata object.
        * @since    iText 2.0.8
        * @return   true if this is a 'content' element; false if this is a 'medadata' element
        */
        
        bool IsContent();
        
        /**
        * Checks if this element is nestable.
        * @since    iText 2.0.8
        * @return   true if this element can be nested inside other elements.
        */
        
        bool IsNestable();
        
        /// <summary>
        /// Gets all the chunks in this element.
        /// </summary>
        /// <value>an ArrayList</value>
        IList<Chunk> Chunks {
            get;
        }
    
        /// <summary>
        /// Gets the content of the text element.
        /// </summary>
        /// <returns>the content of the text element</returns>
        string ToString();
    }
}

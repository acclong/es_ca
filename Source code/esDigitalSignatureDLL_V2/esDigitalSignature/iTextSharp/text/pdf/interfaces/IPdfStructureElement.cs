using System;
using System.Collections.Generic;
using System.Text;

namespace esDigitalSignature.iTextSharp.text.pdf.interfaces
{
    internal interface IPdfStructureElement {
        PdfObject GetAttribute(PdfName name);
        void SetAttribute(PdfName name, PdfObject obj);
	}
}

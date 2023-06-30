using System;

namespace esDigitalSignature.iTextSharp.text.pdf {
    internal interface ICachedColorSpace {
        PdfObject GetPdfObject(PdfWriter writer);
        bool Equals(Object obj);
        int GetHashCode();
    }
}

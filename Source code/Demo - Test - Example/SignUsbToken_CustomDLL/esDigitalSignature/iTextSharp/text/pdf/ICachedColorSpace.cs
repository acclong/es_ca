using System;

namespace esDigitalSignature.iTextSharp.text.pdf {
    public interface ICachedColorSpace {
        PdfObject GetPdfObject(PdfWriter writer);
        bool Equals(Object obj);
        int GetHashCode();
    }
}

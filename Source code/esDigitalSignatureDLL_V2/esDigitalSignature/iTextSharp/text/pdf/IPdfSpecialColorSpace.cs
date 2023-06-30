namespace esDigitalSignature.iTextSharp.text.pdf {
    internal interface IPdfSpecialColorSpace {
        ColorDetails[] GetColorantDetails(PdfWriter writer);
    }
}

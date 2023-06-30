namespace esDigitalSignature.iTextSharp.text.pdf {

    internal class PdfPTableFooter : PdfPTableBody {

        public PdfPTableFooter() : base() {
            role = PdfName.TFOOT;
        }

        public override PdfName Role {
            get { return role; }
            set { this.role = value; }
        }
    }
}

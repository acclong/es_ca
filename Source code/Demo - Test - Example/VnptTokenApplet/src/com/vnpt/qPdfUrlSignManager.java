package com.vnpt;

import java.io.IOException;
import java.io.InputStream;
import java.security.InvalidKeyException;
import java.security.KeyStoreException;
import java.security.NoSuchAlgorithmException;
import java.security.SignatureException;
import java.security.UnrecoverableKeyException;
import java.security.cert.CertificateException;

import com.itextpdf.text.DocumentException;
import com.vnpt.a_pac.aIFileSignManager;
import com.vnpt.b_pac.dSigner;

import sun.security.pkcs11.wrapper.PKCS11Exception;

final class qPdfUrlSignManager extends aIFileSignManager
{
	pPdfUrlSignPrivilege _pr;
	
	qPdfUrlSignManager(pPdfUrlSignPrivilege paramp)
	{
		_pr = paramp;
	}

	public final void sign(String certSerial, String pin, String inputUrl, String outputUrl) 
			throws UnrecoverableKeyException, InvalidKeyException, KeyStoreException, NoSuchAlgorithmException, 
			CertificateException, SignatureException, IOException, PKCS11Exception, DocumentException
	{
		InputStream inputstream = dSigner.downloadFileURL(inputUrl);
		this._pr._applet.setResponse(dSigner.signPdfStreamUrl(certSerial, pin, inputstream, outputUrl, 
				this._pr._temp, this._pr._llx, this._pr._lly, this._pr._width, this._pr._height));
	}
}

/*
 * Location: D:\Code\Visual
 * Studio\CA-VCGM\Doc\VDC\AppletSign-Signbase64-08062015\Sent\VnptTokenApplet.
 * jar!\com\vnpt\q.class
 * Java compiler version: 7 (51.0)
 * JD-Core Version: 0.7.1
 */
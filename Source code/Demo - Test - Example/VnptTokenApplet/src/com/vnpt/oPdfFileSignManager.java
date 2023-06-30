package com.vnpt;

import java.io.IOException;
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

final class oPdfFileSignManager extends aIFileSignManager
{
	nPdfFileSignPrivilege _pr;
	
	oPdfFileSignManager(nPdfFileSignPrivilege paramn)
	{
		_pr = paramn;
	}

	public final void sign(String certSerial, String pin, String filepath, String url) 
			throws UnrecoverableKeyException, InvalidKeyException, KeyStoreException, NoSuchAlgorithmException, 
			CertificateException, SignatureException, IOException, PKCS11Exception, DocumentException
	{
		dSigner.signPdfFileUrl(certSerial, pin, filepath, url, "temp");
	}
}

/*
 * Location: D:\Code\Visual
 * Studio\CA-VCGM\Doc\VDC\AppletSign-Signbase64-08062015\Sent\VnptTokenApplet.
 * jar!\com\vnpt\o.class
 * Java compiler version: 7 (51.0)
 * JD-Core Version: 0.7.1
 */
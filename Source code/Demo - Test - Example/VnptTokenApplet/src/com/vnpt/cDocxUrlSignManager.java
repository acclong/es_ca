package com.vnpt;

import java.io.IOException;
import java.io.InputStream;
import java.security.KeyStoreException;
import java.security.NoSuchAlgorithmException;
import java.security.UnrecoverableKeyException;
import java.security.cert.CertificateException;

import org.apache.poi.openxml4j.exceptions.InvalidFormatException;

import com.vnpt.a_pac.aIFileSignManager;
import com.vnpt.b_pac.dSigner;

import sun.security.pkcs11.wrapper.PKCS11Exception;

final class cDocxUrlSignManager extends aIFileSignManager
{
	bDocxUrlSignPrivilege _pr;
	
	cDocxUrlSignManager(bDocxUrlSignPrivilege paramb)
	{
		_pr = paramb;
	}

	public final void sign(String certSerial, String pin, String inputUrl, String outputUrl) 
			throws UnrecoverableKeyException, KeyStoreException, NoSuchAlgorithmException, 
			CertificateException, InvalidFormatException, IOException, PKCS11Exception
	{
		InputStream inputStream = dSigner.downloadFileURL(inputUrl);
		this._pr._applet.setResponse(dSigner.signDocxStreamUrl(certSerial, pin, inputStream, outputUrl, this._pr._temp));
	}
}

/*
 * Location: D:\Code\Visual
 * Studio\CA-VCGM\Doc\VDC\AppletSign-Signbase64-08062015\Sent\VnptTokenApplet.
 * jar!\com\vnpt\c.class
 * Java compiler version: 7 (51.0)
 * JD-Core Version: 0.7.1
 */
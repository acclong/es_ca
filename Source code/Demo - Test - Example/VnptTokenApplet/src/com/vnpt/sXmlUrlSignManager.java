package com.vnpt;

import java.io.IOException;
import java.io.InputStream;
import java.security.InvalidAlgorithmParameterException;
import java.security.KeyException;
import java.security.KeyStoreException;
import java.security.NoSuchAlgorithmException;
import java.security.UnrecoverableKeyException;
import java.security.cert.CertificateException;

import javax.xml.crypto.MarshalException;
import javax.xml.crypto.dsig.XMLSignatureException;
import javax.xml.parsers.ParserConfigurationException;
import javax.xml.transform.TransformerException;
import javax.xml.transform.TransformerFactoryConfigurationError;

import org.xml.sax.SAXException;

import com.vnpt.a_pac.aIFileSignManager;
import com.vnpt.b_pac.dSigner;

import sun.security.pkcs11.wrapper.PKCS11Exception;

final class sXmlUrlSignManager extends aIFileSignManager
{
	rXmlUrlSignPrivilege _pr;
	
	sXmlUrlSignManager(rXmlUrlSignPrivilege paramr)
	{
		_pr = paramr;
	}

	public final void sign(String certSerial, String pin, String inputUrl, String outputUrl) 
			throws UnrecoverableKeyException, KeyStoreException, NoSuchAlgorithmException, CertificateException, 
			InvalidAlgorithmParameterException, KeyException, IOException, SAXException, ParserConfigurationException, 
			TransformerFactoryConfigurationError, TransformerException, MarshalException, XMLSignatureException, PKCS11Exception
	{
		InputStream inputStream = dSigner.downloadFileURL(inputUrl);
		this._pr._applet.setResponse(dSigner.signXmlStreamUrl(certSerial, pin, inputStream, outputUrl, this._pr._temp));
	}
}

/*
 * Location: D:\Code\Visual
 * Studio\CA-VCGM\Doc\VDC\AppletSign-Signbase64-08062015\Sent\VnptTokenApplet.
 * jar!\com\vnpt\s.class
 * Java compiler version: 7 (51.0)
 * JD-Core Version: 0.7.1
 */
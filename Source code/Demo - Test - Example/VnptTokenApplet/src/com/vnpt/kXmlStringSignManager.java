package com.vnpt;

import java.io.IOException;
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

import com.vnpt.a_pac.bIDataSignManager;
import com.vnpt.b_pac.dSigner;

import sun.security.pkcs11.wrapper.PKCS11Exception;

final class kXmlStringSignManager extends bIDataSignManager
{
	kXmlStringSignManager(jXmlStringSignPrivilege paramj)
	{
	}

	public final String sign(String xmlToSign, String certSerial, String pin) 
			throws UnrecoverableKeyException, KeyStoreException, NoSuchAlgorithmException, CertificateException, 
			InvalidAlgorithmParameterException, KeyException, IOException, SAXException, ParserConfigurationException, 
			TransformerFactoryConfigurationError, TransformerException, MarshalException, XMLSignatureException, PKCS11Exception
	{
		return dSigner.signXmlString(certSerial, pin, xmlToSign);
	}
}

/*
 * Location: D:\Code\Visual
 * Studio\CA-VCGM\Doc\VDC\AppletSign-Signbase64-08062015\Sent\VnptTokenApplet.
 * jar!\com\vnpt\k.class
 * Java compiler version: 7 (51.0)
 * JD-Core Version: 0.7.1
 */
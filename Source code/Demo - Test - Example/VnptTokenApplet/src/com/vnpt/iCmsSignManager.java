package com.vnpt;

import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.security.InvalidAlgorithmParameterException;
import java.security.KeyStoreException;
import java.security.NoSuchAlgorithmException;
import java.security.NoSuchProviderException;
import java.security.UnrecoverableKeyException;
import java.security.cert.CertStoreException;
import java.security.cert.CertificateException;

import org.bouncycastle.cms.CMSException;

import com.vnpt.a_pac.bIDataSignManager;
import com.vnpt.b_pac.dSigner;
import com.vnpt.common.aBase64;

import sun.security.pkcs11.wrapper.PKCS11Exception;

final class iCmsSignManager extends bIDataSignManager
{
	iCmsSignManager(hCmsSignPrivilege paramh)
	{
	}

	public final String sign(String cmsString, String certSerial, String pin) 
			throws UnrecoverableKeyException, KeyStoreException, NoSuchAlgorithmException, CertificateException, 
			InvalidAlgorithmParameterException, NoSuchProviderException, CertStoreException, UnsupportedEncodingException, 
			IOException, CMSException, PKCS11Exception
	{
		byte[] signedData = dSigner.signBytesCMS(certSerial, pin, cmsString.getBytes("UTF-8"));
		String signedBase64 = null;
		if (signedData != null)
		{
			signedBase64 = new String(aBase64.convertBytesToBase64(signedData));
		}
		return signedBase64;
	}
}

/*
 * Location: D:\Code\Visual
 * Studio\CA-VCGM\Doc\VDC\AppletSign-Signbase64-08062015\Sent\VnptTokenApplet.
 * jar!\com\vnpt\i.class
 * Java compiler version: 7 (51.0)
 * JD-Core Version: 0.7.1
 */
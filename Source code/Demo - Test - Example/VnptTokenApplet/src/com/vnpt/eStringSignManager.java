package com.vnpt;

import java.io.UnsupportedEncodingException;

import com.vnpt.a_pac.bIDataSignManager;
import com.vnpt.common.aBase64;

import sun.security.pkcs11.wrapper.PKCS11Exception;

final class eStringSignManager extends bIDataSignManager
{
	eStringSignManager(dStringSignPrivilege paramd)
	{
	}

	public final String sign(String stringToSign, String certSerial, String pin) 
			throws UnsupportedEncodingException, PKCS11Exception
	{
		byte[] signedData = com.vnpt.b_pac.dSigner.signBytes(certSerial, pin, stringToSign.getBytes("UTF-8"));
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
 * jar!\com\vnpt\e.class
 * Java compiler version: 7 (51.0)
 * JD-Core Version: 0.7.1
 */
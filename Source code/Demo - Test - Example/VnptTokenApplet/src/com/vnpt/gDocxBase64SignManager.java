package com.vnpt;

import com.vnpt.a_pac.bIDataSignManager;
import com.vnpt.b_pac.dSigner;
import com.vnpt.common.aBase64;

import sun.security.pkcs11.wrapper.PKCS11Exception;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.security.KeyStoreException;
import java.security.NoSuchAlgorithmException;
import java.security.UnrecoverableKeyException;
import java.security.cert.CertificateException;

import org.apache.poi.openxml4j.exceptions.InvalidFormatException;

final class gDocxBase64SignManager extends bIDataSignManager
{
	gDocxBase64SignManager(fDocxBase64SignPrivilege paramf)
	{
	}

	public final String sign(String base64, String certSerial, String pin) 
			throws UnrecoverableKeyException, KeyStoreException, NoSuchAlgorithmException, 
			CertificateException, InvalidFormatException, IOException, PKCS11Exception
	{
		byte[] dataToSign = aBase64.convertBase64ToBytes(base64);
		ByteArrayInputStream bytesInputStream = new ByteArrayInputStream(dataToSign);
		ByteArrayOutputStream bytesOutputStream = new ByteArrayOutputStream();
		
		dSigner.signDocxStreamInOut(certSerial, pin, bytesInputStream, bytesOutputStream);
		byte[] outBytes = bytesOutputStream.toByteArray();
		String outBase64 = null;
		if (outBytes != null)
		{
			outBase64 = new String(aBase64.convertBytesToBase64(outBytes));
		}
		return outBase64;
	}
}

/*
 * Location: D:\Code\Visual
 * Studio\CA-VCGM\Doc\VDC\AppletSign-Signbase64-08062015\Sent\VnptTokenApplet.
 * jar!\com\vnpt\g.class
 * Java compiler version: 7 (51.0)
 * JD-Core Version: 0.7.1
 */
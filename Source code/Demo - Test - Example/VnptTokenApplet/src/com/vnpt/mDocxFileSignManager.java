package com.vnpt;

import java.io.IOException;
import java.security.KeyStoreException;
import java.security.NoSuchAlgorithmException;
import java.security.UnrecoverableKeyException;
import java.security.cert.CertificateException;

import org.apache.poi.openxml4j.exceptions.InvalidFormatException;

import com.vnpt.a_pac.aIFileSignManager;
import com.vnpt.b_pac.dSigner;

import sun.security.pkcs11.wrapper.PKCS11Exception;

final class mDocxFileSignManager extends aIFileSignManager
{
	mDocxFileSignManager(lDocxFileSignPrivilege paraml)
	{
	}

	public final void sign(String certSerial, String pin, String inputFilePath, String outputFilePath) 
			throws UnrecoverableKeyException, KeyStoreException, NoSuchAlgorithmException, CertificateException, 
			InvalidFormatException, IOException, PKCS11Exception
	{
		dSigner.signDocxFile(certSerial, pin, inputFilePath, outputFilePath);
	}
}

/*
 * Location: D:\Code\Visual
 * Studio\CA-VCGM\Doc\VDC\AppletSign-Signbase64-08062015\Sent\VnptTokenApplet.
 * jar!\com\vnpt\m.class
 * Java compiler version: 7 (51.0)
 * JD-Core Version: 0.7.1
 */
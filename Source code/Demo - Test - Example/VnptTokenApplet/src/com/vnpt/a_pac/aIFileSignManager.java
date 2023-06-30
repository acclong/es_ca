package com.vnpt.a_pac;

import com.itextpdf.text.DocumentException;
import com.vnpt.common.cDungChung;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.security.InvalidAlgorithmParameterException;
import java.security.InvalidKeyException;
import java.security.KeyException;
import java.security.KeyStoreException;
import java.security.NoSuchAlgorithmException;
import java.security.SignatureException;
import java.security.UnrecoverableKeyException;
import java.security.cert.CertificateException;

import javax.swing.JOptionPane;
import javax.swing.JPasswordField;
import javax.xml.crypto.MarshalException;
import javax.xml.crypto.dsig.XMLSignatureException;
import javax.xml.parsers.ParserConfigurationException;
import javax.xml.transform.TransformerException;
import javax.xml.transform.TransformerFactoryConfigurationError;

import org.apache.poi.openxml4j.exceptions.InvalidFormatException;
import org.xml.sax.SAXException;

import sun.security.pkcs11.wrapper.PKCS11Exception;

public abstract class aIFileSignManager
{
	private static String _sPIN;

	public final void sign(String certSerial, String inputPath, String outputPath) throws Exception
	{
		if (cDungChung.isNullOrEmpty(certSerial))
		{
			throw new FileNotFoundException();
		}
		
		JPasswordField fieldPassword = new JPasswordField(10);
		fieldPassword.setFocusable(true);
		fieldPassword.setActionCommand("ok");
		if (cDungChung.isNullOrEmpty(_sPIN))
		{
			Object[] options = new Object[] { "OK", "Cancel" };
			if (JOptionPane.showOptionDialog(null, fieldPassword, "Hãy nhập mã PIN của thiết bị:", 2, 3, null, (Object[]) options, options[0]) == 1)
			{
				return;
			}
			_sPIN = new String(fieldPassword.getPassword());
		}
		if (cDungChung.isNullOrEmpty(_sPIN))
		{
			JOptionPane.showMessageDialog(null, "Trường mã PIN bắt buộc nhập!");
			sign(certSerial, inputPath, outputPath);
		}
		try
		{
			sign(certSerial, _sPIN, inputPath, outputPath);
			return;
		}
		catch (Exception ex)
		{
			if (ex instanceof PKCS11Exception)
			{
				PKCS11Exception pkcs11Ex = (PKCS11Exception) ex;
				if ((pkcs11Ex.getErrorCode() == 160L) || (pkcs11Ex.getErrorCode() == 161L))
				{
					JOptionPane.showMessageDialog(null, "Mã PIN không đúng!", "Error", JOptionPane.ERROR_MESSAGE);
					_sPIN = null;
					sign(certSerial, inputPath, outputPath);
				}
				else if (pkcs11Ex.getErrorCode() == 162L)
				{
					JOptionPane.showMessageDialog(null, "Mã PIN có độ dài không phù hợp!", "Error", JOptionPane.ERROR_MESSAGE);
					_sPIN = null;
					sign(certSerial, inputPath, outputPath);
				}
				else if (pkcs11Ex.getErrorCode() == 164L)
				{
					JOptionPane.showMessageDialog(null, "Mã PIN đã bị lock!", "Error", JOptionPane.ERROR_MESSAGE);
					_sPIN = null;
				}
				else
				{
					throw ex;
				}
			}
			else
			{
				throw ex;
			}
		}
	}

	public abstract void sign(String certSerial, String sPIN, String inputPath, String outputPath) 
			throws UnrecoverableKeyException, KeyStoreException, NoSuchAlgorithmException, CertificateException, 
			InvalidFormatException, IOException, PKCS11Exception, InvalidKeyException, SignatureException, 
			DocumentException, InvalidAlgorithmParameterException, KeyException, SAXException, ParserConfigurationException, 
			TransformerFactoryConfigurationError, TransformerException, MarshalException, XMLSignatureException;
}

/*
 * Location: D:\Code\Visual
 * Studio\CA-VCGM\Doc\VDC\AppletSign-Signbase64-08062015\Sent\VnptTokenApplet.
 * jar!\com\vnpt\a\a.class
 * Java compiler version: 7 (51.0)
 * JD-Core Version: 0.7.1
 */
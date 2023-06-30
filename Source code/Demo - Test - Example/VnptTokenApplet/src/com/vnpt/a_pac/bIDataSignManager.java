package com.vnpt.a_pac;

import com.vnpt.common.cDungChung;

import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.security.InvalidAlgorithmParameterException;
import java.security.KeyException;
import java.security.KeyStoreException;
import java.security.NoSuchAlgorithmException;
import java.security.NoSuchProviderException;
import java.security.UnrecoverableKeyException;
import java.security.cert.CertStoreException;
import java.security.cert.CertificateException;

import javax.swing.JOptionPane;
import javax.swing.JPasswordField;
import javax.xml.crypto.MarshalException;
import javax.xml.crypto.dsig.XMLSignatureException;
import javax.xml.parsers.ParserConfigurationException;
import javax.xml.transform.TransformerException;
import javax.xml.transform.TransformerFactoryConfigurationError;

import org.apache.poi.openxml4j.exceptions.InvalidFormatException;
import org.bouncycastle.cms.CMSException;
import org.xml.sax.SAXException;

import sun.security.pkcs11.wrapper.PKCS11Exception;

public abstract class bIDataSignManager
{
	public final String sign(String certSerial, String stringToSign) throws Exception
	{
		String str;
		for (;;)
		{
			str = "";
			if ((cDungChung.isNullOrEmpty(stringToSign)) || (cDungChung.isNullOrEmpty(certSerial)))
			{
				return str;
			}
			
			JPasswordField fieldPassword = new JPasswordField(10);
			fieldPassword.setActionCommand("ok");
			Object[] options = { "OK", "Cancel" };
			if (JOptionPane.showOptionDialog(null, fieldPassword, "Hãy nhập mã PIN của thiết bị:", 2, 3, null, options, options[0]) == 1)
			{
				return str;
			}
			String sPIN = new String(fieldPassword.getPassword());
			if (cDungChung.isNullOrEmpty(sPIN))
			{
				JOptionPane.showMessageDialog(null, "Trường mã PIN bắt buộc nhập!");
				//paramString1 = paramString1;
				//this = this;
			}
			else
			{
				try
				{
					return sign(stringToSign, certSerial, sPIN);
				}
				catch (Exception ex)
				{
					if (ex instanceof PKCS11Exception)
					{
						PKCS11Exception pkcs11Ex = (PKCS11Exception) ex;
						if ((pkcs11Ex.getErrorCode() == 160L) || (pkcs11Ex.getErrorCode() == 161L))
						{
							JOptionPane.showMessageDialog(null, "Mã PIN không đúng!", "Error", JOptionPane.ERROR_MESSAGE);
							//paramString1 = paramString1;
							//this = this;
							continue;
						}
						if (pkcs11Ex.getErrorCode() == 162L)
						{
							JOptionPane.showMessageDialog(null, "Mã PIN có độ dài không phù hợp!", "Error", JOptionPane.ERROR_MESSAGE);
							//paramString1 = paramString1;
							//this = this;
							continue;
						}
						if (pkcs11Ex.getErrorCode() == 164L)
						{
							JOptionPane.showMessageDialog(null, "Mã PIN đã bị lock!", "Error", JOptionPane.ERROR_MESSAGE);
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
				
				if (!"".equals(str))
				{
					break;
				}
				//paramString1 = paramString1;
				//this = this;
			}
		}
		return str;
	}

	public abstract String sign(String paramString1, String certSerial, String sPIN) 
			throws UnsupportedEncodingException, PKCS11Exception, UnrecoverableKeyException, KeyStoreException, 
			NoSuchAlgorithmException, CertificateException, InvalidFormatException, IOException, 
			InvalidAlgorithmParameterException, KeyException, SAXException, ParserConfigurationException, 
			TransformerFactoryConfigurationError, TransformerException, MarshalException, XMLSignatureException, 
			NoSuchProviderException, CertStoreException, CMSException;
}

/*
 * Location: D:\Code\Visual
 * Studio\CA-VCGM\Doc\VDC\AppletSign-Signbase64-08062015\Sent\VnptTokenApplet.
 * jar!\com\vnpt\a\b.class
 * Java compiler version: 7 (51.0)
 * JD-Core Version: 0.7.1
 */
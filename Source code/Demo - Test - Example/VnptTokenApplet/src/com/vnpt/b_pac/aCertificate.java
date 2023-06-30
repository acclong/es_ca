package com.vnpt.b_pac;

import java.io.IOException;
import java.io.StringWriter;
import java.io.Writer;
//import java.math.BigInteger;
import java.security.KeyStore;
import java.security.KeyStoreException;
import java.security.NoSuchAlgorithmException;
import java.security.PrivateKey;
import java.security.PublicKey;
import java.security.UnrecoverableKeyException;
import java.security.cert.CertificateException;
import java.security.cert.X509Certificate;
import java.util.Enumeration;
//import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.transform.Result;
import javax.xml.transform.Source;
import javax.xml.transform.Transformer;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.dom.DOMSource;
import javax.xml.transform.stream.StreamResult;
import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.Node;

public final class aCertificate
{
	private String _serialNumber;
	private String _subject;
	private String _friendlyName;
	private String _issuer;
	private String _validFrom;
	private String _validTo;
	private String _base64;
	private cToken _token;
	private PrivateKey _privateKey;
	private PublicKey _publicKey;
	private X509Certificate[] _lstCertChain;
	private X509Certificate _cert;

	public final void loadPrivateKey(String pin) throws KeyStoreException, NoSuchAlgorithmException, CertificateException, IOException, UnrecoverableKeyException
	{
		KeyStore keyStore = this._token.getKetStore();
		keyStore.load(null, pin.toCharArray());
		Enumeration<String> enumeration = keyStore.aliases();
		while (enumeration.hasMoreElements())
		{
			String str = (String) enumeration.nextElement();
			String serialNumber = com.vnpt.common.cDungChung.convertBytesToHex(((X509Certificate) keyStore.getCertificate(str)).getSerialNumber().toByteArray());
			if (serialNumber.equals(this._serialNumber))
			{
				X509Certificate[] lstCertChain = (X509Certificate[])keyStore.getCertificateChain(str);
				this._lstCertChain = lstCertChain;
				
				PrivateKey privateKey = (PrivateKey)keyStore.getKey(str, pin.toCharArray());
				this._privateKey = privateKey;
			}
		}
	}

	public final X509Certificate[] getCertificateChain()
	{
		return this._lstCertChain;
	}

	public final PrivateKey getPrivateKey()
	{
		return this._privateKey;
	}

	public final PublicKey getPublicKey()
	{
		return this._publicKey;
	}

	public final void setPublicKey(PublicKey key)
	{
		this._publicKey = key;
	}

	public final cToken getToken()
	{
		return this._token;
	}

	public final void setToken(cToken token)
	{
		this._token = token;
	}

	public final String getFriendlyName()
	{
		return this._friendlyName;
	}

	public final void setFriendlyName(String friendlyName)
	{
		this._friendlyName = friendlyName;
	}

	public final void setBase64(String base64)
	{
		this._base64 = base64;
	}

	public final String getIssuer()
	{
		return this._issuer;
	}

	public final void setIssuer(String issuer)
	{
		this._issuer = issuer;
	}

	public final String getSerialNumber()
	{
		return this._serialNumber;
	}

	public final void setSerialNumber(String serialNumber)
	{
		this._serialNumber = serialNumber;
	}

	public final void setSubject(String subject)
	{
		this._subject = subject;
	}

	public final void setValidFrom(String validFrom)
	{
		this._validFrom = validFrom;
	}

	public final String getValidTo()
	{
		return this._validTo;
	}

	public final void setValidTo(String validTo)
	{
		this._validTo = validTo;
	}

	public final String getCertificateXml()
	{
		try
		{
			Document doc = DocumentBuilderFactory.newInstance().newDocumentBuilder().newDocument();
			Element cert = doc.createElement("certificate");
			doc.appendChild((Node) cert);
			Element certInfo;
			//Object localObject4 = this;
			(certInfo = doc.createElement("dllPath")).appendChild(doc.createTextNode(this._token.getTokenDriver().getDllPath()));
			cert.appendChild((Node) certInfo);
			//localObject4 = this;
			(certInfo = doc.createElement("slotId")).appendChild(doc.createTextNode(String.valueOf(this._token.getSlotId())));
			cert.appendChild((Node) certInfo);
			//localObject4 = this;
			(certInfo = doc.createElement("manufactureId")).appendChild(doc.createTextNode(com.vnpt.common.cDungChung.trim(this._token.getManufactureId())));
			cert.appendChild((Node) certInfo);
			//localObject4 = this;
			(certInfo = doc.createElement("serialNumber")).appendChild(doc.createTextNode(com.vnpt.common.cDungChung.trim(this._serialNumber)));
			cert.appendChild((Node) certInfo);
			//localObject4 = this;
			(certInfo = doc.createElement("deviceSerial")).appendChild(doc.createTextNode(com.vnpt.common.cDungChung.trim(this._token.getTokenSerial())));
			cert.appendChild((Node) certInfo);
			//localObject4 = this;
			(certInfo = doc.createElement("subject")).appendChild(doc.createTextNode(this._subject));
			cert.appendChild((Node) certInfo);
			//localObject4 = this;
			(certInfo = doc.createElement("friendlyName")).appendChild(doc.createTextNode(this._friendlyName));
			cert.appendChild((Node) certInfo);
			//localObject4 = this;
			(certInfo = doc.createElement("issuer")).appendChild(doc.createTextNode(this._issuer));
			cert.appendChild((Node) certInfo);
			//localObject4 = this;
			(certInfo = doc.createElement("validFrom")).appendChild(doc.createTextNode(this._validFrom));
			cert.appendChild((Node) certInfo);
			//localObject4 = this;
			(certInfo = doc.createElement("validTo")).appendChild(doc.createTextNode(this._validTo));
			cert.appendChild((Node) certInfo);
			//localObject4 = this;
			(certInfo = doc.createElement("base64")).appendChild(doc.createTextNode(this._base64));
			cert.appendChild((Node) certInfo);
			
			Transformer transformer = TransformerFactory.newInstance().newTransformer();
			transformer.setOutputProperty("omit-xml-declaration", "yes");
			transformer.setOutputProperty("indent", "yes");
			StringWriter writer = new StringWriter();
			StreamResult result = new StreamResult((Writer) writer);
			DOMSource source = new DOMSource((Node) doc);
			transformer.transform((Source) source, (Result) result);
			return writer.toString();
		}
		catch (Exception ex)
		{
		}
		return "";
	}

	public final X509Certificate getCertificate()
	{
		return this._cert;
	}

	public final void setCertificate(X509Certificate cert)
	{
		this._cert = cert;
	}
}

/*
 * Location: D:\Code\Visual
 * Studio\CA-VCGM\Doc\VDC\AppletSign-Signbase64-08062015
 * \Sent\VnptTokenApplet.jar!\com\vnpt\b\a.class
 * Java compiler version: 7 (51.0)
 * JD-Core Version: 0.7.1
 */
package com.vnpt.b_pac;

//import com.itextpdf.text.D;
//import com.itextpdf.text.DocumentException;
//import com.itextpdf.text.cPdf.aP;
//import com.itextpdf.text.cPdf.aX;
////import com.itextpdf.text.cPdf.aY;
//import com.itextpdf.text.cPdf.ap;
////import com.itextpdf.text.cPdf.bk;
//import com.itextpdf.text.cPdf.bo;
//import com.itextpdf.text.cPdf.bq;
//import com.itextpdf.text.cPdf.bu;
//import com.itextpdf.text.cPdf.be;
//import com.itextpdf.text.cPdf.f;
//import com.itextpdf.text.n;
//import com.sun.org.apache.xml.internal.security.signature.Reference;
//import com.vnpt.common.aBase64;
//import com.vnpt.docx.opc.b;
//import com.vnpt.docx.opc.signature.e;
import java.io.BufferedReader;
import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
//import java.io.PrintStream;
import java.io.StringReader;
import java.io.StringWriter;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
//import java.math.BigInteger;
import java.net.HttpURLConnection;
//import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLConnection;
import java.nio.charset.StandardCharsets;
import java.security.InvalidAlgorithmParameterException;
import java.security.InvalidKeyException;
import java.security.KeyException;
import java.security.KeyStore;
import java.security.KeyStoreException;
import java.security.NoSuchAlgorithmException;
import java.security.NoSuchProviderException;
//import java.security.Principal;
import java.security.Security;
import java.security.Signature;
import java.security.SignatureException;
import java.security.UnrecoverableKeyException;
import java.security.cert.CertStore;
import java.security.cert.CertStoreException;
import java.security.cert.CertificateException;
import java.security.cert.CertificateFactory;
import java.security.cert.CollectionCertStoreParameters;
import java.security.cert.X509Certificate;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Calendar;
import java.util.Collections;
import java.util.Date;
import java.util.List;
//import java.util.logging.Level;

import javax.xml.crypto.MarshalException;
import javax.xml.crypto.XMLStructure;
//import javax.xml.crypto.dsig.XMLSignature;
import javax.xml.crypto.dsig.XMLSignatureException;
import javax.xml.crypto.dsig.XMLSignatureFactory;
import javax.xml.crypto.dsig.dom.DOMSignContext;
import javax.xml.crypto.dsig.keyinfo.KeyInfoFactory;
//import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;
import javax.xml.transform.Transformer;
//import javax.xml.transform.TransformerConfigurationException;
import javax.xml.transform.TransformerException;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.TransformerFactoryConfigurationError;
import javax.xml.transform.dom.DOMResult;
import javax.xml.transform.dom.DOMSource;
import javax.xml.transform.stream.StreamResult;
import javax.xml.transform.stream.StreamSource;
//import org.bouncycastle.a.o;
//import org.bouncycastle.a.p;
//import org.bouncycastle.a.u;
import org.w3c.dom.Document;
import org.xml.sax.InputSource;
import org.xml.sax.SAXException;

import sun.security.pkcs11.SunPKCS11;
import sun.security.pkcs11.wrapper.CK_ATTRIBUTE;
import sun.security.pkcs11.wrapper.CK_C_INITIALIZE_ARGS;
import sun.security.pkcs11.wrapper.CK_MECHANISM;
import sun.security.pkcs11.wrapper.CK_TOKEN_INFO;
import sun.security.pkcs11.wrapper.PKCS11;
import sun.security.pkcs11.wrapper.PKCS11Exception;

import org.bouncycastle.cms.CMSSignedDataGenerator;
import org.bouncycastle.cms.CMSException;
import org.bouncycastle.cms.CMSProcessableByteArray;
import org.bouncycastle.cms.CMSSignedData;

import com.itextpdf.text.Rectangle;						//D
import com.itextpdf.text.DocumentException;
import com.itextpdf.text.pdf.PdfName;					//aP
import com.itextpdf.text.pdf.PdfPKCS7;					//aX
import com.itextpdf.text.pdf.PdfDictionary;				//ap
import com.itextpdf.text.pdf.PdfSignatureAppearance;	//bo
import com.itextpdf.text.pdf.PdfStamper;				//bq
import com.itextpdf.text.pdf.PdfString;					//bu
import com.itextpdf.text.pdf.PdfReader;					//be
import com.itextpdf.text.pdf.BaseFont;					//f
import com.itextpdf.text.Font;							//n

import org.apache.poi.openxml4j.exceptions.InvalidFormatException;
import org.apache.poi.poifs.crypt.dsig.SignatureConfig;

// Referenced classes of package com.vnpt.b:
//            a, b, c, e

public class dSigner
{

	public dSigner()
	{
		
	}

	public static void loadDLL(ArrayList<String> listDllPath) 
			throws IllegalAccessException, IllegalArgumentException, InvocationTargetException, 
			IOException, KeyStoreException, PKCS11Exception
	{
		for (int i = 0; i < listDllPath.size(); i++)
		{
			if ((new File(listDllPath.get(i))).exists())
			{
				com.vnpt.b_pac.bTokenDriver tokenDriver = new com.vnpt.b_pac.bTokenDriver();
				PKCS11 pkcs11 = loadPKCS11(listDllPath.get(i));
				tokenDriver.setPKCS11(pkcs11);
				tokenDriver.setDllPath((String) listDllPath.get(i));
				TokenDrivers.add(tokenDriver);
				loadToken(tokenDriver);
			}
		}
	}

	public static byte[] signBytes(String certSerial, String pin, byte dataToSign[]) throws PKCS11Exception
	{
		long session;
		long slotId;
		
		com.vnpt.b_pac.aCertificate cert = getCertificate(certSerial);
		com.vnpt.b_pac.cToken token = cert.getToken();
		slotId = token.getSlotId();
		session = openSession(token.getSlotId());		
		login(slotId, session, pin);
		PKCS11 pkcs11 = token.getTokenDriver().getPKCS11();		
		
		CK_ATTRIBUTE[] keyAttributes = (new CK_ATTRIBUTE[] {
			new CK_ATTRIBUTE(0L, 3L)
		});
		pkcs11.C_FindObjectsInit(session, keyAttributes);
		long[] lstObj = pkcs11.C_FindObjects(session, _MaxObjectCount);
		pkcs11.C_FindObjectsFinal(session);		
		long key = lstObj == null || lstObj.length <= 0 ? 0L : lstObj[0];
		
		CK_MECHANISM signMech = new CK_MECHANISM(6L);
		pkcs11.C_SignInit(session, signMech, key);
		byte[] signedData = pkcs11.C_Sign(session, dataToSign);
		
		logout(slotId, session);
		closeSession(slotId, session);
		// break MISSING_BLOCK_LABEL_197;
		// s;
		// a(l1, l);
		// throw s;
		return signedData;
	}

	@SuppressWarnings("deprecation")
	public static byte[] signBytesCMS(String certSerial, String pin, byte signData[]) 
			throws IOException, UnrecoverableKeyException, KeyStoreException, NoSuchAlgorithmException, CertificateException, 
			InvalidAlgorithmParameterException, NoSuchProviderException, CertStoreException, CMSException, PKCS11Exception
	{
		long session;
		long slotId;
		
		aCertificate cert = getCertificate(certSerial);
		session = openSession(cert.getToken().getSlotId());
		slotId = cert.getToken().getSlotId();
		login(slotId, session, pin);
		logout(slotId, session);
		closeSession(slotId, session);
		cert.loadPrivateKey(pin);
		// break MISSING_BLOCK_LABEL_72;
		// s1;
		// a(l1, l);
		// throw s1;		
		
		CMSSignedDataGenerator generator = new CMSSignedDataGenerator();
		generator.addSigner(cert.getPrivateKey(), cert.getCertificate(), CMSSignedDataGenerator.DIGEST_SHA1);
		ArrayList<X509Certificate> lstCertChain = new ArrayList<>();
		X509Certificate[] certChain = cert.getCertificateChain();
		int j = certChain != null ? certChain.length : 0;
		for (int i = 0; i < j; i++)
			lstCertChain.add(certChain[i]);

		CertStore certstore = CertStore.getInstance("Collection", new CollectionCertStoreParameters(lstCertChain), "BC");
		generator.addCertificatesAndCRLs(certstore);
		CMSProcessableByteArray cmsByteArr = new CMSProcessableByteArray(signData);
		CMSSignedData signedData = generator.generate(cmsByteArr, true, "SunPKCS11-SmartCard");
		return signedData.getEncoded();
		// JVM INSTR pop ;
		// com.vnpt.b_pac.d.getName();
		// Level.SEVERE;
		// throw new PKCS11Exception(161L);
	}

	public static int signXmlStreamUrl(String certSerial, String pin, InputStream inputstream, String sUrl, String filename) 
			throws UnrecoverableKeyException, KeyStoreException, NoSuchAlgorithmException, CertificateException, 
			InvalidAlgorithmParameterException, KeyException, IOException, SAXException, ParserConfigurationException, 
			TransformerFactoryConfigurationError, TransformerException, MarshalException, XMLSignatureException, PKCS11Exception
	{
		String xmlString = readStreamToString(inputstream);
		String signedXmlString = signXmlString(certSerial, pin, xmlString);
		ByteArrayInputStream stream = new ByteArrayInputStream(signedXmlString.getBytes(StandardCharsets.UTF_8));
		return uploadFileURL((InputStream) stream, filename, sUrl);
	}

	public static String signXmlString(String certSerial, String pin, String xmlToSign) 
			throws UnrecoverableKeyException, KeyStoreException, NoSuchAlgorithmException, CertificateException, IOException, 
			SAXException, ParserConfigurationException, TransformerFactoryConfigurationError, TransformerException, 
			InvalidAlgorithmParameterException, KeyException, MarshalException, XMLSignatureException, PKCS11Exception
	{
		long session;
		long slotId;
		
		aCertificate cert = getCertificate(certSerial);
		session = openSession(cert.getToken().getSlotId());
		slotId = cert.getToken().getSlotId();
		login(slotId, session, pin);
		logout(slotId, session);
		closeSession(slotId, session);
		// break MISSING_BLOCK_LABEL_61;
		// s1;
		// a(l1, l);
		// throw s1;
		cert.loadPrivateKey(pin);
		
		InputSource inputSource = new InputSource();
		inputSource.setCharacterStream(new StringReader(xmlToSign));
		Document doc = DocumentBuilderFactory.newInstance().newDocumentBuilder().parse(inputSource);
		
		String objectTag = "<xsl:stylesheet version=\"1.0\"\r\nxmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\">\r\n<xsl:output method=\"xml\" omit-xml-declaration=\"yes\"/>\r\n<xsl:strip-space elements=\"*\"/>\r\n<xsl:template match=\"@*|node()\">\r\n<xsl:copy>\r\n<xsl:apply-templates select=\"@*|node()\"/>\r\n</xsl:copy>\r\n</xsl:template>\r\n</xsl:stylesheet>";
		StreamSource streamSource = new StreamSource(new StringReader(objectTag));
		Transformer transformer = TransformerFactory.newInstance().newTransformer(streamSource);
		DOMResult domResult = new DOMResult();
		transformer.transform(new DOMSource(doc), domResult);
		
		Document docSignature = (Document) domResult.getNode();
		XMLSignatureFactory xmlSignFactory = XMLSignatureFactory.getInstance("DOM");
		javax.xml.crypto.dsig.Reference reference = xmlSignFactory.newReference("", xmlSignFactory.newDigestMethod("http://www.w3.org/2000/09/xmldsig#sha1", null), Collections.singletonList(xmlSignFactory.newTransform("http://www.w3.org/2000/09/xmldsig#enveloped-signature", (XMLStructure) null)), null, null);
		javax.xml.crypto.dsig.SignedInfo signedInfo = xmlSignFactory.newSignedInfo(xmlSignFactory.newCanonicalizationMethod("http://www.w3.org/TR/2001/REC-xml-c14n-20010315", (XMLStructure)null), xmlSignFactory.newSignatureMethod("http://www.w3.org/2000/09/xmldsig#rsa-sha1", null), Collections.singletonList(reference));
		KeyInfoFactory keyInfoFactory = xmlSignFactory.getKeyInfoFactory();
		javax.xml.crypto.dsig.keyinfo.KeyValue keyValue = keyInfoFactory.newKeyValue(cert.getPublicKey());
		ArrayList<X509Certificate> lstCert = new ArrayList<>();
		lstCert.add(cert.getCertificate());
		javax.xml.crypto.dsig.keyinfo.X509Data x509Data = keyInfoFactory.newX509Data(lstCert);
		ArrayList<Object> lstKeyInfo = new ArrayList<>();
		lstKeyInfo.add(keyValue);
		lstKeyInfo.add(x509Data);
		javax.xml.crypto.dsig.keyinfo.KeyInfo keyInfo = keyInfoFactory.newKeyInfo(lstKeyInfo);
		javax.xml.crypto.dsig.XMLSignature xmlSignature = xmlSignFactory.newXMLSignature(signedInfo, keyInfo);
		DOMSignContext signContext = new DOMSignContext(cert.getPrivateKey(), docSignature.getDocumentElement());
		xmlSignature.sign(signContext);
		
		StringWriter writer = new StringWriter();
		StreamResult result = new StreamResult(writer);
		transformer.transform(new DOMSource(docSignature), result);
		return writer.toString();
		// JVM INSTR pop ;
		// com/vnpt/b/d.getName();
		// Level.SEVERE;
		// throw new PKCS11Exception(161L);
	}

	public static void signDocxFile(String certSerial, String pin, String inputFilePath, String outputFilePath) 
			throws UnrecoverableKeyException, KeyStoreException, NoSuchAlgorithmException, CertificateException, 
			IOException, PKCS11Exception, InvalidFormatException
	{
		long session;
		long slotId;
		
		aCertificate cert = getCertificate(certSerial);
		session = openSession(cert.getToken().getSlotId());
		slotId = cert.getToken().getSlotId();
		login(slotId, session, pin);
		logout(slotId, session);
		closeSession(slotId, session);
		cert.loadPrivateKey(pin);
		// break MISSING_BLOCK_LABEL_61;
		// s1;
		// a(l1, l);
		// throw s1;
		
		FileInputStream stream = new FileInputStream(inputFilePath);
		//System.out.println((new StringBuilder("file to stream ")).append(stream.toString()).toString());
//		com.vnpt.docx.opc.a a1 = com.vnpt.docx.opc.a.a(stream, b.c);
//		e e1 = new e(a1);
//		e1.a(cert.getPrivateKey(), cert.getCertificate(), null);
//		e1.a().a(new FileOutputStream(outputFilePath));
		
		org.apache.poi.openxml4j.opc.OPCPackage opcPackage = org.apache.poi.openxml4j.opc.OPCPackage.open(stream);
		SignatureConfig signConfig = new SignatureConfig();
		signConfig.setOpcPackage(opcPackage);
		signConfig.setKey(cert.getPrivateKey());
		signConfig.setSigningCertificateChain(Arrays.asList(cert.getCertificateChain()));
		signConfig.getOpcPackage().save(new FileOutputStream(outputFilePath));
		
		return;
		// JVM INSTR pop ;
		// com/vnpt/b/d.getName();
		// Level.SEVERE;
		// throw new PKCS11Exception(161L);
	}

	public static int signDocxStreamUrl(String certSerial, String pin, InputStream inputStream, String sUrl, String filename) 
			throws IOException, UnrecoverableKeyException, KeyStoreException, NoSuchAlgorithmException, CertificateException, 
			PKCS11Exception, InvalidFormatException
	{
		long session;
		long slotId;
		
		aCertificate cert = getCertificate(certSerial);
		session = openSession(cert.getToken().getSlotId());
		slotId = cert.getToken().getSlotId();
		login(slotId, session, pin);
		logout(slotId, session);
		closeSession(slotId, session);
		cert.loadPrivateKey(pin);
		// break MISSING_BLOCK_LABEL_63;
		// Exception exception;
		// exception;
		// a(l1, l);
		// throw exception;
		
		ByteArrayOutputStream outputStream = new ByteArrayOutputStream();
		
//		com.vnpt.docx.opc.a a1 = com.vnpt.docx.opc.a.a(inputStream, b.c);
//		e e1 = new e(a1);
//		e1.a(cert.getPrivateKey(), cert.getCertificate(), null);
//		e1.a().a(outputStream);
		
		org.apache.poi.openxml4j.opc.OPCPackage opcPackage = org.apache.poi.openxml4j.opc.OPCPackage.open(inputStream);
		SignatureConfig signConfig = new SignatureConfig();
		signConfig.setOpcPackage(opcPackage);
		signConfig.setKey(cert.getPrivateKey());
		signConfig.setSigningCertificateChain(Arrays.asList(cert.getCertificateChain()));
		signConfig.getOpcPackage().save(outputStream);
		
		byte[] signedBytes = outputStream.toByteArray();
		outputStream.close();
		ByteArrayInputStream bytesInputStream = new ByteArrayInputStream(signedBytes);
		int responseCode = uploadFileURL(bytesInputStream, filename, sUrl);
		// break MISSING_BLOCK_LABEL_169;
		// JVM INSTR pop ;
		// com.vnpt.b_pac.d.getName();
		// Level.SEVERE;
		// throw new PKCS11Exception(161L);		
		
		return responseCode;
	}

	public static void signDocxStreamInOut(String certSerial, String pin, InputStream inputStream, OutputStream outputStream) 
			throws UnrecoverableKeyException, KeyStoreException, NoSuchAlgorithmException, CertificateException, IOException, 
			PKCS11Exception, InvalidFormatException
	{
		long session;
		long slotId;
		
		aCertificate cert = getCertificate(certSerial);
		session = openSession(cert.getToken().getSlotId());
		slotId = cert.getToken().getSlotId();
		login(slotId, session, pin);
		logout(slotId, session);
		closeSession(slotId, session);
		cert.loadPrivateKey(pin);
		// break MISSING_BLOCK_LABEL_61;
		// s1;
		// a(l1, l);
		// throw s1;
		
//		com.vnpt.docx.opc.a a1 = com.vnpt.docx.opc.a.a(inputStream, b.c);
//		e e1 = new e(a1);
//		e1.a(cert.getPrivateKey(), cert.getCertificate(), null);
//		e1.a().a(outputStream);
		
		org.apache.poi.openxml4j.opc.OPCPackage opcPackage = org.apache.poi.openxml4j.opc.OPCPackage.open(inputStream);
		SignatureConfig signConfig = new SignatureConfig();
		signConfig.setOpcPackage(opcPackage);
		signConfig.setKey(cert.getPrivateKey());
		signConfig.setSigningCertificateChain(Arrays.asList(cert.getCertificateChain()));
		signConfig.getOpcPackage().save(outputStream);

		return;
	}

	public static int signPdfFileUrl(String certSerial, String pin, String filename, String sUrl, int llx, int lly, int urx, int ury, String outputFilename) 
			throws UnrecoverableKeyException, KeyStoreException, NoSuchAlgorithmException, CertificateException, 
			IOException, InvalidKeyException, SignatureException, PKCS11Exception, DocumentException
	{
		long session;
		long slotId;
		
		aCertificate cert = getCertificate(certSerial);
		session = openSession(cert.getToken().getSlotId());
		slotId = cert.getToken().getSlotId();
		login(slotId, session, pin);
		logout(slotId, session);
		closeSession(slotId, session);
		cert.loadPrivateKey(pin);
		// break MISSING_BLOCK_LABEL_63;
		// i;
		// a(l2, l1);
		// throw i;
		
		ByteArrayOutputStream outputStream = new ByteArrayOutputStream();
		
		PdfSignatureAppearance signatureAppearance = PdfStamper.createSignature(new PdfReader(filename), outputStream, '\0', null, true).getSignatureAppearance();
		signatureAppearance.setCertificationLevel(0);
		signatureAppearance.setCrypto(cert.getPrivateKey(), cert.getCertificateChain(), null, PdfSignatureAppearance.SELF_SIGNED);
		
		BaseFont baseFont  = BaseFont.createFont("com/vnpt/resource/times.ttf", "Identity-H", true, true, null, null);
		baseFont.setDirectTextToByte(false);	//f1.setForceWidthsOutput(false);
		Font font = new Font(baseFont, 12F);
		font.setColor(com.itextpdf.text.BaseColor.RED);
        signatureAppearance.setLayer2Font(font);
        signatureAppearance.setSignDate(Calendar.getInstance());
        signatureAppearance.setVisibleSignature(new Rectangle(llx, lly, urx, ury), 1, PdfPKCS7.getSubjectFields(cert.getCertificateChain()[0]).getField("CN"));
        signatureAppearance.setExternalDigest(new byte[128], null, "RSA");
        signatureAppearance.preClose();
        
        Signature signature = Signature.getInstance("SHA1withRSA");
        signature.initSign(cert.getPrivateKey());
        InputStream inputStream = signatureAppearance.getRangeStream();
        
        byte[] buffer = new byte[8192];
        int bytesRead = inputStream.read(buffer);
        while(bytesRead > 0)
        {
        	signature.update(buffer, 0, bytesRead);
        	bytesRead = inputStream.read(buffer);
        }
        byte[] signedBytes = signature.sign();
        
        PdfPKCS7 pkcs = signatureAppearance.getSigStandard().getSigner();
        pkcs.setExternalDigest(signedBytes, null, "RSA");
        PdfDictionary ap1 = new PdfDictionary();
        ap1.put(PdfName.CONTENTS, (new PdfString(pkcs.getEncodedPKCS1())).setHexWriting(true));
        signatureAppearance.setCryptoDictionary(ap1);
        
        byte[] outputBytes = outputStream.toByteArray();
        outputStream.close();
        ByteArrayInputStream bytesInputStream = new ByteArrayInputStream(outputBytes);
        int responseCode = uploadFileURL(((InputStream) bytesInputStream), outputFilename, sUrl);
		// break MISSING_BLOCK_LABEL_371;
		// JVM INSTR pop ;
		// com.vnpt.b_pac.d.getName();
		// Level.SEVERE;
		// throw new PKCS11Exception(161L);
        
		return responseCode;
	}

	
	public static int signPdfFileUrl(String certSerial, String pin, String filename, String sUrl, String outputFilename) 
			throws UnrecoverableKeyException, InvalidKeyException, KeyStoreException, NoSuchAlgorithmException, 
			CertificateException, SignatureException, IOException, PKCS11Exception, DocumentException
	{
		int responseCode = signPdfFileUrl(certSerial, pin, filename, sUrl, 0, 0, 166, 90, outputFilename);
		return responseCode;
	}

	public static int signPdfStreamUrl(String certSerial, String pin, InputStream inputstream, String sUrl, String filename, 
			int llx, int lly, int width, int height) 
			throws UnrecoverableKeyException, KeyStoreException, NoSuchAlgorithmException, CertificateException, IOException, 
			InvalidKeyException, SignatureException, PKCS11Exception, DocumentException
	{
		long session;
		long slotId;
		
		aCertificate cert = getCertificate(certSerial);
		session = openSession(cert.getToken().getSlotId());
		slotId = cert.getToken().getSlotId();
		login(slotId, session, pin);
		logout(slotId, session);
		closeSession(slotId, session);
		cert.loadPrivateKey(pin);
		// break MISSING_BLOCK_LABEL_63;
		// Exception exception;
		// exception;
		// a(l2, l1);
		// throw exception;
		
		ByteArrayOutputStream bytesOutputStream = new ByteArrayOutputStream();
		
		PdfSignatureAppearance signatureAppearance = PdfStamper.createSignature(new PdfReader(inputstream), bytesOutputStream, '\0', null, true).getSignatureAppearance();
		signatureAppearance.setCertificationLevel(0);
		signatureAppearance.setCrypto(cert.getPrivateKey(), cert.getCertificateChain(), null, PdfSignatureAppearance.SELF_SIGNED);
		new SimpleDateFormat("dd/MM/yyyy HH:mm:ss");//
		BaseFont baseFont = BaseFont.createFont("com/vnpt/resource/times.ttf", "Identity-H", true, true, null, null);
		baseFont.setDirectTextToByte(false);	//f1.setForceWidthsOutput(false);
		Font font = new Font(baseFont, 12F);
		font.setColor(com.itextpdf.text.BaseColor.RED);
		signatureAppearance.setLayer2Font(font);
		signatureAppearance.setSignDate(Calendar.getInstance());
		signatureAppearance.setVisibleSignature(new Rectangle(llx, lly, llx + width, lly + height), 1, null);
		signatureAppearance.setExternalDigest(new byte[128], null, "RSA");
		signatureAppearance.preClose();
		
		Signature signature = Signature.getInstance("SHA1withRSA");
		signature.initSign(cert.getPrivateKey());
		InputStream inputStream = signatureAppearance.getRangeStream();
		
		byte[] buffer = new byte[8192];
		int bytesRead = inputStream.read(buffer);
		while (bytesRead > 0)
		{
			signature.update(buffer, 0, bytesRead);
			bytesRead = inputStream.read(buffer);
		}
		byte[] signatureBytes = signature.sign();
		
		PdfPKCS7 pkcs = signatureAppearance.getSigStandard().getSigner();
		pkcs.setExternalDigest(signatureBytes, null, "RSA");
		PdfDictionary ap1  = new PdfDictionary();
		ap1.put(PdfName.CONTENTS, (new PdfString(pkcs.getEncodedPKCS1())).setHexWriting(true));
		signatureAppearance.setCryptoDictionary(ap1);
		
		byte[] outputBytes = bytesOutputStream.toByteArray();
		bytesOutputStream.close();
		ByteArrayInputStream bytesInputStream = new ByteArrayInputStream(outputBytes);
		int responseCode = uploadFileURL(((InputStream) bytesInputStream), filename, sUrl);
		// break MISSING_BLOCK_LABEL_378;
		// JVM INSTR pop ;
		// com.vnpt.b_pac.d.getName();
		// Level.SEVERE;
		// throw new PKCS11Exception(161L);
		
		return responseCode;
	}

	public static InputStream downloadFileURL(String sUrl) 
			throws IOException
	{
		ByteArrayInputStream bytesInputStream = null;
		ByteArrayOutputStream bytesOutputStream = new ByteArrayOutputStream();
		
		//Đọc web
		URL url = new URL(sUrl);
		URLConnection urlConn = url.openConnection();
		InputStream inputStream = urlConn.getInputStream();
		
		//Lấy dữ liệu web và đẩy vào OutputStream
		byte buffer[] = new byte[8192];
		int bytesRead = inputStream.read(buffer);
		while (bytesRead != -1)
		{
			bytesOutputStream.write(buffer, 0, bytesRead);
			bytesRead = inputStream.read(buffer);
		}
		inputStream.close();
		bytesOutputStream.flush();
		
		//Chuyển dữ liệu web từ OutputStream sang InputStream để xử lý
		byte outputBytes[] = bytesOutputStream.toByteArray();
		bytesInputStream = new ByteArrayInputStream(outputBytes);
		bytesOutputStream.close();
		
		return bytesInputStream;
	}

	public static List<bTokenDriver> TokenDrivers = new ArrayList<>(); // bTokenDriver
	public static ArrayList<String> DllPath = new com.vnpt.b_pac.eDLLPath();

	/* Private member */
	private static void loadCertificate(com.vnpt.b_pac.cToken token) throws PKCS11Exception
	{
		PKCS11 pkcs11;
		pkcs11 = token.getTokenDriver().getPKCS11();

		CK_ATTRIBUTE[] attributes = new CK_ATTRIBUTE[] { new CK_ATTRIBUTE(0L, 1L) };
		long sessionId = pkcs11.C_OpenSession(token.getSlotId(), 4L, null, null);

		try
		{
			pkcs11.C_FindObjectsInit(sessionId, attributes);
			long[] lstObj = pkcs11.C_FindObjects(sessionId, _MaxObjectCount);
			for (int j = 0; j < lstObj.length; j++)
			{
				long obj = lstObj[j];
				CK_ATTRIBUTE ack_attribute[] = {
						new CK_ATTRIBUTE(17L), new CK_ATTRIBUTE(258L), new CK_ATTRIBUTE(3L)
				};
				pkcs11.C_GetAttributeValue(sessionId, obj, ack_attribute);
				try
				{
					com.vnpt.b_pac.aCertificate cert = new com.vnpt.b_pac.aCertificate();
					CertificateFactory certFactory = CertificateFactory.getInstance("X.509");
					X509Certificate x509certificate = (X509Certificate) certFactory.generateCertificate(new ByteArrayInputStream(ack_attribute[0].getByteArray()));

					cert.setToken(token);
					cert.setBase64(new String(com.vnpt.common.aBase64.convertBytesToBase64(x509certificate.getEncoded())));
					cert.setSubject(com.vnpt.common.cDungChung.toString(x509certificate.getSubjectDN()));
					cert.setIssuer(com.vnpt.common.cDungChung.toString(x509certificate.getIssuerDN()));
					SimpleDateFormat dateFormat = new SimpleDateFormat("dd/MM/yyyy HH:mm:ss");
					cert.setValidTo(dateFormat.format(x509certificate.getNotAfter()));
					cert.setValidFrom(dateFormat.format(x509certificate.getNotBefore()));
					cert.setSerialNumber(com.vnpt.common.cDungChung.convertBytesToHex(x509certificate.getSerialNumber().toByteArray()));
					cert.setFriendlyName(getNameCN(x509certificate.getSubjectDN().getName()));
					cert.setPublicKey(x509certificate.getPublicKey());
					cert.setCertificate(x509certificate);

					if (x509certificate.getNotAfter().compareTo(new Date()) > 0)
						token.getListCertificate().add(cert);
				}
				catch (CertificateException ex)
				{
					 System.out.print(ex);
				}
			}

			pkcs11.C_FindObjectsFinal(sessionId);
			pkcs11.C_CloseSession(sessionId);
			return;
		}
		catch (Exception ex)
		{
			pkcs11.C_FindObjectsFinal(sessionId);
			pkcs11.C_CloseSession(sessionId);
			throw ex;
		}
	}

	private static String getNameCN(String s)
	{
		String s1 = "";
		String[] lst = s.split(",");
		int i = 0;
		do
		{
			if (i >= lst.length)
				break;
			if (lst[i].indexOf("CN") >= 0)
			{
				s1 = lst[i].split("=")[1];
				break;
			}
			i++;
		}
		while (true);
		return s1;
	}

	private static void loadToken(com.vnpt.b_pac.bTokenDriver tokenDriver) throws PKCS11Exception, KeyStoreException
	{
		long lstSlot[] = tokenDriver.getPKCS11().C_GetSlotList(true);
		if (lstSlot == null)
			return;

		for (int i = 0; i < lstSlot.length; i++)
		{
			long slot = lstSlot[i];
			com.vnpt.b_pac.cToken token = new com.vnpt.b_pac.cToken();

			String dllPath = tokenDriver.getDllPath();
			String command = (new StringBuilder("name = SmartCard\nlibrary=")).append(dllPath).append("\nslot=").append(slot).toString();
			byte byteCommand[] = command.getBytes();
			ByteArrayInputStream stream = new ByteArrayInputStream(byteCommand);
			SunPKCS11 sunPKCS11 = new SunPKCS11(stream);
			Security.addProvider(sunPKCS11);
			KeyStore keyStore = KeyStore.getInstance("PKCS11");
			CK_TOKEN_INFO ck_token_info = tokenDriver.getPKCS11().C_GetTokenInfo(slot);
			// char[] _tmp = ck_token_info.model;

			token.setManufactureId((new String(ck_token_info.manufacturerID)).trim());
			token.setTokenSerial((new String(ck_token_info.serialNumber)).trim());
			token.setTokenDriver(tokenDriver);
			token.setSlotId(slot);
			token.setKetStore(keyStore);

			tokenDriver.getListToken().add(token);

			loadCertificate(token);
		}
	}

	private static PKCS11 loadPKCS11(String dllPath) 
			throws IllegalAccessException, IllegalArgumentException, InvocationTargetException, IOException
	{
		Method lstMethods[] = sun.security.pkcs11.wrapper.PKCS11.class.getMethods();
		Method method = null;
		PKCS11 pkcs11 = null;
		CK_C_INITIALIZE_ARGS ck_c_initialize_args = new CK_C_INITIALIZE_ARGS();
		ck_c_initialize_args.pReserved = null;
		ck_c_initialize_args.flags = 0L;
		for (int i = 0; i < lstMethods.length; i++)
			if (lstMethods[i].getName().equals("getInstance"))
				method = lstMethods[i];

		dllPath = (new File(dllPath)).getCanonicalPath();
		String javaVersion = System.getProperty("java.version");
		if (javaVersion.indexOf("1.6") >= 0 || javaVersion.indexOf("1.7") >= 0 || javaVersion.indexOf("1.8") >= 0)
			pkcs11 = (PKCS11) method.invoke(null, new Object[] {
					dllPath, "C_GetFunctionList", ck_c_initialize_args, Boolean.valueOf(false)
			});
		else if (javaVersion.indexOf("1.5") >= 0)
			pkcs11 = (PKCS11) method.invoke(null, new Object[] {
					dllPath, ck_c_initialize_args, Boolean.valueOf(false)
			});
		return pkcs11;
	}

	private static com.vnpt.b_pac.cToken getTokenFromSlotId(long slotId)
	{
		for (int i = 0; i < TokenDrivers.size(); i++)
		{
			com.vnpt.b_pac.bTokenDriver tokenDriver = (com.vnpt.b_pac.bTokenDriver) TokenDrivers.get(i);
			for (int j = 0; j < tokenDriver.getListToken().size(); j++)
			{
				com.vnpt.b_pac.cToken token = (com.vnpt.b_pac.cToken) tokenDriver.getListToken().get(j);
				if (token.getSlotId() == slotId)
					return token;
			}
			
		}

		return null;
	}

	private static long openSession(long slotId) throws PKCS11Exception
	{
		com.vnpt.b_pac.cToken token = getTokenFromSlotId(slotId);
		long session = token.getTokenDriver().getPKCS11().C_OpenSession(slotId, 6L, null, null);
		return session;
	}

	private static void closeSession(long slotId, long session) throws PKCS11Exception
	{
		getTokenFromSlotId(slotId).getTokenDriver().getPKCS11().C_CloseSession(session);
	}

	private static com.vnpt.b_pac.aCertificate getCertificate(String serialNumber)
	{
		for (int i = 0; i < TokenDrivers.size(); i++)
		{
			com.vnpt.b_pac.bTokenDriver tokenDriver = (com.vnpt.b_pac.bTokenDriver) TokenDrivers.get(i);
			for (int j = 0; j < tokenDriver.getListToken().size(); j++)
			{
				com.vnpt.b_pac.cToken token = (com.vnpt.b_pac.cToken) tokenDriver.getListToken().get(j);
				for (int k = 0; k < token.getListCertificate().size(); k++)
				{
					com.vnpt.b_pac.aCertificate cert = (com.vnpt.b_pac.aCertificate) token.getListCertificate().get(k);
					if (serialNumber.endsWith(cert.getSerialNumber()))
						return cert;
				}
			}
		}

		return null;
	}

	private static void login(long slotId, long session, String pin) throws PKCS11Exception
	{
		cToken token = getTokenFromSlotId(slotId);
		token.getTokenDriver().getPKCS11().C_Login(session, 1L, pin.toCharArray());
		return;
		// JVM INSTR dup ;
		// l;
		// getErrorCode();
		// 256L;
		// JVM INSTR lcmp ;
		// JVM INSTR ifeq 37;
		// goto _L1 _L2
		// _L1:
		// break MISSING_BLOCK_LABEL_35;
		// _L2:
		// break MISSING_BLOCK_LABEL_37;
		// throw l;
	}

	private static void logout(long slotId, long session) throws PKCS11Exception
	{
		cToken token = getTokenFromSlotId(slotId);
		token.getTokenDriver().getPKCS11().C_Logout(session);
	}

	private static String readStreamToString(InputStream inputStream)
	{
		BufferedReader bufferedReader;
		StringBuilder stringBuilder;
		bufferedReader = null;
		stringBuilder = new StringBuilder();
		try
		{
			bufferedReader = new BufferedReader(new InputStreamReader(inputStream));
			while (bufferedReader.readLine() != null)
				stringBuilder.append(inputStream);
		}
		catch (IOException _ex)
		{
			if (bufferedReader != null)
				try
				{
					bufferedReader.close();
				}
				catch (IOException _ex2)
				{
				}
			// break MISSING_BLOCK_LABEL_86;
		}
		try
		{
			bufferedReader.close();
		}
		catch (IOException _ex)
		{
		}
		// break MISSING_BLOCK_LABEL_86;
		// inputstream;
		if (bufferedReader != null)
			try
			{
				bufferedReader.close();
			}
			catch (IOException _ex)
			{
			}
		// throw inputstream;
		return stringBuilder.toString();
	}

	private static int uploadFileURL(InputStream inputstream, String filename, String sUrl) throws IOException
	{
		URL url = new URL(sUrl);
		HttpURLConnection httpConn = (HttpURLConnection) url.openConnection();
		httpConn.setDoOutput(true);
		httpConn.setDoInput(true);
		httpConn.setUseCaches(true);
		httpConn.setRequestMethod("POST");
		httpConn.setRequestProperty("Content-Type", "binary/octet-stream");
		httpConn.setRequestProperty("Content-Disposition", (new StringBuilder("form-data; filename=\"")).append(filename).append("\"").toString());
		httpConn.connect();
		
		DataOutputStream outputStream = new DataOutputStream(httpConn.getOutputStream());
		int bytesAvailable = inputstream.available();
		while (bytesAvailable > 0)
		{
			byte buffer[] = bytesAvailable  < 8192 ? new byte[bytesAvailable] : new byte[8192];
			inputstream.read(buffer);
			outputStream.write(buffer);
			outputStream.flush();
			bytesAvailable = inputstream.available();
		}
		inputstream.close();
		outputStream.close();
		// break MISSING_BLOCK_LABEL_153;
		// inputstream;
		// s.close();
		// throw inputstream;
		int responseCode = httpConn.getResponseCode();
		httpConn.disconnect();
		return responseCode;
	}

	private static int _MaxObjectCount = 1000; // MaxObjectCount
	
}

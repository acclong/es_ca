package com.vnpt.b_pac;

import java.io.StringWriter;
import java.io.Writer;
import java.security.KeyStore;
import java.util.ArrayList;
import java.util.List;
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

public final class cToken
{
	private List<Object> _lstCertificate = new ArrayList<Object>();
	private String _manufactureId;
	private String _tokenSerial;
	private bTokenDriver _tokenDriver;
	private long _slotId;
	private KeyStore _keyStore;

	public final KeyStore getKetStore()
	{
		return this._keyStore;
	}

	public final void setKetStore(KeyStore keyStore)
	{
		this._keyStore = keyStore;
	}

	public final long getSlotId()
	{
		return this._slotId;
	}

	public final void setSlotId(long slotId)
	{
		this._slotId = slotId;
	}

	public final bTokenDriver getTokenDriver()
	{
		return this._tokenDriver;
	}

	public final void setTokenDriver(bTokenDriver driver)
	{
		this._tokenDriver = driver;
	}

	public final List<Object> getListCertificate()
	{
		return this._lstCertificate;
	}

	public final String getManufactureId()
	{
		return this._manufactureId;
	}

	public final void setManufactureId(String manufactureId)
	{
		this._manufactureId = manufactureId;
	}

	public final String getTokenSerial()
	{
		return this._tokenSerial;
	}

	public final void setTokenSerial(String tokenSerial)
	{
		this._tokenSerial = tokenSerial;
	}

	public final String getSlotXml()
	{
		try
		{
			Document doc = DocumentBuilderFactory.newInstance().newDocumentBuilder().newDocument();
			Element slot = doc.createElement("slot");
			doc.appendChild((Node) slot);
			//Object localObject4 = this;
			Element slotInfo;
			(slotInfo = doc.createElement("dllPath")).appendChild(doc.createTextNode(this._tokenDriver.getDllPath()));
			slot.appendChild((Node) slotInfo);
			//localObject4 = this;
			(slotInfo = doc.createElement("slotId")).appendChild(doc.createTextNode(String.valueOf(this._slotId)));
			slot.appendChild((Node) slotInfo);
			//localObject4 = this;
			(slotInfo = doc.createElement("serialNumber")).appendChild(doc.createTextNode(com.vnpt.common.cDungChung.trim(this._tokenSerial)));
			slot.appendChild((Node) slotInfo);
			//localObject4 = this;
			(slotInfo = doc.createElement("manufactureId")).appendChild(doc.createTextNode(com.vnpt.common.cDungChung.trim(this._manufactureId)));
			slot.appendChild((Node) slotInfo);
			
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
}

/*
 * Location: D:\Code\Visual
 * Studio\CA-VCGM\Doc\VDC\AppletSign-Signbase64-08062015
 * \Sent\VnptTokenApplet.jar!\com\vnpt\b\c.class
 * Java compiler version: 7 (51.0)
 * JD-Core Version: 0.7.1
 */
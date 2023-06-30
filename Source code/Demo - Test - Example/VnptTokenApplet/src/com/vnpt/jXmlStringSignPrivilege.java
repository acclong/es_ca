package com.vnpt;

import java.security.PrivilegedExceptionAction;

final class jXmlStringSignPrivilege implements PrivilegedExceptionAction<Object>
{
	VnptTokenApplet _applet;
	String _certSerial;
	String _xmlToSign;
	
	jXmlStringSignPrivilege(VnptTokenApplet applet, String certSerial, String xmlToSign)
	{
		_applet = applet;
		_certSerial = certSerial;
		_xmlToSign = xmlToSign;
	}

	public final Object run() throws Exception
	{
		return new kXmlStringSignManager(this).sign(this._certSerial, this._xmlToSign);
	}
}

/*
 * Location: D:\Code\Visual
 * Studio\CA-VCGM\Doc\VDC\AppletSign-Signbase64-08062015\Sent\VnptTokenApplet.
 * jar!\com\vnpt\j.class
 * Java compiler version: 7 (51.0)
 * JD-Core Version: 0.7.1
 */
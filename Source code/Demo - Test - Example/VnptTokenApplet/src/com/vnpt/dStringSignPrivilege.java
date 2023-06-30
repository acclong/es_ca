package com.vnpt;

import java.security.PrivilegedExceptionAction;

final class dStringSignPrivilege implements PrivilegedExceptionAction<Object>
{
	VnptTokenApplet _applet;
	String _certSerial;
	String _stringToSign;
	
	dStringSignPrivilege(VnptTokenApplet applet, String certSerial, String stringToSign)
	{
		_applet = applet;
		_certSerial = certSerial;
		_stringToSign = stringToSign;
	}

	public final Object run() throws Exception
	{
		return new eStringSignManager(this).sign(this._certSerial, this._stringToSign);
	}
}

/*
 * Location: D:\Code\Visual
 * Studio\CA-VCGM\Doc\VDC\AppletSign-Signbase64-08062015\Sent\VnptTokenApplet.
 * jar!\com\vnpt\d.class
 * Java compiler version: 7 (51.0)
 * JD-Core Version: 0.7.1
 */
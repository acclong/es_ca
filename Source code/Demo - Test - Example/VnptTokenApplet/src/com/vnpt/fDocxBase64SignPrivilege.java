package com.vnpt;

import java.security.PrivilegedExceptionAction;

final class fDocxBase64SignPrivilege implements PrivilegedExceptionAction<Object>
{
	VnptTokenApplet _applet;
	String _certSerial;
	String _inputBase64;
	
	fDocxBase64SignPrivilege(VnptTokenApplet applet, String certSerial, String inputBase64)
	{
		_applet = applet;
		_certSerial = certSerial;
		_inputBase64 = inputBase64;
	}

	public final Object run() throws Exception
	{
		return new gDocxBase64SignManager(this).sign(this._certSerial, this._inputBase64);
	}
}

/*
 * Location: D:\Code\Visual
 * Studio\CA-VCGM\Doc\VDC\AppletSign-Signbase64-08062015\Sent\VnptTokenApplet.
 * jar!\com\vnpt\f.class
 * Java compiler version: 7 (51.0)
 * JD-Core Version: 0.7.1
 */
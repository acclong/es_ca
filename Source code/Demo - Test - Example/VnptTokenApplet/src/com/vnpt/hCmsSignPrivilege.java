package com.vnpt;

import java.security.PrivilegedExceptionAction;

final class hCmsSignPrivilege implements PrivilegedExceptionAction<Object>
{
	VnptTokenApplet _applet;
	String _certSerial;
	String _cmsString;
	
	hCmsSignPrivilege(VnptTokenApplet applet, String certSerial, String cmsString)
	{
		_applet = applet;
		_certSerial = certSerial;
		_cmsString = cmsString;
	}

	public final Object run() throws Exception
	{
		return new iCmsSignManager(this).sign(this._certSerial, this._cmsString);
	}
}

/*
 * Location: D:\Code\Visual
 * Studio\CA-VCGM\Doc\VDC\AppletSign-Signbase64-08062015\Sent\VnptTokenApplet.
 * jar!\com\vnpt\h.class
 * Java compiler version: 7 (51.0)
 * JD-Core Version: 0.7.1
 */
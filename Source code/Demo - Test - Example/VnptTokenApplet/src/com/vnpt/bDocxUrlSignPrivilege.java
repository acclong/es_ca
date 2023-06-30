package com.vnpt;

import java.security.PrivilegedExceptionAction;

final class bDocxUrlSignPrivilege implements PrivilegedExceptionAction<Object>
{
	VnptTokenApplet _applet;
	String _temp;
	String _certSerial;
	String _inputUrl;
	String _outputUrl;
	
	bDocxUrlSignPrivilege(VnptTokenApplet applet, String temp, String certSerial, String inputUrl, String outputUrl)
	{
		_applet = applet;
		_temp = temp;
		_certSerial = certSerial;
		_inputUrl = inputUrl;
		_outputUrl = outputUrl;
	}

	public final Object run() throws Exception
	{
		new cDocxUrlSignManager(this).sign(this._certSerial, this._inputUrl, this._outputUrl);
		//Toantk 24/7/2015: khoong chắc hàm applet.getAppletInfo()
		return Integer.valueOf(this._applet.getAppletInfo());
	}
}

/*
 * Location: D:\Code\Visual
 * Studio\CA-VCGM\Doc\VDC\AppletSign-Signbase64-08062015\Sent\VnptTokenApplet.
 * jar!\com\vnpt\b.class
 * Java compiler version: 7 (51.0)
 * JD-Core Version: 0.7.1
 */
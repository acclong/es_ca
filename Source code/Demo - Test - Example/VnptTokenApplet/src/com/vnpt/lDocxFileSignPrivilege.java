package com.vnpt;

import java.security.PrivilegedExceptionAction;

final class lDocxFileSignPrivilege implements PrivilegedExceptionAction<Object>
{
	VnptTokenApplet _applet;
	String _certSerial;
	String _inputPath;
	String _outputPath;
	
	lDocxFileSignPrivilege(VnptTokenApplet applet, String certSerial, String inputPath, String outputPath)
	{
		_applet = applet;
		_certSerial = certSerial;
		_inputPath = inputPath;
		_outputPath =outputPath;
	}

	public final Object run() throws Exception
	{
		new mDocxFileSignManager(this).sign(this._certSerial, this._inputPath, this._outputPath);
		return null;
	}
}

/*
 * Location: D:\Code\Visual
 * Studio\CA-VCGM\Doc\VDC\AppletSign-Signbase64-08062015\Sent\VnptTokenApplet.
 * jar!\com\vnpt\l.class
 * Java compiler version: 7 (51.0)
 * JD-Core Version: 0.7.1
 */
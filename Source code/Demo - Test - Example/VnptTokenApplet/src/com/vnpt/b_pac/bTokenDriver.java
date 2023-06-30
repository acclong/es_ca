package com.vnpt.b_pac;

import java.util.ArrayList;
import java.util.List;
import sun.security.pkcs11.wrapper.PKCS11;

public final class bTokenDriver
{
	private List<Object> _lstToken = new ArrayList<Object>();
	private String _dllPath;
	private PKCS11 _pkcs11;

	public final PKCS11 getPKCS11()
	{
		return this._pkcs11;
	}

	public final void setPKCS11(PKCS11 pkcs11)
	{
		this._pkcs11 = pkcs11;
	}

	public final String getDllPath()
	{
		return this._dllPath;
	}

	public final void setDllPath(String path)
	{
		this._dllPath = path;
	}

	public final List<Object> getListToken()
	{
		return this._lstToken;
	}
}

/*
 * Location: D:\Code\Visual
 * Studio\CA-VCGM\Doc\VDC\AppletSign-Signbase64-08062015
 * \Sent\VnptTokenApplet.jar!\com\vnpt\b\b.class
 * Java compiler version: 7 (51.0)
 * JD-Core Version: 0.7.1
 */
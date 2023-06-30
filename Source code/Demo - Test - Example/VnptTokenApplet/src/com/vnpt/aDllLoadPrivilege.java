package com.vnpt;

import com.vnpt.a_pac.cLoadCertificateDialog;
import com.vnpt.a_pac.jLoadTokenDialog;
import com.vnpt.b_pac.dSigner;

import sun.security.pkcs11.wrapper.PKCS11Exception;

import java.io.IOException;
import java.lang.reflect.InvocationTargetException;
import java.security.KeyStoreException;
import java.security.PrivilegedExceptionAction;
import java.util.ArrayList;

final class aDllLoadPrivilege implements PrivilegedExceptionAction<Object>
{
	VnptTokenApplet _app;
	
	aDllLoadPrivilege(VnptTokenApplet app)
	{
		this._app = app;
	}

	public final Object run() throws IllegalAccessException, IllegalArgumentException, InvocationTargetException, KeyStoreException, 
		IOException, PKCS11Exception
	{
		ArrayList<String> lstDLL = new ArrayList<>();
		
		//Đọc tham số danh sách dll từ html
		String dllList = this._app.getParameter("dll");
		String system32Folder = System.getenv("SystemRoot") + "\\system32\\";
		String[] lstDllFile = dllList.split(",");
		for (int j = 0; j < lstDllFile.length; j++)
		{
			String dll = lstDllFile[j];
			dll = system32Folder + dll;
			lstDLL.add(dll);
		}
		
		VnptTokenApplet.class.getName();
		new StringBuilder("danh sach dll:").append(dllList);
		
		//load DLL và token tương ứng
		if (lstDllFile.length > 0)
		{
			dSigner.loadDLL(lstDLL);
		}
		else
		{
			dSigner.loadDLL(dSigner.DllPath);
		}
		
		//VnptTokenApplet.a(this._app, new cLoadCertificateDialog(null, true));
		//VnptTokenApplet.a(this._app, new jLoadTokenDialog(null, true));
		_app.add(new cLoadCertificateDialog(null, true));
		_app.add(new jLoadTokenDialog(null, true));
		return null;
	}
}

/*
 * Location: D:\Code\Visual
 * Studio\CA-VCGM\Doc\VDC\AppletSign-Signbase64-08062015\Sent\VnptTokenApplet.
 * jar!\com\vnpt\a.class
 * Java compiler version: 7 (51.0)
 * JD-Core Version: 0.7.1
 */
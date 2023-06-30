package com.vnpt;

import com.vnpt.common.bFileFilter;

import java.io.File;
import java.security.PrivilegedExceptionAction;
import javax.swing.JFileChooser;

final class nPdfFileSignPrivilege implements PrivilegedExceptionAction<Object>
{
	VnptTokenApplet _applet;
	String _certSerial;
	String _outputUrl;
	
	nPdfFileSignPrivilege(VnptTokenApplet applet, String certSerial, String outputUrl)
	{
		_applet = applet;
		_certSerial = certSerial;
		_outputUrl = outputUrl;
	}

	public final Object run() throws Exception
	{
		JFileChooser fileChooser = new JFileChooser();
		String[] arrayOfString = { "pdf" };
		fileChooser.setFileSelectionMode(0);
		fileChooser.setFileFilter(new bFileFilter("Pdf file", arrayOfString));
		if (fileChooser.showSaveDialog(this._applet) == 0)
		{
			File file = fileChooser.getSelectedFile();
			//Toantk 23/7/2015: kiểm tra hàm _applet.setFilePath() có thể là hàm khác
			this._applet.setFilepathselected(file.getAbsolutePath());
		}
		
		//Toantk 23/7/2015: kiểm tra hàm _applet.getFilePath() có thể là hàm khác
		new oPdfFileSignManager(this).sign(this._certSerial, this._applet.getFilepathselected(), this._outputUrl);
		return null;
	}
}

/*
 * Location: D:\Code\Visual
 * Studio\CA-VCGM\Doc\VDC\AppletSign-Signbase64-08062015\Sent\VnptTokenApplet.
 * jar!\com\vnpt\n.class
 * Java compiler version: 7 (51.0)
 * JD-Core Version: 0.7.1
 */
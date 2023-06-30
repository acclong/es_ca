package com.vnpt.common;

import javax.swing.ImageIcon;

public class cDungChung
{
	public static ImageIcon icon = new ImageIcon(cDungChung.class.getResource("/com/vnpt/resource/vnpt.png"));
	private static final char[] _arrHexChar = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };

	public static String toString(Object obj)
	{
		if (obj == null)
		{
			return "";
		}
		return obj.toString();
	}

	public static String convertBytesToHex(byte[] bytes)
	{
		return new String(convertBytesToHex(bytes, _arrHexChar)).trim();
	}

	public static String trim(String s)
	{
		if (s == null)
		{
			return null;
		}
		return s.trim();
	}

	private static char[] convertBytesToHex(byte[] bytes, char[] arrHexChar)
	{
		int i = bytes.length;
		char[] arrHex = new char[bytes.length * 3];
		int j = 0;
		int k = 0;
		while (j < i)
		{
			arrHex[(k++)] = arrHexChar[((0xF0 & bytes[j]) >>> 4)];
			arrHex[(k++)] = arrHexChar[(0xF & bytes[j])];
			arrHex[(k++)] = ' ';
			j++;
		}
		return arrHex;
	}

	public static boolean isNullOrEmpty(String s)
	{
		return (s == null) || (s.length() == 0);
	}
}

/*
 * Location: D:\Code\Visual
 * Studio\CA-VCGM\Doc\VDC\AppletSign-Signbase64-08062015
 * \Sent\VnptTokenApplet.jar!\com\vnpt\c\c.class
 * Java compiler version: 7 (51.0)
 * JD-Core Version: 0.7.1
 */
package com.vnpt.common;

public final class aBase64
{
	private static final char[] _baseChar = new char[64];
	private static final byte[] _baseByte;

	public static char[] convertBytesToBase64(byte[] bytes)
	{
		int i = bytes.length;
		// bytes = 0;
		// bytes = bytes;
		int j = ((i << 2) + 2) / 3;
		//int k;
		char[] arrBase64 = new char[(i + 2) / 3 << 2];	//char[] arrBase64 = new char[k = (i + 2) / 3 << 2];
		int m = 0;
		i += 0;
		for (int n = 0; m < i; n++)
		{
			int i1 = bytes[(m++)] & 0xFF;
			int i2 = m < i ? bytes[(m++)] & 0xFF : 0;
			int i3 = m < i ? bytes[(m++)] & 0xFF : 0;
			int i4 = i1 >>> 2;
			i1 = (i1 & 0x3) << 4 | i2 >>> 4;
			i2 = (i2 & 0xF) << 2 | i3 >>> 6;
			i3 &= 0x3F;
			arrBase64[(n++)] = _baseChar[i4];
			arrBase64[(n++)] = _baseChar[i1];
			arrBase64[n] = (n < j ? _baseChar[i2] : '=');
			n++;
			arrBase64[n] = (n < j ? _baseChar[i3] : '=');
		}
		return arrBase64;
	}

	public static byte[] convertBase64ToBytes(String strBase64)
	{
		int i = strBase64.length();
		// strBase64 = 0;
		char[] arrBase64 = strBase64.toCharArray();
		if (i % 4 != 0)
		{
			throw new IllegalArgumentException("Length of Base64 encoded input string is not a multiple of 4.");
		}
		while ((i > 0) && (arrBase64[(i + 0 - 1)] == '='))
		{
			i--;
		}
		int j;
		byte[] arrayOfByte = new byte[j = i * 3 / 4];
		int k = 0;
		i += 0;
		int m = 0;
		while (k < i)
		{
			int n = arrBase64[(k++)];
			int i1 = arrBase64[(k++)];
			int i2 = k < i ? arrBase64[(k++)] : 65;
			int i3 = k < i ? arrBase64[(k++)] : 65;
			if ((n > 127) || (i1 > 127) || (i2 > 127) || (i3 > 127))
			{
				throw new IllegalArgumentException("Illegal character in Base64 encoded data.");
			}
			n = _baseByte[n];
			i1 = _baseByte[i1];
			i2 = _baseByte[i2];
			i3 = _baseByte[i3];
			if ((n < 0) || (i1 < 0) || (i2 < 0) || (i3 < 0))
			{
				throw new IllegalArgumentException("Illegal character in Base64 encoded data.");
			}
			n = n << 2 | i1 >>> 4;
			i1 = (i1 & 0xF) << 4 | i2 >>> 2;
			i2 = (i2 & 0x3) << 6 | i3;
			arrayOfByte[(m++)] = ((byte) n);
			if (m < j)
			{
				arrayOfByte[(m++)] = ((byte) i1);
			}
			if (m < j)
			{
				arrayOfByte[(m++)] = ((byte) i2);
			}
		}
		return arrayOfByte;
	}

	static
	{
		int i = 0;
		for (int j = 65; j <= 90; j = j + 1)
		{
			_baseChar[(i++)] = (char) j;
		}
		for (int j = 97; j <= 122; j = j + 1)
		{
			_baseChar[(i++)] = (char) j;
		}
		for (int j = 48; j <= 57; j = j + 1)
		{
			_baseChar[(i++)] = (char) j;
		}
		_baseChar[(i++)] = '+';
		_baseChar[i] = '/';
		
		_baseByte = new byte[''];
		for (i = 0; i < _baseByte.length; i++)
		{
			_baseByte[i] = -1;
		}
		for (i = 0; i < 64; i++)
		{
			_baseByte[_baseChar[i]] = ((byte) i);
		}
	}
}

/*
 * Location: D:\Code\Visual
 * Studio\CA-VCGM\Doc\VDC\AppletSign-Signbase64-08062015
 * \Sent\VnptTokenApplet.jar!\com\vnpt\c\a.class
 * Java compiler version: 7 (51.0)
 * JD-Core Version: 0.7.1
 */
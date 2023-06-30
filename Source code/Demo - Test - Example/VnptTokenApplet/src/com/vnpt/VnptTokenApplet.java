package com.vnpt;

import java.applet.Applet;
import java.io.FileNotFoundException;
import java.security.AccessController;
import java.security.PrivilegedActionException;
import java.util.Iterator;
import java.util.List;
import javax.swing.JFileChooser;
import javax.swing.JOptionPane;
import javax.swing.UIManager;
import org.xml.sax.SAXException;

import com.vnpt.b_pac.aCertificate;

import sun.security.pkcs11.wrapper.PKCS11Exception;

public class VnptTokenApplet extends Applet
{
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private static long _exceptionCode = -1L;
	private com.vnpt.a_pac.jLoadTokenDialog _dlgLoadToken = null;
	private com.vnpt.a_pac.cLoadCertificateDialog _dlgLoadCertificate = null;
	private String _filePath;
	private String _filename;
	private String _fileExtension;
	private String _filepathselected;
	private String _listFileSigned = new String();
	private int _responseCode = 0;
	private JFileChooser _fileChooser;

	public VnptTokenApplet()
	{
		try
		{
			UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
			this._fileChooser = new JFileChooser();
			return;
		}
		catch (Exception ex)
		{
			VnptTokenApplet.class.getName();
		}
	}

	public JFileChooser getChooser()
	{
		return this._fileChooser;
	}

	public void setChooser(JFileChooser fileChooser)
	{
		try
		{
			UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
		}
		catch (Exception localException)
		{
			VnptTokenApplet.class.getName();
		}
		this._fileChooser = fileChooser;
	}

	public int getResponse()
	{
		return this._responseCode;
	}

	public void setResponse(int responseCode)
	{
		this._responseCode = responseCode;
	}

	public String getListFileSigned()
	{
		return this._listFileSigned;
	}

	public void setListFileSigned(String listFileSigned)
	{
		this._listFileSigned = listFileSigned;
	}

	public String getFilepathselected()
	{
		return this._filepathselected;
	}

	public void setFilepathselected(String filepathselected)
	{
		this._filepathselected = filepathselected;
	}

	public String getFilename()
	{
		return this._filename;
	}

	public void setFilename(String filename)
	{
		this._filename = filename;
	}

	public String getFileExtension()
	{
		return this._fileExtension;
	}

	public void setFileExtension(String fileExtension)
	{
		this._fileExtension = fileExtension;
	}

	public String getFilePath()
	{
		return this._filePath;
	}

	public void setFilePath(String filePath)
	{
		this._filePath = filePath;
	}

	public static long getExceptionCode()
	{
		return _exceptionCode;
	}

	public static void setExceptionCode(long exceptionCode)
	{
		_exceptionCode = exceptionCode;
	}

	public void init()
	{
		try
		{
			super.init();
			try
			{
				UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
			}
			catch (Exception ex)
			{
				VnptTokenApplet.class.getName();
			}
			AccessController.doPrivileged(new aDllLoadPrivilege(this));

			return;
		}
		catch (PrivilegedActionException localPrivilegedActionException)
		{
			VnptTokenApplet.class.getName();
		}
	}

	public String sign(String certSerial, String stringToSign) throws Exception
	{
		String outString = null;
		try
		{
			outString = (String) AccessController.doPrivileged(new dStringSignPrivilege(this, certSerial, stringToSign));
		}
		catch (Exception ex)
		{
			VnptTokenApplet.class.getName();
			if (ex.getCause() instanceof PKCS11Exception)
			{
				_exceptionCode = ((PKCS11Exception) ex.getCause()).getErrorCode();
			}
			else
			{
				_exceptionCode = -1L;
			}
			throw ex;
		}
		return outString;
	}

	public String signDocxBase64(String certSerial, String inputBase64) throws Exception
	{
		String outBase64 = null;
		try
		{
			outBase64 = (String) AccessController.doPrivileged(new fDocxBase64SignPrivilege(this, certSerial, inputBase64));
		}
		catch (Exception ex)
		{
			VnptTokenApplet.class.getName();
			if (ex.getCause() instanceof PKCS11Exception)
			{
				_exceptionCode = ((PKCS11Exception) ex.getCause()).getErrorCode();
			}
			else
			{
				_exceptionCode = -1L;
			}
			throw ex;
		}
		return outBase64;
	}

	public String signCMS(String certSerial, String cmsString) throws Exception
	{
		String outString = null;
		try
		{
			outString = (String) AccessController.doPrivileged(new hCmsSignPrivilege(this, certSerial, cmsString));
		}
		catch (Exception ex)
		{
			VnptTokenApplet.class.getName();
			if (ex.getCause() instanceof PKCS11Exception)
			{
				_exceptionCode = ((PKCS11Exception) ex.getCause()).getErrorCode();
			}
			else
			{
				_exceptionCode = -1L;
			}
			throw ex;
		}
		return outString;
	}

	public String signXml(String certSerial, String xmlToSign) throws Exception
	{
		String outXmlString = null;
		try
		{
			outXmlString = (String) AccessController.doPrivileged(new jXmlStringSignPrivilege(this, certSerial, xmlToSign));
		}
		catch (Exception ex)
		{
			VnptTokenApplet.class.getName();
			if (ex.getCause() instanceof PKCS11Exception)
			{
				_exceptionCode = ((PKCS11Exception) ex.getCause()).getErrorCode();
			}
			else if ((ex.getCause() instanceof SAXException))
			{
				_exceptionCode = 2415919200L;
			}
			else
			{
				_exceptionCode = -1L;
			}
			throw ex;
		}
		return outXmlString;
	}

	public void signDocx(String certSerial, String inputFilepath, String outputFilepath) throws Exception
	{
		try
		{
			AccessController.doPrivileged(new lDocxFileSignPrivilege(this, certSerial, inputFilepath, outputFilepath));
			return;
		}
		catch (Exception ex)
		{
			VnptTokenApplet.class.getName();
			if (ex.getCause() instanceof PKCS11Exception)
			{
				_exceptionCode = ((PKCS11Exception) ex.getCause()).getErrorCode();
			}
			else if ((ex.getCause() instanceof FileNotFoundException))
			{
				_exceptionCode = 2415919201L;
			}
			else
			{
				_exceptionCode = -1L;
			}
			throw ex;
		}
	}

	public void signPdf(String certSerial, String temp, String outputUrl) throws Exception
	{
		try
		{
			AccessController.doPrivileged(new nPdfFileSignPrivilege(this, certSerial, outputUrl));
			return;
		}
		catch (Exception ex)
		{
			if (ex.getCause() instanceof PKCS11Exception)
			{
				_exceptionCode = ((PKCS11Exception) ex.getCause()).getErrorCode();
			}
			else if ((ex.getCause() instanceof FileNotFoundException))
			{
				_exceptionCode = 2415919201L;
			}
			else
			{
				_exceptionCode = -1L;
			}
			throw ex;
		}
	}

	public int signPdf(String certSerial, String inputUrl, String outputUrl, String temp, int llx, int lly, int urx, int ury) throws Exception
	{
		try
		{
			AccessController.doPrivileged(new pPdfUrlSignPrivilege(this, temp, llx, lly, urx, ury, certSerial, inputUrl, outputUrl));
		}
		catch (Exception ex)
		{
			if (ex.getCause() instanceof PKCS11Exception)
			{
				_exceptionCode = ((PKCS11Exception) ex.getCause()).getErrorCode();
			}
			else if ((ex.getCause() instanceof FileNotFoundException))
			{
				_exceptionCode = 2415919201L;
			}
			else
			{
				_exceptionCode = -1L;
			}
			throw ex;
		}
		return this._responseCode;
	}

	public int signXml(String certSerial, String inputUrl, String outputUrl, String temp) throws Exception
	{
		try
		{
			AccessController.doPrivileged(new rXmlUrlSignPrivilege(this, temp, certSerial, inputUrl, outputUrl));
		}
		catch (Exception ex)
		{
			if (ex.getCause() instanceof PKCS11Exception)
			{
				_exceptionCode = ((PKCS11Exception) ex.getCause()).getErrorCode();
			}
			else if ((ex.getCause() instanceof FileNotFoundException))
			{
				_exceptionCode = 2415919201L;
			}
			else
			{
				_exceptionCode = -1L;
			}
			throw ex;
		}
		return this._responseCode;
	}

	public int signDocx(String certSerial, String inputUrl, String outputUrl, String temp) throws Exception
	{
		try
		{
			AccessController.doPrivileged(new bDocxUrlSignPrivilege(this, temp, certSerial, inputUrl, outputUrl));
		}
		catch (Exception ex)
		{
			if (ex.getCause() instanceof PKCS11Exception)
			{
				_exceptionCode = ((PKCS11Exception) ex.getCause()).getErrorCode();
			}
			else if (ex.getCause() instanceof FileNotFoundException)
			{
				_exceptionCode = 2415919201L;
			}
			else
			{
				_exceptionCode = -1L;
			}
			throw ex;
		}
		return this._responseCode;
	}

	public String loadTokenInfo()
	{
		this._dlgLoadToken.setVisible(true);
		com.vnpt.b_pac.cToken token = this._dlgLoadToken.getToken();
		if (token == null)
		{
			return "";
		}
		return token.getSlotXml();
	}

	public String loadCertificateInfo()
	{
		return loadCertificateInfo(true);
	}

	public String loadCertificateInfo(boolean isChooseCert)
	{
		return loadCertificateInfo(null, isChooseCert);
	}

	public String loadCertificateInfo(String certSerial)
	{
		return loadCertificateInfo(certSerial, true);
	}

	public String loadCertificateInfo(String certSerial, boolean isChooseCert)
	{
		aCertificate certificate = null;
		List<aCertificate> lstCert = this._dlgLoadCertificate.getListCertificate();
		Iterator<aCertificate> iterCert;

		if (!com.vnpt.common.cDungChung.isNullOrEmpty(certSerial))
		{
			if (!lstCert.isEmpty())
			{
				iterCert = lstCert.iterator();
				while (iterCert.hasNext())
				{
					aCertificate tmp = iterCert.next();
					if (tmp.getSerialNumber().equalsIgnoreCase(certSerial))
					{
						lstCert.remove(tmp);
						break;
					}
				}
			}
			this._dlgLoadCertificate.fillCertsToTable();
		}

		if (isChooseCert)
		{
			List<aCertificate> lstCertificate = lstCert;
			// VnptTokenApplet applet = this;
			if (lstCertificate.isEmpty())
			{
				JOptionPane.showMessageDialog(null, "Hệ thống không tìm thấy thiết bị nào đượcc cắm!\nVui lòng kiểm tra thiết bị!");
				return "";
			}
			if (lstCertificate.size() == 1)
			{
				certificate = lstCertificate.get(0);
				this._dlgLoadCertificate.setCertificate(certificate);
			}
			else
			{
				this._dlgLoadCertificate.setVisible(true);
				certificate = this._dlgLoadCertificate.getCertificate();
				if (certificate == null)
				{
					return "";
				}
			}
			return certificate.getCertificateXml();
		}
		this._dlgLoadCertificate.setVisible(true);

		aCertificate cert = this._dlgLoadCertificate.getCertificate();
		if (cert == null)
		{
			return "";
		}
		return cert.getCertificateXml();
	}
}

/*
 * Location: D:\Code\Visual
 * Studio\CA-VCGM\Doc\VDC\AppletSign-Signbase64-08062015
 * \Sent\VnptTokenApplet.jar!\com\vnpt\VnptTokenApplet.class
 * Java compiler version: 7 (51.0)
 * JD-Core Version: 0.7.1
 */
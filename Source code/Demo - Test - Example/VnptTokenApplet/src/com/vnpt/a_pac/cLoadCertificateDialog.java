package com.vnpt.a_pac;

import com.vnpt.b_pac.aCertificate;
import com.vnpt.b_pac.bTokenDriver;
import com.vnpt.b_pac.cToken;
import com.vnpt.b_pac.dSigner;

import java.awt.Component;
//import java.awt.Container;
import java.awt.Dimension;
import java.awt.Toolkit;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.util.ArrayList;
import java.util.List;
import javax.swing.GroupLayout;
//import javax.swing.GroupLayout.Alignment;
//import javax.swing.GroupLayout.ParallelGroup;
//import javax.swing.GroupLayout.SequentialGroup;
import javax.swing.JButton;
import javax.swing.JDialog;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JScrollPane;
import javax.swing.JTable;
import javax.swing.LayoutStyle;
//import javax.swing.ListSelectionModel;
import javax.swing.event.ListSelectionEvent;
import javax.swing.event.ListSelectionListener;

public class cLoadCertificateDialog extends JDialog
{
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private JButton btnOK;
	private JButton btnCancel;
	private JLabel lblIcon;
	private JScrollPane jScrollPane1;
	private JTable tblCert;
	private List<aCertificate> _lstCert = new ArrayList<>();
	private aCertificate _certificate;

	public final List<aCertificate> getListCertificate()
	{
		return this._lstCert;
	}

	public cLoadCertificateDialog(JFrame owner, boolean modal)
	{
		super((JFrame) null, true);
		
		setTitle("Lựa chọn chứng thư số");
		this.setAlwaysOnTop(true);
		this.lblIcon = new JLabel();
		this.jScrollPane1 = new JScrollPane();
		this.tblCert = new JTable();
		this.btnOK = new JButton();
		this.btnCancel = new JButton();
		this.setDefaultCloseOperation(2);
		this.lblIcon.setIcon(com.vnpt.common.cDungChung.icon);
		this.loadCertificate();
		this.tblCert.setSelectionMode(0);
		this.tblCert.getSelectionModel().addListSelectionListener(new ListSelectionListener()
		{
			public final void valueChanged(ListSelectionEvent e)
			{
				if ((tblCert.getSelectedRow() >= 0) && (tblCert.getSelectedRow() < _lstCert.size()))
				{
					_certificate = _lstCert.get(tblCert.getSelectedRow());
					return;
				}
				_certificate = null;
			}
		});
		this.jScrollPane1.setViewportView(this.tblCert);
		this.btnOK.setText("Đồng ý");
		this.btnOK.addActionListener(new ActionListener()
		{
			public final void actionPerformed(ActionEvent e)
			{
				if (_certificate == null)
				{
					JOptionPane.showMessageDialog((Component) e.getSource(), "Bạn chưa chọn chứng thư số", "Lỗi chọn chứng thư", 0);
					return;
				}
				setVisible(false);
			}
		});
		this.btnCancel.setText("Hủy bỏ");
		this.btnCancel.addActionListener(new ActionListener()
		{
			public final void actionPerformed(ActionEvent e)
			{
				tblCert.clearSelection();
				setVisible(false);
			}
		});
		GroupLayout layout = new GroupLayout(this.getContentPane());
		this.getContentPane().setLayout(layout);
		layout.setHorizontalGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
				.addGroup(layout.createSequentialGroup().addContainerGap()
						.addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
								.addGroup(layout.createSequentialGroup()
										.addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
												.addComponent(this.lblIcon, -1, 430, 32767)
												.addComponent(this.jScrollPane1, GroupLayout.Alignment.TRAILING, -2, 425, -2))
										.addContainerGap()).addGroup(GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
												.addComponent(this.btnOK).addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
												.addComponent(this.btnCancel).addGap(12, 12, 12)))));
		layout.setVerticalGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
				.addGroup(layout.createSequentialGroup().addComponent(this.lblIcon)
						.addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
						.addComponent(this.jScrollPane1, -2, 90, -2).addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
						.addGroup(layout.createParallelGroup(GroupLayout.Alignment.BASELINE)
								.addComponent(this.btnOK).addComponent(this.btnCancel))
						.addContainerGap(15, 32767)));
		this.pack();
		Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
		this.setLocation((int) ((dim.getWidth() - this.getWidth()) / 2.0D), (int) ((dim.getHeight() - this.getHeight()) / 2.0D));
		this.addWindowListener(new WindowAdapter()
		{
			public final void windowClosing(WindowEvent e)
			{
				setVisible(false);
			}
		});
	}

	public final void fillCertsToTable()
	{
		String[][] certInfo = new String[this._lstCert.size()][3];
		for (int j = 0; j < this._lstCert.size(); j++)
		{
			aCertificate cert = this._lstCert.get(j);
			certInfo[j][0] = cert.getFriendlyName();
			certInfo[j][1] = cert.getIssuer();
			certInfo[j][2] = cert.getValidTo();
		}
		this.tblCert.setModel(new CustomTableModel(this, certInfo, new String[] { "Cấp cho", "Cấp bởi", "Hết hạn" }));
	}

	public final aCertificate getCertificate()
	{
		return this._certificate;
	}

	public final void setCertificate(aCertificate certificate)
	{
		this._certificate = certificate;
	}

	private void loadCertificate()
	{
		try
		{
			//Lấy chứng thư trong danh sách thiết bị
			for (int i = 0; i < dSigner.TokenDrivers.size(); i++)
			{
				bTokenDriver driver = dSigner.TokenDrivers.get(i);
				for (int j = 0; j < driver.getListToken().size(); j++)
				{
					cToken token = (cToken) driver.getListToken().get(j);
					for (int k = 0; k < token.getListCertificate().size(); k++)
					{
						aCertificate cert = (aCertificate) token.getListCertificate().get(k);
						this._lstCert.add(cert);
					}
				}
			}
			
			//Lấy thông tin chứng thư đổ vào jTable
			String[][] certInfo = new String[this._lstCert.size()][3];
			for (int j = 0; j < this._lstCert.size(); j++)
			{
				aCertificate cert = this._lstCert.get(j);
				certInfo[j][0] = cert.getFriendlyName();
				certInfo[j][1] = cert.getIssuer();
				certInfo[j][2] = cert.getValidTo();
			}
			this.tblCert.setModel(new CustomTableModel(this, (Object[][]) certInfo, new String[] { "Cấp cho", "Cấp bởi", "Hết hạn" }));
			return;
		}
		catch (Exception localException)
		{
			cLoadCertificateDialog.class.getName();
		}
	}
}

/*
 * Location: D:\Code\Visual
 * Studio\CA-VCGM\Doc\VDC\AppletSign-Signbase64-08062015\Sent\VnptTokenApplet.
 * jar!\com\vnpt\a\c.class
 * Java compiler version: 7 (51.0)
 * JD-Core Version: 0.7.1
 */
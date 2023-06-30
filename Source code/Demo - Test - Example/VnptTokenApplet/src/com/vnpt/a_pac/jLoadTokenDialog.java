package com.vnpt.a_pac;

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

public final class jLoadTokenDialog extends JDialog
{
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private JButton btnOK;
	private JButton btnCancel;
	private JLabel lblLogo;
	private JScrollPane jScrollPane1;
	private JTable tblThietBi;
	private List<cToken> _lstToken = new ArrayList<>();
	private com.vnpt.b_pac.cToken _token;

	public jLoadTokenDialog(JFrame owner, boolean modal)
	{
		super((JFrame) null, true); this.

		setTitle("Lựa chọn thiết bị");
		this.setAlwaysOnTop(true);
		this.lblLogo = new JLabel();
		this.jScrollPane1 = new JScrollPane();
		this.tblThietBi = new JTable();
		this.btnOK = new JButton();
		this.btnCancel = new JButton();
		this.setDefaultCloseOperation(2);
		this.lblLogo.setIcon(com.vnpt.common.cDungChung.icon);
		this.loadToken();
		this.tblThietBi.setSelectionMode(0);
		this.tblThietBi.getSelectionModel().addListSelectionListener(new ListSelectionListener()
		{
			public final void valueChanged(ListSelectionEvent e)
			{
				if ((tblThietBi.getSelectedRow() >= 0) && (tblThietBi.getSelectedRow() < _lstToken.size()))
				{
					_token = _lstToken.get(tblThietBi.getSelectedRow());
					return;
				}
				_token = null;
			}
		});
		this.jScrollPane1.setViewportView(this.tblThietBi);
		this.btnOK.setText("Đồng ý");
		this.btnOK.addActionListener(new ActionListener()
		{
			public final void actionPerformed(ActionEvent e)
			{
				if (_token == null)
				{
					JOptionPane.showMessageDialog((Component) e.getSource(), "Bạn chưa chọn thiết bị nào", "Lỗi chọn thiết bị", JOptionPane.ERROR_MESSAGE);
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
				tblThietBi.clearSelection();
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
												.addComponent(this.lblLogo, -1, 430, 32767)
												.addComponent(this.jScrollPane1, GroupLayout.Alignment.TRAILING, -2, 425, -2)).addContainerGap())
								.addGroup(GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
										.addComponent(this.btnOK).addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
										.addComponent(this.btnCancel).addGap(12, 12, 12)))));
		layout.setVerticalGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
				.addGroup(layout.createSequentialGroup().addComponent(this.lblLogo)
						.addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
						.addComponent(this.jScrollPane1, -2, 90, -2)
						.addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
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

	public final cToken getToken()
	{
		return this._token;
	}

	private void loadToken()
	{
		try
		{
			//Lấy danh sách thiết bị
			for (int i = 0; i < dSigner.TokenDrivers.size(); i++)
			{
				bTokenDriver driver = dSigner.TokenDrivers.get(i);
				for (int j = 0; j < driver.getListToken().size(); j++)
				{
					cToken token = (cToken) driver.getListToken().get(j);
					this._lstToken.add(token);
				}
			}
			
			//Lấy thông tin thiết bị và đổ vào JTable
			String[][] tokenInfo = new String[this._lstToken.size()][3];
			for (int j = 0; j < this._lstToken.size(); j++)
			{
				cToken token = this._lstToken.get(j);
				tokenInfo[j][0] = String.valueOf(token.getSlotId());
				tokenInfo[j][1] = token.getManufactureId();
				tokenInfo[j][2] = token.getTokenSerial();
			}
			this.tblThietBi.setModel(new CustomTableModel(this, tokenInfo, new String[] { "Slot", "Hãng sản xuất", "Serial Number" }));
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
 * jar!\com\vnpt\a\j.class
 * Java compiler version: 7 (51.0)
 * JD-Core Version: 0.7.1
 */
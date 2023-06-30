package com.vnpt.a_pac;

import javax.swing.JDialog;
import javax.swing.table.DefaultTableModel;

final class CustomTableModel extends DefaultTableModel
{
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;

	CustomTableModel(JDialog owner, Object[][] data, Object[] header)
	{
		super(data, header);
	}

	public final boolean isCellEditable(int col, int row)
	{
		return false;
	}
}

/*
 * Location: D:\Code\Visual
 * Studio\CA-VCGM\Doc\VDC\AppletSign-Signbase64-08062015\Sent\VnptTokenApplet.
 * jar!\com\vnpt\a\o.class
 * Java compiler version: 7 (51.0)
 * JD-Core Version: 0.7.1
 */
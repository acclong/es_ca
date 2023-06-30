package com.vnpt.common;

import java.io.File;
import javax.swing.filechooser.FileFilter;

public final class bFileFilter
  extends FileFilter
{
  private String _description;
  private String[] _extensions = null;
  
  public bFileFilter(String description, String[] arrFileExtensions)
  {
    if (description == null) {
      this._description = arrFileExtensions[0];
    } else {
      this._description = description;
    }
    if (arrFileExtensions != null)
    {
      this._extensions = ((String[])arrFileExtensions.clone());
      toLowerCase(this._extensions);
    }
  }
  
  private static void toLowerCase(String[] arrString)
  {
    int i = 0;
    int j = arrString.length;
    while (i < j)
    {
      arrString[i] = arrString[i].toLowerCase();
      i++;
    }
  }
  
  public final String getDescription()
  {
    return this._description;
  }
  
  public final boolean accept(File file)
  {
    if (file.isDirectory()) {
      return true;
    }
    if (this._extensions != null)
    {
      String path = file.getAbsolutePath().toLowerCase();
      int i = 0;
      int j = this._extensions.length;
      while (i < j)
      {
        String str = this._extensions[i];
        if ((path.endsWith(str)) && (path.charAt((int)(file.length() - str.length() - 1)) == '.')) {
          return true;
        }
        i++;
      }
    }
    else
    {
      return true;
    }
    return false;
  }
}


/* Location:              D:\Code\Visual Studio\CA-VCGM\Doc\VDC\AppletSign-Signbase64-08062015\Sent\VnptTokenApplet.jar!\com\vnpt\c\b.class
 * Java compiler version: 7 (51.0)
 * JD-Core Version:       0.7.1
 */
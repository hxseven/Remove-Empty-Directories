using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

/*
 * By: Burgpflanze 
 * 
 * Source: http://www.mycsharp.de/wbb2/thread.php?threadid=6439&threadview=0&hilight=&hilightuser=0&page=2
 * 
 * Modified by Myself.
 */

namespace RED2
{
    class cSettings
    {
        private XmlDocument xmlDoc;
	    private XmlElement root = null;
	    private string xmlConfigPath;
    	
	    public cSettings (string configPath)
	    {
            xmlConfigPath = configPath;

		    xmlDoc = new XmlDocument();

            if (File.Exists(configPath))
		    {
			    try
			    {
                    xmlDoc.Load(xmlConfigPath);
				    root = xmlDoc.DocumentElement;
			    }
			    catch (Exception e)
			    {
				    throw e;
			    }
		    }
		    else 
		    {
			    XmlDeclaration xmlDec = xmlDoc.CreateXmlDeclaration ("1.0", "UTF-8", "yes");
			    xmlDoc.AppendChild (xmlDec);
			    //xmlDoc.Save (xmlFile);
		    }
    		
		    if (root == null)
		    {
			    root = xmlDoc.CreateElement("settings");
			    xmlDoc.AppendChild (root);
			    xmlDoc.Save (xmlConfigPath);
		    }
	    }
    	
	    private XmlNode FindNode (string key, bool createNew)
	    {
            XmlNodeList Nodes = root.SelectNodes("//settings/" + key);
            return (Nodes.Count == 1) ? Nodes[0] : null;
	    }
    	
	    public void Remove (string key)
	    {
		    XmlNode parent;
		    XmlNode node = FindNode (key, false);
    		
		    while (node != null && node.ChildNodes.Count == 0)
		    {
			    parent = node.ParentNode;
			    parent.RemoveChild (node);
			    if (parent == root)
				    break;
			    node = parent;
		    }
		    xmlDoc.Save (xmlConfigPath);
	    }
    	
	    public void Write (string key, string newValue)
	    {
		    XmlNode node = FindNode (key, true);
            if (node == null)
            {
                node = xmlDoc.CreateElement(key);
                root.AppendChild(node);
            }

		    node.InnerText = newValue;
		    xmlDoc.Save (xmlConfigPath);
	    }
    	
	    public void Write (string key, char newValue)
	    {
		    Write (key, newValue.ToString ());
	    }
    	
	    public void Write (string key, byte newValue)
	    {
		    Write (key, newValue.ToString ());
	    }
    	
	    public void Write (string key, short newValue)
	    {
		    Write (key, newValue.ToString ());
	    }
    	
	    public void Write (string key, int newValue)
	    {
		    Write (key, newValue.ToString ());
	    }

        public void Write(string key, bool newValue)
        {
            Write(key, newValue.ToString());
        }
    	
	    public void Write (string key, long newValue)
	    {
		    Write (key, newValue.ToString ());
	    }
    	
	    public void Write (string key, ushort newValue)
	    {
		    Write (key, newValue.ToString ());
	    }
    	
	    public void Write (string key, uint newValue)
	    {
		    Write (key, newValue.ToString ());
	    }
    	
	    public void Write (string key, ulong newValue)
	    {
		    Write (key, newValue.ToString ());
	    }
    	
	    public void Write (string key, float newValue)
	    {
		    Write (key, newValue.ToString ());
	    }
    	
	    public void Write (string key, double newValue)
	    {
		    Write (key, newValue.ToString ());
	    }
    	
	    public void Write (string key, DateTime newValue)
	    {
		    Write (key, newValue.Ticks.ToString ());
	    }	
    	
	    public string Read (string key, string defValue)
	    {
		    XmlNode node = FindNode (key, false);
		    if (node != null && node.InnerText != null)
			    return node.InnerText;
		    else
			    return defValue;
	    }
    	
	    public char Read (string key, char defValue)
	    {
		    string result = Read (key, defValue.ToString ());
		    try
		    {
			    return Convert.ToChar(result);
		    }
		    catch
		    {
			    return defValue;
		    }
	    }
    	
	    public byte Read (string key, byte defValue)
	    {
		    string result = Read (key, defValue.ToString ());
		    try
		    {
			    return Convert.ToByte (result);
		    }
		    catch
		    {
			    return defValue;
		    }
	    }
    	
	    public short Read (string key, short defValue)
	    {
		    string result = Read (key, defValue.ToString ());
		    try
		    {
			    return Convert.ToInt16 (result);
		    }
		    catch
		    {
			    return defValue;
		    }
	    }
    	
	    public int Read (string key, int defValue)
	    {
		    string result = Read (key, defValue.ToString ());
		    try
		    {
			    return Convert.ToInt32 (result);
		    }
		    catch
		    {
			    return defValue;
		    }
	    }
    	
	    public long Read (string key, long defValue)
	    {
		    string result = Read (key, defValue.ToString ());
		    try
		    {
			    return Convert.ToInt64 (result);
		    }
		    catch
		    {
			    return defValue;
		    }
	    }
    	
	    public ushort Read (string key, ushort defValue)
	    {
		    string result = Read (key, defValue.ToString ());
		    try
		    {
			    return Convert.ToUInt16 (result);
		    }
		    catch
		    {
			    return defValue;
		    }
	    }
    	
	    public uint Read (string key, uint defValue)
	    {
		    string result = Read (key, defValue.ToString ());
		    try
		    {
			    return Convert.ToUInt32 (result);
		    }
		    catch
		    {
			    return defValue;
		    }
	    }
    	
	    public ulong Read (string key, ulong defValue)
	    {
		    string result = Read (key, defValue.ToString ());
		    try
		    {
			    return Convert.ToUInt64 (result);
		    }
		    catch
		    {
			    return defValue;
		    }
	    }
    	
	    public bool Read (string key, bool defValue)
	    {
		    string result = Read (key, defValue.ToString ());
		    try
		    {
			    return Convert.ToBoolean (result);
		    }
		    catch
		    {
			    return defValue;
		    }
	    }
    	
	    public float Read (string key, float defValue)
	    {
		    string result = Read (key, defValue.ToString ());
		    try
		    {
			    return Convert.ToSingle (result);
		    }
		    catch
		    {
			    return defValue;
		    }
	    }

	    public double Read (string key, double defValue)
	    {
		    string result = Read (key, defValue.ToString ());
		    try
		    {
			    return Convert.ToDouble (result);
		    }
		    catch
		    {
			    return defValue;
		    }
	    }
    	
	    public DateTime Read (string key, DateTime defValue)
	    {
		    string result = Read (key, defValue.Ticks.ToString ());
		    try
		    {
			    return new DateTime (Convert.ToInt64 (result));
		    }
		    catch
		    {
			    return defValue;
		    }
	    }
    }
}

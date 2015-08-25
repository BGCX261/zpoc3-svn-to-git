
using System;
using System.Collections;
using System.Xml;
using ZPoC.Enums;

namespace ZPoC.Core
{
	
	
	public class Preferences
	{
		#region Protected local variables
		protected const string cNAMESPACE = "ZPoC";
		protected const string cVERSION = "3.00a";
		protected XmlTextReader reader;
		protected XmlTextWriter writer;
		protected string fileToOpen = String.Empty;
		protected bool loadSuccess = false;
		protected bool saveSuccess = false;
		protected string pNamespace = String.Empty;
		protected string pVersion = String.Empty;
		protected string pNick = String.Empty;
		protected string pDesc = String.Empty;
		protected string pPass = String.Empty;
		protected string pContact = String.Empty;
		protected bool pAutoReg = false;
		protected Connection pConnex;
		protected ArrayList pListSrv = new ArrayList();
		protected bool isNew = true;
		#endregion
		
		#region Public properties
		public string Name
		{
			get { return this.fileToOpen; }
		}
		public string Nick
		{
			get { return this.pNick; }
			set { this.pNick = value; }
		}
		public string Description
		{
			get { return this.pDesc; }
			set { this.pDesc = value; }
		}
		public string Password
		{
			get { return this.pPass; }
			set { this.pPass = value; }
		}
		public string ContactInfo
		{
			get { return this.pContact; }
			set { this.pContact = value; }
		}
		public bool AutomaticallyRegister
		{
			get { return this.pAutoReg; }
			set { this.pAutoReg = value; }
		}
		public Connection ConnectionType
		{
			get { return this.pConnex; }
			set { this.pConnex = value; }
		}
		public ArrayList HublistServers
		{
			get { return this.pListSrv; }
			protected set { this.pListSrv = value; }
		}
		public bool IsNew
		{
			get { return this.isNew; }
		}
		#endregion
		
		public Preferences() : this("prefs.xml") { }
		
		public Preferences(string xmlfile)
		{
			this.fileToOpen = xmlfile;
			if(System.IO.File.Exists(this.fileToOpen))
			{
				this.isNew = false;
			}
		}
		public void Load()
		{
			this.reader = new XmlTextReader(this.fileToOpen);
			if(reader.ReadToFollowing("Preferences") && reader.HasAttributes)
			{
				this.pNamespace = reader.GetAttribute("namespace");
				this.pVersion = reader.GetAttribute("version");
				
				if(reader.ReadToFollowing("General"))
				{
					if(reader.ReadToFollowing("Nick"))
					{
						this.pNick = reader.ReadElementContentAsString();
					}
					if(reader.ReadToFollowing("ConnectionType"))
					{
						string conn = reader.ReadElementContentAsString();
						Connection connex;
						switch(conn)
						{
						case "56Kbps":
						case "Modem":
							connex = Connection.Modem;
							break;
						case "ISDN":
							connex = Connection.ISDN;
							break;
						case "Cable":
							connex = Connection.Cable;
							break;
						case "DSL":
							connex = Connection.DSL;
							break;
						case "Fiber":
							connex = Connection.Fiber;
							break;
						case "T1":
							connex = Connection.T1;
							break;
						case "T3":
							connex = Connection.T3;
							break;
						case "OC3":
							connex = Connection.OC3;
							break;
						case "LAN":
							connex = Connection.LAN;
							break;
						default:
							connex = Connection.Modem;
							break;
						}
						this.pConnex = connex;
					}
					if(reader.ReadToFollowing("Description"))
					{
						this.pDesc = reader.ReadElementContentAsString();
					}
					if(reader.ReadToFollowing("Password"))
					{
						string hash = reader.GetAttribute("hash");
						if(hash != null)
						{
							switch(hash)
							{
							case "plain":
								this.pPass = reader.ReadElementContentAsString();
								break;
							}
						}
					}
					if(reader.ReadToFollowing("Contact"))
					{
						this.pContact = reader.ReadElementContentAsString();
					}
					if(reader.ReadToFollowing("AutoRegister") && reader.HasAttributes)
					{
						this.pAutoReg = reader.ReadElementContentAsBoolean();
					}
				}
				if(reader.ReadToFollowing("Connection"))
				{
					if(reader.ReadToFollowing("RoomLists"))
					{
						this.pListSrv.Clear();
						string[] arr = (reader.ReadElementContentAsString()).Split(new char[]{';'});
						for(int i=0;i<arr.Length;i++)
						{
							this.pListSrv.Add(arr[i]);
						}
					}
				}
				
				this.loadSuccess = true;
			}
			reader.Close();
		}
		public void Load(string xmlfile)
		{
			this.fileToOpen = xmlfile;
			this.Load();
		}
		public void Save()
		{
			this.writer = new XmlTextWriter(this.fileToOpen, null);
			writer.WriteStartDocument();
			
			writer.WriteStartElement("Preferences");							//start Preferences
			writer.WriteAttributeString("namespace", cNAMESPACE);
			//writer.WriteStartAttribute("namespace", cNAMESPACE);				//Preferences namespace=cNAMESPACE
			//writer.WriteEndAttribute();
			writer.WriteAttributeString("version", cVERSION);
			//writer.WriteStartAttribute("version", cVERSION);					//Preferences version=cVERSION
			//writer.WriteEndAttribute();
			writer.WriteStartElement("General");								//start General
			writer.WriteStartElement("Nick");									//start Nick
			writer.WriteString(this.pNick);										//set Nick=this.pNick
			writer.WriteEndElement();											//end Nick
			writer.WriteStartElement("ConnectionType");								//start Connection
			writer.WriteString(this.GetStringFromEnum(this.ConnectionType));	//set Connection=this.ConnectionType
			writer.WriteEndElement();											//end Connection
			writer.WriteStartElement("Description");							//start Description
			writer.WriteString(this.pDesc);										//set Description=this.pDesc
			writer.WriteEndElement();											//end Description
			writer.WriteStartElement("Password");								//start Password
			writer.WriteAttributeString("hash", "plain");
			//writer.WriteStartAttribute("hash", "plain");						//Password hash=plain
			//writer.WriteEndAttribute();
			writer.WriteString(this.pPass);										//set Password=this.pPass
			writer.WriteEndElement();											//end Password
			writer.WriteStartElement("Contact");								//start Contact
			writer.WriteAttributeString("type", "email");
			//writer.WriteStartAttribute("type", "email");
			//writer.WriteEndAttribute();
			writer.WriteString(this.pContact);
			writer.WriteEndElement();
			writer.WriteStartElement("AutoRegister");
			writer.WriteAttributeString("value", this.pAutoReg.ToString());
			//writer.WriteStartAttribute("value", this.pAutoReg.ToString());
			//writer.WriteEndAttribute();
			writer.WriteEndElement();
			writer.WriteEndElement();											//end General
			writer.WriteStartElement("Connection");
			writer.WriteStartElement("RoomLists");
			writer.WriteString(this.GetStringFromArrayList(this.pListSrv, ';'));									//FIXME: Add roomlist ArrayList to string parser!
			writer.WriteEndElement();
			writer.WriteEndElement();
			writer.WriteEndElement();											//end Preferences
			
			writer.WriteEndDocument();
			writer.Close();
			
			this.saveSuccess = true;
			this.isNew = false;
		}
		public void Save(string xmlfile)
		{
			this.fileToOpen = xmlfile;
			this.Save();
		}
		
		private string GetStringFromArrayList(ArrayList arr, char sep)
		{
			System.Text.StringBuilder s = new System.Text.StringBuilder();
			IEnumerator line = arr.GetEnumerator();
			while(line.MoveNext())
			{
				s.Append(line.Current.ToString());
				s.Append(sep);
			}
			return s.ToString();
		}
		private string GetStringFromEnum(Connection c)
		{
			string s = String.Empty;
			switch(c)
			{
			case Connection.Modem:
				s = "Modem";
				break;
			case Connection.ISDN:
				s = "ISDN";
				break;
			case Connection.Cable:
				s = "Cable";
				break;
			case Connection.DSL:
				s = "DSL";
				break;
			case Connection.Fiber:
				s = "Fiber";
				break;
			case Connection.T1:
				s = "T1";
				break;
			case Connection.T3:
				s = "T3";
				break;
			case Connection.OC3:
				s = "OC3";
				break;
			case Connection.LAN:
				s = "LAN";
				break;
			}
			return s;
		}
	}
}

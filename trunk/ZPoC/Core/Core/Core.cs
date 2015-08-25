
using System;
using System.Collections.Generic;
using FlowLib.Connections;
using FlowLib.Containers;
using FlowLib.Events;

namespace ZPoC.Core
{
	
	
	public class Core
	{
		protected SortedList<string, Hub> hublist;
		protected Preferences prefs;
		
		public SortedList<string, Hub> HubList
		{
			get { return this.hublist; }
		}
		
		public Core()
		{
			hublist = new SortedList<string, Hub>();
			prefs = new Preferences();
			
			if(prefs.IsNew) {
				prefs.Nick = "ZPoC3Test";
				prefs.Description = "Testing a new ZPoC client!";
				prefs.ContactInfo = "new@zpoc.com";
				prefs.ConnectionType = ZPoC.Enums.Connection.LAN;
				prefs.Save();
			}
			else
			{
				prefs.Load();
			}
		}
		public bool ConnectToHub(string ip)
		{
			HubSetting settings = new HubSetting();
			settings.Address = ip;
			settings.Protocol = "Zpoc";
			settings.DisplayName = prefs.Nick;
			
			Hub hub = new Hub(settings);
			hub.Me.TagInfo.Version = "ZPoc V:3.00a";
			hub.ProtocolChange += new FmdcEventHandler(protUpdate);
			hub.Connect();
			
			return true;
		}
		public void protUpdate(object sender, FmdcEventArgs e)
		{
			Hub hub = (sender as Hub);
			hub.Protocol.Update += new FmdcEventHandler(protRcvd);
		}
		public void protRcvd(object sender, FmdcEventArgs e)
		{
			Hub hub = (sender as Hub);
			switch(e.Action)
			{
			case Actions.StatusChange:
				HubStatus status = (e.Data as HubStatus);
				if(status.Code.Equals("Connected"))
				{
					this.hublist.Add(hub.Name, hub);
				}
				else if(status.Code.Equals("Disconnected"))
				{
					this.hublist.Remove(hub.Name);
				}
				break;
			}
		}
	}
}

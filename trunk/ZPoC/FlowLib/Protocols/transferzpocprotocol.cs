
/*
 *
 * Copyright (C) 2009 Tosk
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
 *
 */

using System.Threading;

using FlowLib.Events;
using FlowLib.Interfaces;
using FlowLib.Protocols.TransferZpoc;
using FlowLib.Containers;
using FlowLib.Connections;
using FlowLib.Managers;
using FlowLib.Enums;

#if COMPACT_FRAMEWORK
using FlowLib.Utils.CompactFramworkExtensionMethods;
#endif

namespace FlowLib.Protocols
{
    /// <summary>
    /// Transfer Zpoc Protocol
    /// </summary>
    public class TransferZpocProtocol : TransferNmdcProtocol
    {
        public event FmdcEventHandler MessageReceived;
        public event FmdcEventHandler MessageToSend;
        public event FmdcEventHandler ChangeDownloadItem;
        public event FmdcEventHandler RequestTransfer;
        public event FmdcEventHandler Error;
        public event FmdcEventHandler Update;

        public string Name
        {
            get { return "Zpoc"; }
        }
        
        public TransferZpocProtocol(ITransfer trans)
            :this(trans,
#if !COMPACT_FRAMEWORK
            System.AppDomain.CurrentDomain.BaseDirectory
#else
            System.IO.Directory.GetCurrentDirectory()
#endif
            )
        {

        }

        public TransferZpocProtocol(ITransfer trans, string dir) : base(trans, dir)
        {
        }
	}
}
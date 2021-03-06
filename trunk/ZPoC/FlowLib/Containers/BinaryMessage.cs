
/*
 *
 * Copyright (C) 2009 Mattias Blomqvist, patr-blo at dsv dot su dot se
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

using FlowLib.Interfaces;

namespace FlowLib.Containers
{
    /// <summary>
    /// This class is mainly for indicating binary downloaded content.
    /// This is so you can make diffrent actions depending on if it is a message or binary data
    /// </summary>
    public class BinaryMessage : ConMessage
    {
        protected int length = -1;

        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        /// <summary>
        /// Creating Binary message
        /// </summary>
        /// <param name="con">Connection where message should be sent or was received from</param>
        /// <param name="raw">content of this message</param>
        public BinaryMessage(IConnection con, byte[] raw, int length)
            : base(con, raw)
        {
            this.length = length;
        }
    }
}

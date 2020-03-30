﻿using Newtonsoft.Json.Linq;
using System;
using System.Text;

namespace EngineIOSharp.Common.Packet
{
    partial class EngineIOPacket
    {
        internal static EngineIOPacket CreateErrorPacket(Exception Exception)
        {
            return CreatePacket(EngineIOPacketType.UNKNOWN, Exception.ToString());
        }

        internal static EngineIOPacket CreateOpenPacket(string SocketID, int PingInterval, int PingTimeout)
        {
            return CreatePacket(EngineIOPacketType.OPEN, new JObject()
            {
                ["sid"] = SocketID,
                ["pingInterval"] = PingInterval,
                ["pingTimeout"] = PingTimeout,
                ["upgrades"] = new JArray()
            }.ToString());
        }

        internal static EngineIOPacket CreateClosePacket()
        {
            return CreatePacket(EngineIOPacketType.CLOSE);
        }

        internal static EngineIOPacket CreatePingPacket(string Data = null)
        {
            return CreatePacket(EngineIOPacketType.PING, Data ?? string.Empty);
        }

        internal static EngineIOPacket CreatePongPacket(string Data = null)
        {
            return CreatePacket(EngineIOPacketType.PONG, Data ?? string.Empty);
        }

        internal static EngineIOPacket CreateMessagePacket(string Data)
        {
            return CreatePacket(EngineIOPacketType.MESSAGE, Data);
        }

        internal static EngineIOPacket CreateMessagePacket(byte[] RawData)
        {
            return CreatePacket(EngineIOPacketType.MESSAGE, RawData);
        }

        private static EngineIOPacket CreatePacket(EngineIOPacketType Type)
        {
            return new EngineIOPacket()
            {
                Type = Type,
                IsText = true,
            };
        }

        private static EngineIOPacket CreatePacket(EngineIOPacketType Type, string Data)
        {
            return new EngineIOPacket()
            {
                Type = Type,
                IsText = true,
                Data = Data,
                RawData = Encoding.UTF8.GetBytes(Data)
            };
        }

        private static EngineIOPacket CreatePacket(EngineIOPacketType Type, byte[] RawData)
        {
            return new EngineIOPacket()
            {
                Type = Type,
                IsBinary = true,
                Data = BitConverter.ToString(RawData),
                RawData = RawData
            };
        }
    }
}

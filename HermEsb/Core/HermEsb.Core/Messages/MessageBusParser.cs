using System;
using System.Text;
using HermEsb.Core.Compression;
using HermEsb.Core.Serialization;

namespace HermEsb.Core.Messages
{
    public static class MessageBusParser
    {
        private static readonly IDataContractSerializer Serializer = new JsonDataContractSerializer();

        public static RouterHeader GetHeaderFromBytes(byte[] message)
        {
            var routerHeader = new RouterHeader();
            routerHeader.Type = (MessageBusType)message[0];
            routerHeader.Priority = message[1];
            var ticks = ((long)message[3]) + ((long)message[4] << 8) + ((long)message[5] << 16)
                         + ((long)message[6] << 24) + ((long)message[7] << 32) + ((long)message[8] << 40)
                         + ((long)message[9] << 48) + ((long)message[10] << 56);
            routerHeader.CreatedAt = new DateTime(ticks);

            var bodyLength = (message[11]) + (message[12] << 8) + (message[13] << 16) + (message[14] << 24);
            var arraybody = new byte[bodyLength];
            Array.Copy(message, 15, arraybody, 0, bodyLength);
            routerHeader.BodyType = Encoding.UTF8.GetString(arraybody);

            var messagePosition = 15 + bodyLength;
            routerHeader.MessageLength = (message[messagePosition]) + (message[messagePosition + 1] << 8) +
                                         (message[messagePosition + 2] << 16) + (message[messagePosition + 3] << 24);
            routerHeader.MessagePosition = messagePosition + 4;

            routerHeader.Identification = new Identification();

            var idPosition = routerHeader.MessagePosition + routerHeader.MessageLength;
            var idLength = (message[idPosition]) + (message[idPosition + 1] << 8) + (message[idPosition + 2] << 16) + (message[idPosition + 3] << 24);
            if (idLength > 0)
            {
                var idArray = new byte[idLength];
                Array.Copy(message, idPosition + 4, idArray, 0, idLength);
                routerHeader.Identification.Id = Encoding.UTF8.GetString(idArray);

                var typePosition = idPosition + 4 + idLength;
                var typeLength = (message[typePosition] ) + (message[typePosition + 1] << 8) +
                                 (message[typePosition + 2] << 16) + (message[typePosition + 3] << 24);
                var typeArray = new byte[typeLength];
                Array.Copy(message, typePosition + 4, typeArray, 0, typeLength);
                routerHeader.Identification.Type = Encoding.UTF8.GetString(typeArray);
            }
            return routerHeader;
        }

        public static string GetMessageFromBytes(byte[] message)
        {
            var bodyTypeLength = message[11] + (message[12] << 8) + (message[13] << 16) + (message[14] << 24);
            var messagePosition = 15 + bodyTypeLength;
            var messageLength = message[messagePosition]  + (message[messagePosition + 1] << 8)
                                + (message[messagePosition + 2] << 16) + (message[messagePosition + 3] << 24);
            var arrayMessage = new byte[messageLength];
            Array.Copy(message, messagePosition + 4, arrayMessage, 0, messageLength);

            return GzipCompression.Unzip(arrayMessage);
        }

        public static byte[] ToBytes(MessageBus messageBus)
        {
            var serializedMessage = Serializer.Serialize(messageBus);

            var bytesMessage = GzipCompression.Zip(serializedMessage);
            var bytesBodyType = Encoding.UTF8.GetBytes(messageBus.Header.BodyType);

            CallerContext callerContext = null;
            if (messageBus.Header.CallStack.Count > 0)
                callerContext = messageBus.Header.CallStack.Peek();
            byte[] idBytes = new byte[0];
            byte[] typeBytes = new byte[0];
            int bufferLength = 0;
            if (callerContext != null)
            {
                idBytes = Encoding.UTF8.GetBytes(callerContext.Identification.Id);
                typeBytes = Encoding.UTF8.GetBytes(callerContext.Identification.Type);
                bufferLength = 15 + bytesBodyType.Length + 4 + bytesMessage.Length + 4 + idBytes.Length + 4 +
                               typeBytes.Length;
            }
            else
            {
                bufferLength = 15 + bytesBodyType.Length + 4 + bytesMessage.Length + 4;
            }


            var bytes = new byte[bufferLength];
            bytes[0] = (byte)messageBus.Header.Type;
            bytes[1] = (byte)messageBus.Header.Priority;

            var ticks = messageBus.Header.CreatedAt.Ticks;
            bytes[3] = (byte)(ticks);
            bytes[4] = (byte)(ticks >> 8);
            bytes[5] = (byte)(ticks >> 16);
            bytes[6] = (byte)(ticks >> 24);
            bytes[7] = (byte)(ticks >> 32);
            bytes[8] = (byte)(ticks >> 40);
            bytes[9] = (byte)(ticks >> 48);
            bytes[10] = (byte)(ticks >> 56);


            bytes[11] = (byte)(bytesBodyType.Length);
            bytes[12] = (byte)(bytesBodyType.Length >> 8);
            bytes[13] = (byte)(bytesBodyType.Length >> 16);
            bytes[14] = (byte)(bytesBodyType.Length >> 24);

            Array.Copy(bytesBodyType, 0, bytes, 15, bytesBodyType.Length);

            var messagePosition = 15 + bytesBodyType.Length;

            bytes[messagePosition] = (byte)(bytesMessage.Length);
            bytes[messagePosition + 1] = (byte)(bytesMessage.Length >> 8);
            bytes[messagePosition + 2] = (byte)(bytesMessage.Length >> 16);
            bytes[messagePosition + 3] = (byte)(bytesMessage.Length >> 24);

            Array.Copy(bytesMessage, 0, bytes, messagePosition + 4, bytesMessage.Length);

            var idPosition = messagePosition + 4 + bytesMessage.Length;
            bytes[idPosition] = (byte)(idBytes.Length);
            bytes[idPosition + 1] = (byte)(idBytes.Length >> 8);
            bytes[idPosition + 2] = (byte)(idBytes.Length >> 16);
            bytes[idPosition + 3] = (byte)(idBytes.Length >> 24);

            if (callerContext != null)
            {
                Array.Copy(idBytes, 0, bytes, idPosition + 4, idBytes.Length);

                var typePosition = idPosition + 4 + idBytes.Length;
                bytes[typePosition] = (byte)(typeBytes.Length);
                bytes[typePosition + 1] = (byte)(typeBytes.Length >> 8);
                bytes[typePosition + 2] = (byte)(typeBytes.Length >> 16);
                bytes[typePosition + 3] = (byte)(typeBytes.Length >> 24);

                Array.Copy(typeBytes, 0, bytes, typePosition + 4, typeBytes.Length);
            }
            return bytes;
        }
    }
}
#include "MessageBusFactory.h"
#include <vector>
#include <boost/foreach.hpp>
#include "rapidjson/writer.h"
#include "rapidjson/stringbuffer.h"
#include "../utils/timeutils.hpp"
#include <zlib.h>

using boost::property_tree::ptree;
using namespace rapidjson;

namespace HermEsb
{
	namespace Messages
	{
		MessageBus* MessageBusFactory::CreateMessageBus(Identification* identification, string key, string body)
		{
			MessageBus *msg = new MessageBus();
			msg->Body = body;
			msg->Header.BodyType = key;
			msg->Header.EncodingCodePage = 0;
			msg->Header.CreatedAt = boost::posix_time::microsec_clock::universal_time();
			
			CallerContext cc;
			cc.Identification = *identification;
			msg->Header.CallStack.push(cc);

			return msg;
		}

		int MessageBusFactory::CreateRouterMessage(MessageBus* messageBus, void** messageBuffer , bool useCompression)
		{
			StringBuffer sbMessageBus;
			Writer<StringBuffer> writerBus(sbMessageBus);
			messageBus->Serialize(writerBus);

			void *serializedMessage = (void*) sbMessageBus.GetString();
			int serializedMessageLen = sbMessageBus.Size();
			Bytef *zBuffer = NULL;

			if(useCompression)
			{
				
				z_stream defstream;
				defstream.zalloc = Z_NULL;
				defstream.zfree = Z_NULL;
				defstream.opaque = Z_NULL;
				defstream.avail_in = sbMessageBus.Size(); 
				defstream.next_in = (Bytef *)sbMessageBus.GetString();

				int zBufLen = (int) ceil(1.001 * sbMessageBus.Size()) + 12 + 6; 
				zBuffer = (Bytef *)malloc( zBufLen );

				defstream.avail_out = zBufLen;
				defstream.next_out = zBuffer;
				deflateInit2( &defstream, Z_DEFAULT_COMPRESSION, Z_DEFLATED, (15+16), 8, Z_DEFAULT_STRATEGY);
				int status;
				while( ( status = deflate( &defstream, Z_FINISH ) ) == Z_OK );
				deflateEnd(&defstream);

				serializedMessage = zBuffer;
				serializedMessageLen = defstream.total_out;
			}

			int bufferLen = 15 + messageBus->Header.BodyType.size() + 4 + serializedMessageLen + 4;

			CallerContext c1;
			if(!messageBus->Header.CallStack.empty())
			{
				c1 = messageBus->Header.CallStack.top();
				bufferLen += c1.Identification.Id.size() + 4 + c1.Identification.Type.size();
			}
			 
			void* bufferRet = malloc(bufferLen);
			char* pos = (char*)bufferRet;
			memset(bufferRet, 0, bufferLen);
			memcpy(pos, &(messageBus->Header.Type), sizeof(char));
			pos += 1;
			memcpy(pos, &(messageBus->Header.Priority), sizeof(char));
			pos += 2; //Reservado
			__int64 ticks = HermEsb::Utils::Time::TimeExtensions::ticks_from_mindate(messageBus->Header.CreatedAt);
			
			memcpy(pos, &ticks, sizeof(__int64));
			pos +=sizeof(__int64);
			int bodytypeLen = messageBus->Header.BodyType.size();
			memcpy(pos, &bodytypeLen, sizeof(int));
			pos += sizeof(int);
			memcpy(pos, messageBus->Header.BodyType.c_str(), bodytypeLen);
			pos += bodytypeLen;
			int bodysize = serializedMessageLen;
			memcpy(pos, &bodysize, sizeof(int));
			pos += sizeof(int);
			memcpy(pos, serializedMessage, bodysize);

			if(!c1.Identification.Id.empty())
			{
				pos += bodysize;
				int idLen = c1.Identification.Id.size();
				memcpy(pos, &idLen, sizeof(int));
				pos +=  sizeof(int);
				memcpy(pos, c1.Identification.Id.c_str(), c1.Identification.Id.size());
				pos += c1.Identification.Id.size();
				int typeLen = c1.Identification.Type.size();
				memcpy(pos, &typeLen, sizeof(int));
				pos +=  sizeof(int);
				memcpy(pos, c1.Identification.Type.c_str(), c1.Identification.Type.size());
			}

			if(zBuffer!=NULL)
				free(zBuffer);

			*messageBuffer = bufferRet;
			return bufferLen;
		}
	}
}
using System.Collections.Generic;
using HermEsb.Core.Messages;
using ServiceStack.Text;

namespace HermEsb.Core.Serialization
{
    public static class InstallSerializers
    {
        public static void Install()
        {
            JsConfig<Stack<CallerContext>>.RawDeserializeFn = s =>
            {
                var jsonSerializer = new JsonSerializer<List<CallerContext>>();
                var callerContexts = jsonSerializer.DeserializeFromString(s);
                callerContexts.Reverse();
                return new Stack<CallerContext>(callerContexts);
            };

            JsConfig.DateHandler = JsonDateHandler.ISO8601;
        }
    }
}
using Newtonsoft.Json.Linq;

namespace EmailingService.Extensions
{
    public static class PayloadDeserializer
    {
        public static TValue Get<TValue>(object payload)
        {
            return ((JObject)payload).ToObject<TValue>();
        }
    }
}
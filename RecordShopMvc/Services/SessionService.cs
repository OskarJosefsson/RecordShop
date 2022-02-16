using Newtonsoft.Json;

namespace RecordShopMvc.Services
{
    public class SessionService
    {

        public static void SetObjectAsJson(ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }



        public static T GetObjectAsJson<T>(ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        


    }
}

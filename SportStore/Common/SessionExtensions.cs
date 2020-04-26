using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.WebUi.Common
{
    public static class SessionExtensions
    {
        public static T GetJson<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            return data is null ? default  : JsonConvert.DeserializeObject<T>(data);
        }

        public static void SetJson(this ISession session, string key, object value)
        {           
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
    }
}

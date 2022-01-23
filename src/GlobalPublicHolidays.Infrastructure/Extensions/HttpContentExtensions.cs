﻿using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace GlobalPublicHolidays.Infrastructure.Extensions
{
    public static class HttpContentExtensions
    {

        public static async Task<T> ReadAsAsync<T>(this HttpContent content)
        {
            return await JsonSerializer.DeserializeAsync<T>(await content.ReadAsStreamAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
        }
    }
}

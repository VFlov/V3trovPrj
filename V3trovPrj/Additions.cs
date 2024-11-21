using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Azure.Core.Pipeline;
using V3trovPrj.Models;

namespace V3trovPrj
{
    public static class Additions
    {
       public static string ServerPath = "http://usbeex.ru/";
        public static async Task<Bitmap> ImageDownload(string url)
        {
            if (url.Contains(".svg"))
                return null;
            using (HttpClient httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(ServerPath + url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var byteArray = await response.Content.ReadAsByteArrayAsync();
                    using (var stream = new MemoryStream(byteArray))
                    {
                        return new Bitmap(stream);
                    }
                }
            }
            return null;
        }
        public static List<Software> GetData()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                return httpClient.GetFromJsonAsync<List<Software>>("https://usbeex.ru/output.json").Result;
            }
        }
        
    }
    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using shopapp.Domain.Entities;

namespace shopapp.WebUI.Controllers
{
    public abstract class BaseController : Controller
    {
        private static readonly HttpClient client = new HttpClient();
        protected T GetApiResult<T>(string url)
        {
            var urlBuilder_ = new StringBuilder();
            urlBuilder_.Append("https://localhost:44391").Append(url);
            using (var client_ = new HttpClient())
            {
                var data = client_.GetStringAsync(urlBuilder_.ToString()).Result;

                var model = JsonConvert.DeserializeObject<T>(data);
                return model;
            }
        }

        protected T GetApiResult<T>(string url,int? id)
        {
            var urlBuilder_ = new StringBuilder();
            urlBuilder_.Append("https://localhost:44391").Append(url).Append("?id=").Append(id);
            using (var client_ = new HttpClient())
            {
                var data = client_.GetStringAsync(urlBuilder_.ToString()).Result;

                var model = JsonConvert.DeserializeObject<T>(data);
                return model;
            }
        }

        protected T GetApiResult<T>(string url, string userid)
        {
            var urlBuilder_ = new StringBuilder();
            urlBuilder_.Append("https://localhost:44391").Append(url).Append("?userid=").Append(userid);
            using (var client_ = new HttpClient())
            {
                var data = client_.GetStringAsync(urlBuilder_.ToString()).Result;

                var model = JsonConvert.DeserializeObject<T>(data);
                return model;
            }
        }


        protected async Task Post(string url,string userid)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("https://localhost:44391").Append(url);
            Cart cart = new Cart();
            cart.UserId = userid;
            var content = new StringContent(JsonConvert.SerializeObject(cart),Encoding.UTF8,"application/json");
            var response = await client.PostAsync(urlBuilder_.ToString(), content);

        }
    }
}

using Microsoft.Extensions.Configuration;
using MvcEventosApiAWS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MvcEventosApiAWS.Services
{
    public class ServiceApiEventos
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceApiEventos(IConfiguration configuration)
        {
            this.UrlApi = configuration.GetValue<string>("ApiUrls:ApiEventosAWS");
            this.Header =
            new MediaTypeWithQualityHeaderValue("application/json");
        }
        private async Task<T> CallApi<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                string url = this.UrlApi + request;
                HttpResponseMessage response =
                await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    T data =
                    await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }

        }
        public async Task<List<CategoriaEvento>> GetCategoriasAsync()
        {
            string request = "/api/eventos";
            List<CategoriaEvento> categorias =
            await this.CallApi<List<CategoriaEvento>>(request);
            return categorias;
        }
        public async Task<List<Evento>> GetEventosAsync()
        {
            string request = "/api/eventos/leventos";
            List<Evento> eventos =
            await this.CallApi<List<Evento>>(request);
            return eventos;
        }
        public async Task<List<Evento>> GetEventosCategoriaAsync(int idcategoria)
        {
            string request = "/api/eventos/"+idcategoria;
            List<Evento> eventos =
            await this.CallApi<List<Evento>>(request);
            return eventos;
        }
        public async Task InsertEventoAsync(Evento evento)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/eventos";

                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                string json = JsonConvert.SerializeObject(evento);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
            }
        }
        public async Task DeleteEvento(string idEvento)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/eventos/" + idEvento;

                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.DeleteAsync(request);
            }
        }
        public async Task<Evento> GetEventoAsync(string idevento)
        {
            string request = "api/eventos/find/" + idevento;
            Evento evento = await this.CallApi<Evento>(request);
            return evento;
        }
        public async Task UpdateEventoAsync(Evento evento)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/eventos";

                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                string json = JsonConvert.SerializeObject(evento);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(request, content);
            }
        }

    }
}

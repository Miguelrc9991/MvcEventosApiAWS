using Microsoft.AspNetCore.Mvc;
using MvcEventosApiAWS.Models;
using MvcEventosApiAWS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcEventosApiAWS.Controllers
{
    public class EventosController : Controller
    {
        private ServiceApiEventos service;
        public EventosController(ServiceApiEventos service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
            List<Evento> eventos =
                await this.service.GetEventosAsync();
            return View(eventos);
        }
        public async Task<IActionResult> GetCategorias()
        {
            List<CategoriaEvento> categorias = await  this.service.GetCategoriasAsync();
            return View(categorias);
        }
     
        public async Task<IActionResult> GetEventosCategoriaAsync(int idcategoria)
        {
            List<Evento> eventos = await this.service.GetEventosCategoriaAsync(idcategoria);
            return View(eventos);
        }
        public IActionResult NuevoEvento()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NuevoEvento(Evento evento)
        {
            await this.service.InsertEventoAsync(evento);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(string idevento)
        {
            await this.service.DeleteEvento(idevento);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> UpdateEvento(string idevento)
        {
            Evento evento = await this.service.GetEventoAsync(idevento);
            List<CategoriaEvento> categorias = await this.service.GetCategoriasAsync();
            ViewData["CATEGORIAS"] = categorias;
            return View(evento);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEvento(string idevento,string nombre,string artista,int idcategoria)
        {
            Evento evento = await this.service.GetEventoAsync(idevento);
            evento.IdCategoria = idcategoria;
            await this.service.UpdateEventoAsync(evento);
            return RedirectToAction("Index");

        }
    }
}

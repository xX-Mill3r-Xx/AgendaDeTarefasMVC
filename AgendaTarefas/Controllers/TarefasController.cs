using AgendaTarefas.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaTarefas.Controllers
{
    public class TarefasController : Controller
    {
        private readonly Contexto _contexto;

        public TarefasController(Contexto contexto)
        {
            _contexto = contexto;
        }

        public IActionResult Index()
        {
            return View(PegarDatas());
        }

        private List<DatasViewModel> PegarDatas()
        {
            DateTime dataAtual = DateTime.Now;
            DateTime dataLimite = DateTime.Now.AddDays(10);
            int qtdDias = 0;
            DatasViewModel datas;
            List<DatasViewModel> listaDatas = new List<DatasViewModel>();

            while(dataAtual < dataLimite)
            {
                datas = new DatasViewModel();
                datas.Data = dataAtual.ToShortDateString();
                datas.Identificadores = "collapse" + dataAtual.ToShortDateString().Replace("/", "");
                listaDatas.Add(datas);
                qtdDias = qtdDias + 1;
                dataAtual = DateTime.Now.AddDays(qtdDias);
            }
            return listaDatas;
        }

        [HttpGet]
        public IActionResult CriarTarefas(string dataTarefa)
        {
            Tarefa tarefa = new Tarefa
            {
                Data = dataTarefa
            };
            return View(tarefa);  
        }

        [HttpPost]
        public async Task<IActionResult> CriarTarefa(Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                _contexto.Tarefas.Add(tarefa);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(tarefa);
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarTarefa(int tarefaId)
        {
            Tarefa tarefa = await _contexto.Tarefas.FindAsync(tarefaId);

            if(tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarTarefa(Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                _contexto.Update(tarefa);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(tarefa);
        }
    }
}

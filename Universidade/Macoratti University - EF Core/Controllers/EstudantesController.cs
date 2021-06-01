using AutoMapper;
using Universidade.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Universidade.Infrastructure;
using System.Linq.Expressions;
using Universidade.Core.Entidades;
using Universidade.Utility;

namespace Universidade.Controllers
{
    public class EstudantesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EstudantesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string ordem, string filtro)
        {
            ViewData["NomeParm"] = String.IsNullOrEmpty(ordem) ? "nome_desc" : "";
            ViewData["DataParm"] = ordem == "Data" ? "data_desc" : "Data";
            ViewData["Filtro"] = filtro;

            var estudantes = await _unitOfWork.Estudantes.Listar();

            //filtro
            if (!String.IsNullOrEmpty(filtro))
            {
                estudantes = estudantes.Where(s => 
                    s.SobreNome.ToUpper().Contains(filtro.ToUpper()) 
                    || s.Nome.ToUpper().Contains(filtro.ToUpper())
                );
            }

            //ordenação
            estudantes = ordem switch
            {
                "nome_desc" => estudantes.OrderByDescending(est => est.SobreNome),
                "Data" => estudantes.OrderBy(est => est.DataCriacao),
                "data_desc" => estudantes.OrderByDescending(est => est.DataCriacao),
                _ => estudantes.OrderBy(est => est.SobreNome),
            };

            return View(estudantes.Select(x => _mapper.Map<EstudanteViewModel>(x)));
        }

        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id == null)
                return NotFound();

            var estudante = await _unitOfWork.Estudantes.BuscarEstudanteEMatriculasPorEstudanteId(id.Value);
            
            if (estudante == null)
                return NotFound();

            return View(_mapper.Map<EstudanteViewModel>(estudante));
        }

        public IActionResult Inserir()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Inserir([Bind("SobreNome, Nome, DataCriacao")] EstudanteViewModel estudante)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var lEstudante = _mapper.Map<Estudante>(estudante);
                    lEstudante.Validar();

                    if (lEstudante.MensagensErro.Any())
                    {
                        this.AdicionarMensagemDeErro(String.Join(",", lEstudante.MensagensErro));
                        return View(estudante);
                    }

                    _unitOfWork.Estudantes.Adicionar(lEstudante);
                    _unitOfWork.Complete();

                    this.AdicionarMensagemDeSucesso("Estudante cadastrado com sucesso!");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(Exception ex)
            {
                this.AdicionarMensagemDeErro("Não foi possível salvar. Tente novamente, e se o problema persistir chame o suporte.");
            }

            return View(estudante);
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
                return NotFound();

            var lEstudante = await _unitOfWork.Estudantes.BuscarPorId(id.Value);
            
            if (lEstudante == null)
                return NotFound();

            return View(_mapper.Map<EstudanteViewModel>(lEstudante));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmarEdicao(int id, [Bind("EstudanteID, Nome, SobreNome, DataCriacao")] EstudanteViewModel estudante)
        {
            if (id == estudante.EstudanteID)
                return NotFound();

            try
            {
                if (ModelState.IsValid)
                {
                    var lEstudante = _mapper.Map<Estudante>(estudante);
                    lEstudante.Validar();

                    if (lEstudante.MensagensErro.Any())
                    {
                        this.AdicionarMensagemDeErro(String.Join(",", lEstudante.MensagensErro));
                        return View(nameof(Editar), estudante);
                    }

                    _unitOfWork.Estudantes.Editar(lEstudante);
                    _unitOfWork.Complete();

                    this.AdicionarMensagemDeSucesso("Estudante editado com sucesso!");
                    return RedirectToAction("Index");
                }

                return View(estudante);
            }
            catch (Exception ex)
            {
                this.AdicionarMensagemDeErro("Não foi possível salvar. Tente novamente, e se o problema persistir chame o suporte.");
            }

            return View(nameof(Editar), estudante);
        }

        public async Task<IActionResult> Excluir(int? id, bool? saveChangesError = false)
        {
            if (id == null)
                return NotFound();
            Estudante lEstudante;

            try
            {
                lEstudante = await _unitOfWork.Estudantes.BuscarPorId(id.Value);

                if (lEstudante == null)
                    return NotFound();

                if (saveChangesError.GetValueOrDefault())
                    this.AdicionarMensagemDeErro("A exclusão falhou. Tente novamente e se o problema persistir contate o suporte.");
            }
            catch(Exception ex)
            {
                this.AdicionarMensagemDeErro("A exclusão falhou. Tente novamente e se o problema persistir contate o suporte.");
                return RedirectToAction(nameof(Index));
            }
            
            return View(_mapper.Map<EstudanteViewModel>(lEstudante));
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarExclusao(int id)
        {
            var estudante = await _unitOfWork.Estudantes.BuscarPorId(id);

            if (estudante == null)
                return RedirectToAction("Index");
            
            try
            {
                _unitOfWork.Estudantes.Deletar(id);
                _unitOfWork.Complete();

                this.AdicionarMensagemDeSucesso("Estudante removido com sucesso!");
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Excluir), new { id = id, saveChangesError = true });
            }
        }

        private bool EstudanteExists(int id)
        {
            return _unitOfWork.Estudantes.Listar(e => e.EstudanteID == id).Result.Count() > 0;
        }
    }
}

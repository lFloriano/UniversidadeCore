using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Universidade.Models;
using AutoMapper;
using Universidade.Infrastructure;
using Universidade.Core.Entidades;
using Universidade.Utility;

namespace Universidade.Controllers
{
    public class DepartamentosController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartamentosController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var lDepartamentos = await _unitOfWork.Departamentos.Listar();

            return View(lDepartamentos.Select(x => _mapper.Map<DepartamentoViewModel>(x)));
        }

        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id == null)
                return NotFound();

            var departamento = await _unitOfWork.Departamentos.BuscarPorId(id.Value);

            if (departamento == null)
                return NotFound();

            return View(_mapper.Map<DepartamentoViewModel>(departamento));
        }

        public IActionResult Inserir()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Inserir([Bind("DepartamentoID,Nome,Supervisor")] DepartamentoViewModel departamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var lDepartamento = _mapper.Map<Departamento>(departamento);
                    lDepartamento.Validar();

                    if (lDepartamento.MensagensErro.Any())
                    {
                        this.AdicionarMensagemDeErro(String.Join(",", lDepartamento.MensagensErro));
                        return View(departamento);
                    }

                    _unitOfWork.Departamentos.Adicionar(lDepartamento);
                    _unitOfWork.Complete();

                    this.AdicionarMensagemDeSucesso("Departamento cadastrado com sucesso!");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Nome", ex.Message);
            }
            
            return View(departamento);
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null) 
                return NotFound();

            var lDepartamento = await _unitOfWork.Departamentos.BuscarPorId(id.Value);
            
            if (lDepartamento == null) 
                return NotFound();
            
            return View(_mapper.Map<DepartamentoViewModel>(lDepartamento));
        }

        public IActionResult ConfirmarEdicao([Bind("DepartamentoID, Nome, Supervisor")] DepartamentoViewModel departamento)
        {
            if (departamento == null)
                return NotFound();

            try
            {
                if (ModelState.IsValid)
                {
                    var lDepartamento = _mapper.Map<Departamento>(departamento);
                    lDepartamento.Validar();

                    if (lDepartamento.MensagensErro.Any())
                    {
                        this.AdicionarMensagemDeErro(String.Join(",", lDepartamento.MensagensErro));
                        return View(nameof(Editar), departamento);
                    }

                    _unitOfWork.Departamentos.Editar(lDepartamento);
                    _unitOfWork.Complete();

                    this.AdicionarMensagemDeSucesso("Departamento editado com sucesso!");

                    return RedirectToAction("Index");
                }

                return View(nameof(Editar), departamento);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Não foi possível salvar. Tente novamente, e se o problema persistir chame o suporte.");                
            }

            return View(nameof(Editar), departamento);
        }

        public async Task<IActionResult> Excluir(int? id, bool? saveChangesError = false)
        {
            if (id == null) 
                return NotFound();

            var lDepartamento = await _unitOfWork.Departamentos.BuscarPorId(id.Value);
            
            if (lDepartamento == null) 
                return NotFound();

            if (saveChangesError.GetValueOrDefault())
                this.AdicionarMensagemDeErro("A exclusão falhou. Tente novamente e se o problema persistir contate o suporte.");

            return View(_mapper.Map<DepartamentoViewModel>(lDepartamento));
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmarExclusao(int id)
        {
            try
            {
                _unitOfWork.Departamentos.Deletar(id);
                _unitOfWork.Complete();

                this.AdicionarMensagemDeSucesso("Departamento removido com sucesso!");
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                return RedirectToAction(nameof(Excluir), new {id = id, saveChangesError = true });
            }
        }

        private bool DepartamentoExiste(int id)
        {
            return _unitOfWork.Departamentos.Listar(x => x.DepartamentoID == id).Result.Count() > 0;
        }
    }
}

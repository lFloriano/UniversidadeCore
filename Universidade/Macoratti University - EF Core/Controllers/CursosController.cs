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
    public class CursosController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CursosController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var lCursos = await _unitOfWork.Cursos.ListarCursosDepartamentoEMatriculas();

            return View(lCursos.Select(x => _mapper.Map<CursoViewModel>(x)));
        }

        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id == null)
                return NotFound();

            var lCurso = await _unitOfWork.Cursos.BuscarCursoDepartamentoEMatriculasPorCursoId(id.Value);

            if (lCurso == null)
                return NotFound();

            var lCursoViewModel = _mapper.Map<CursoViewModel>(lCurso);

            return View(lCursoViewModel);
        }

        public IActionResult Inserir()
        {
            ViewBag.SelectListDepartamentos = MontarSelectListDepartamentos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Inserir([Bind("CursoID,Titulo,Creditos,DepartamentoID,LotacaoAlunos")] CursoViewModel curso)
        {
            if (curso == null)
                return NotFound();

            try
            {
                if (ModelState.IsValid)
                {
                    var lCurso = _mapper.Map<Curso>(curso);
                    lCurso.Validar();

                    if (lCurso.MensagensErro.Any())
                    {
                        this.AdicionarMensagemDeErro(String.Format(", ", lCurso.MensagensErro));
                        return View(lCurso);
                    }

                    _unitOfWork.Cursos.Adicionar(lCurso);
                    _unitOfWork.Complete();

                    this.AdicionarMensagemDeSucesso("Curso inserido com sucesso!");
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction(nameof(Inserir));
            }
            catch (Exception)
            {
                this.AdicionarMensagemDeErro("Não foi possível salvar. Tente novamente, e se o problema persistir chame o suporte.");
                return RedirectToAction(nameof(Inserir));
            }
        }
        
        public async Task<IActionResult> Editar(int? id)
        {
            Curso lCurso = null;

            if (id == null)
                return NotFound();

            try
            {
                lCurso = await _unitOfWork.Cursos.BuscarCursoDepartamentoEMatriculasPorCursoId(id.Value);
                ViewBag.SelectListDepartamentos = MontarSelectListDepartamentos(lCurso);

                if (lCurso == null)
                    return NotFound();
            }
            catch(Exception e)
            {
                this.AdicionarMensagemDeErro("Não foi possível salvar. Tente novamente, e se o problema persistir chame o suporte.");
            }

            return View(_mapper.Map<CursoViewModel>(lCurso));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmarEdicao([Bind("CursoID,Titulo,Creditos,DepartamentoID,LotacaoAlunos")] CursoViewModel curso, string idsEstudantesMatriculados)
        {
            if (curso == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var lCurso = _mapper.Map<Curso>(curso);
                    lCurso.Validar();

                    if (lCurso.MensagensErro.Any())
                    {
                        this.AdicionarMensagemDeErro(String.Join(", ", lCurso.MensagensErro));
                        return View(nameof(Editar), curso);
                    }

                    _unitOfWork.Cursos.Editar(lCurso);
                    _unitOfWork.Complete();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    this.AdicionarMensagemDeErro("Não foi possível salvar. Tente novamente, e se o problema persistir chame o suporte.");
                    return RedirectToAction(nameof(Editar), new { id = curso.CursoID });
                }
            }

            return View(nameof(Editar), curso);
        }

        public async Task<IActionResult> Excluir(int? id, bool? saveChangesError = false)
        {
            if (id == null)
                return NotFound();
            CursoViewModel lCursoViewModel;

            try
            {
                var lCurso = await _unitOfWork.Cursos.BuscarCursoDepartamentoEMatriculasPorCursoId(id.Value);

                if (lCurso == null)
                    return NotFound();

                if (saveChangesError.GetValueOrDefault())
                    this.AdicionarMensagemDeErro("A exclusão falhou. Tente novamente e se o problema persistir contate o suporte.");

                lCursoViewModel = _mapper.Map<CursoViewModel>(lCurso);
            }
            catch(Exception e)
            {
                this.AdicionarMensagemDeErro("Não foi possível executar a operação. Tente novamente, e se o problema persistir chame o suporte.");
                return RedirectToAction(nameof(Index));
            }

            return View(lCursoViewModel);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmarExclusao(int id)
        {
            try
            {
                _unitOfWork.Cursos.Deletar(id);
                _unitOfWork.Complete();
            }
            catch(Exception e)
            {
                this.AdicionarMensagemDeErro("Não foi possível executar a operação. Tente novamente, e se o problema persistir chame o suporte.");
                return RedirectToAction(nameof(Excluir), new { id = id, saveChangesError = true });
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CursoExiste(int id)
        {
            return _unitOfWork.Cursos.Listar(x => x.CursoID == id).Result.Count() > 0;
        }

        private List<SelectListItem> MontarSelectListDepartamentos(Curso curso = null)
        {
            var lSelectListDepartamentos = new List<SelectListItem>();
            var lDepartamentos = _unitOfWork.Departamentos.Listar().Result;

            lSelectListDepartamentos = lDepartamentos.Select(x =>
                new SelectListItem()
                {
                    Text = x.Nome,
                    Value = x.DepartamentoID.ToString(),
                    Selected = (curso != null && curso.DepartamentoID == x.DepartamentoID)
                }
            ).ToList();

            return lSelectListDepartamentos;
        }
    }
}

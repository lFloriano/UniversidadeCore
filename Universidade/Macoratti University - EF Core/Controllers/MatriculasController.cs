using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Universidade.Core.Entidades;
using Universidade.Infrastructure;
using Universidade.Infrastructure.Data;
using Universidade.Models;
using Universidade.Utility;

namespace Universidade.Controllers
{
    public class MatriculasController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public MatriculasController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var lCursos = await _unitOfWork.Cursos.ListarCursosDepartamentoEMatriculas();
                return View(lCursos.Select(x => _mapper.Map<CursoViewModel>(x)));
            }
            catch (Exception e)
            {
                this.AdicionarMensagemDeErro("Erro ao buscas matrículas. Tente novamente");
                return View(new List<CursoViewModel>());
            }
        }

        public async Task<IActionResult> Detalhes(int? id)
        {
            try
            {
                if (id == null)
                    return NotFound();

                var lCurso = await _unitOfWork.Cursos.BuscarCursoDepartamentoEMatriculasPorCursoId(id.Value);

                if (lCurso == null)
                    return NotFound();

                var lGerenciarMatriculasViewModel = new GerenciarMatriculasViewModel()
                {
                    Curso = _mapper.Map<CursoViewModel>(lCurso),
                    Matriculas = lCurso.Matriculas.Select(x => _mapper.Map<MatriculaViewModel>(x)).ToList()
                };

                return View(nameof(Detalhes), lGerenciarMatriculasViewModel);
            }
            catch (Exception e)
            {
                this.AdicionarMensagemDeErro("Erro ao carregar matriculas do curso. Tente novamente");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GerenciarMatriculas(int? id)
        {
            try
            {
                if (id == null)
                    return NotFound();

                var lCurso = await _unitOfWork.Cursos.BuscarCursoDepartamentoEMatriculasPorCursoId(id.Value);

                if (lCurso == null)
                    return NotFound();

                ViewBag.SelectListEstudantes = MontarSelectListEstudantes(lCurso);
                ViewBag.IdsEstudantesMatriculados = String.Join(";", lCurso.Matriculas.Select(x => x.EstudanteID));
                var lGerenciarMatriculasViewModel = new GerenciarMatriculasViewModel()
                {
                    Curso = _mapper.Map<CursoViewModel>(lCurso),
                    Matriculas = lCurso.Matriculas.Select(x => _mapper.Map<MatriculaViewModel>(x)).ToList()
                };

                return View(nameof(GerenciarMatriculas), lGerenciarMatriculasViewModel);
            }
            catch(Exception e)
            {
                this.AdicionarMensagemDeErro("Erro ao carregar matriculas do curso. Tente novamente");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> GerenciarMatriculas(GerenciarMatriculasViewModel model, string idsEstudantesMatriculados)
        {
            try
            {
                if (model.Curso == null || model.Curso.CursoID < 1)
                {
                    this.AdicionarMensagemDeErro("Erro. Curso não informado");
                    return RedirectToAction(nameof(Index));
                }

                idsEstudantesMatriculados = idsEstudantesMatriculados ?? "";
                ViewBag.IdsEstudantesMatriculados = idsEstudantesMatriculados;
                var idsEstudantesMatriculadosArray = idsEstudantesMatriculados.Split(";", StringSplitOptions.RemoveEmptyEntries).Select(x=> Convert.ToInt32(x));

                var lCurso = await _unitOfWork.Cursos.BuscarCursoDepartamentoEMatriculasPorCursoId(model.Curso.CursoID);
                if (lCurso == null)
                {
                    this.AdicionarMensagemDeErro("Erro. Curso não encontrado");
                    return RedirectToAction(nameof(Index));
                }

                if (lCurso.Matriculas != null)
                {
                    foreach (var matricula in lCurso.Matriculas.Where(x => !idsEstudantesMatriculadosArray.Contains(x.EstudanteID)))
                    {
                        _unitOfWork.Matriculas.Deletar(matricula.MatriculaID);
                    }
                }

                foreach (var lIdEstudante in idsEstudantesMatriculadosArray)
                {
                    var lMatricula = new MatriculaViewModel()
                    {
                        Curso = null,
                        CursoID = lCurso.CursoID,
                        Estudante = null,
                        EstudanteID = lIdEstudante,
                        Nota = null
                    };

                    if (!lCurso.Matriculas.Any(x => x.EstudanteID == lIdEstudante))
                    {
                        _unitOfWork.Matriculas.Adicionar(_mapper.Map<Matricula>(lMatricula));
                    }
                }

                _unitOfWork.Complete();
                this.AdicionarMensagemDeSucesso("Matrículas modificadas com sucesso!");
            }
            catch(Exception e)
            {
                this.AdicionarMensagemDeErro("Não foi possível salvar. Tente novamente, e se o problema persistir chame o suporte.");
            }

            return RedirectToAction(nameof(Index));
        }

        public List<SelectListItem> MontarSelectListEstudantes(Curso curso)
        {
            var lSelectListEstudantes = new List<SelectListItem>();

            try
            {
                var lEstudantes = _unitOfWork.Estudantes.Listar().Result;
                lSelectListEstudantes.AddRange(lEstudantes.Select(x => 
                    new SelectListItem()
                    {
                        Text = $"{x.Nome} {x.SobreNome}",
                        Value = x.EstudanteID.ToString(),
                        Selected = curso.Matriculas != null && curso.Matriculas.Any(y => y.Estudante.EstudanteID == x.EstudanteID)
                    })
                );
            }
            catch(Exception e)
            {
                this.AdicionarMensagemDeErro("Erro ao buscar estudantes. Tente novamente");
                throw;
            }

            return lSelectListEstudantes;
        }

        private bool MatriculaExiste(int id)
        {
            return (_unitOfWork.Matriculas.Listar(x => x.MatriculaID == id).Result.Count() > 0);
        }
    }
}

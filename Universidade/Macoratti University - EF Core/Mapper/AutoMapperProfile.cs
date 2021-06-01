using AutoMapper;
using Universidade.Core.Entidades;
using Universidade.Models;

namespace Universidade.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Estudante, EstudanteViewModel>().ReverseMap();
            CreateMap<Matricula, MatriculaViewModel>().ReverseMap();
            CreateMap<Curso, CursoViewModel>().ReverseMap();
            CreateMap<Departamento, DepartamentoViewModel>().ReverseMap();
        }
    }
}

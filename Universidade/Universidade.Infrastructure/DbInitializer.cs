using System;
using Universidade.Core.Entidades;
using Universidade.Infrastructure.Data;

namespace Universidade.Infrastructure
{
    public class DbInitializer
    {
        public static void Initialize(UniversidadeContext context)
        {
            context.Database.EnsureCreated();

            // procura por qualquer estudante
            //if (context.Estudantes.Any())
            //{
            //    return;  //O banco foi inicializado
            //}

            //var estudantes = new Estudante[]
            //{
            //    new Estudante{Nome="Carlos",SobreNome="Silveira",DataMatricula=DateTime.Parse("2015-09-01")},
            //    new Estudante{Nome="Maria",SobreNome="Alonso",DataMatricula=DateTime.Parse("2012-09-01")},
            //    new Estudante{Nome="Bianca",SobreNome="Almeida",DataMatricula=DateTime.Parse("2013-09-01")},
            //    new Estudante{Nome="Jose Carlos",SobreNome="Siqueira",DataMatricula=DateTime.Parse("2012-09-01")},
            //    new Estudante{Nome="Yuri",SobreNome="Silva",DataMatricula=DateTime.Parse("2012-09-01")},
            //    new Estudante{Nome="Mario",SobreNome="Domingues",DataMatricula=DateTime.Parse("2011-09-01")},
            //    new Estudante{Nome="Laura",SobreNome="Santos",DataMatricula=DateTime.Parse("2013-09-01")},
            //    new Estudante{Nome="Jefferson",SobreNome="Bueno",DataMatricula=DateTime.Parse("2015-09-01")}
            //};

            ////adiciona os estudoantes criados ao contexto e salva no banco
            //foreach (Estudante s in estudantes)
            //{
            //    context.Estudantes.Add(s);
            //}

            //var departamentos = new Departamento[]
            //{
            //    new Departamento{Nome="Tecnologia da Informação", Supervisor="Carlos Marrom"},
            //    new Departamento{Nome="Matemática e Estatística", Supervisor="Rosana Violeta"},
            //    new Departamento{Nome="Ciências Biológicas", Supervisor="Marta Jasmim"},
            //    new Departamento{Nome="Comunicação e Linguagens", Supervisor="Eucides Carmesim"},
            //};

            //foreach (var departamento in departamentos)
            //{
            //    context.Departamentos.Add(departamento);
            //}
            //context.SaveChanges();

            //context.SaveChanges();

            //var cursos = new Curso[]
            //{
            //    new Curso{CursoID=1050, Titulo="Quimica", Creditos=3, DepartamentoID=3},
            //    new Curso{CursoID=4022, Titulo="Economia", Creditos=3, DepartamentoID=2},
            //    new Curso{CursoID=4041, Titulo="Ecologia", Creditos=3, DepartamentoID=3},
            //    new Curso{CursoID=1045, Titulo="Cálculo", Creditos=4, DepartamentoID=2},
            //    new Curso{CursoID=3141, Titulo="Trigonometria", Creditos=4, DepartamentoID=2},
            //    new Curso{CursoID=2021, Titulo="Engenharia de Software", Creditos=3, DepartamentoID=1},
            //    new Curso{CursoID=2042, Titulo="Literatura", Creditos=4, DepartamentoID=4}
            //};

            //foreach (Curso c in cursos)
            //{
            //    context.Cursos.Add(c);
            //}
            //context.SaveChanges();

            //var matriculas = new Matricula[]
            //{
            //new Matricula{EstudanteID=1,CursoID=1050,Nota=Nota.A},
            //new Matricula{EstudanteID=1,CursoID=4022,Nota=Nota.C},
            //new Matricula{EstudanteID=1,CursoID=4041,Nota=Nota.B},
            //new Matricula{EstudanteID=2,CursoID=1045,Nota=Nota.B},
            //new Matricula{EstudanteID=2,CursoID=3141,Nota=Nota.F},
            //new Matricula{EstudanteID=2,CursoID=2021,Nota=Nota.F},
            //new Matricula{EstudanteID=3,CursoID=1050},
            //new Matricula{EstudanteID=4,CursoID=1050,},
            //new Matricula{EstudanteID=4,CursoID=4022,Nota=Nota.F},
            //new Matricula{EstudanteID=5,CursoID=4041,Nota=Nota.C},
            //new Matricula{EstudanteID=6,CursoID=1045},
            //new Matricula{EstudanteID=7,CursoID=3141,Nota=Nota.A},
            //};
            //foreach (Matricula e in matriculas)
            //{
            //    context.Matriculas.Add(e);
            //}
            //context.SaveChanges();
        }
    }
}

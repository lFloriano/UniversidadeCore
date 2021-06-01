using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Universidade.Utility
{
    public static class ControllerExtension
    {
        public static void AdicionarMensagemDeErro(this Controller controller, string mensagem)
        {
            controller.TempData["MessageError"] = mensagem;
        }

        public static void AdicionarMensagemDeSucesso(this Controller controller, string mensagem)
        {
            controller.TempData["MessageSuccess"] = mensagem;
        }

        public static void AdicionarMensagemDeAlerta(this Controller controller, string mensagem)
        {
            controller.TempData["MessageAlert"] = mensagem;
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace CargasBrasilDB.Util
{
    public class ResponseApi
    {
        public static JsonResult SucesseResponse(string mensagem)
        {
            var retorno = new
            {
                Sucesso = true,
                Mensagem = mensagem
            };

            return new JsonResult(retorno);

        }


        public static JsonResult FailedResponse(string mensagem)
        {
            var retorno = new
            {
                Sucesso = false,
                Mensagem = mensagem
            };

            return new JsonResult(retorno);

        }

        public static JsonResult SucesseResponseObject(object data, string mensagem)
        {
            var retorno = new
            {
                Sucesso = true,
                Mensagem = mensagem,
                Data = data

            };

            return new JsonResult(retorno);

        }
    }
}

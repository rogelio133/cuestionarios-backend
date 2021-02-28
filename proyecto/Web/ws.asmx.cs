using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Web
{
    /// <summary>
    /// Descripción breve de ws
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
     [System.Web.Script.Services.ScriptService]
    public class ws : System.Web.Services.WebService
    {

        [WebMethod]
        public Response ValidateLogin(string user, string password)
        {
            Response response = new Response();
            try
            {
                if(user == "q@a.com" && password=="123")
                {
                    response.Success = true;
                    response.Data = new
                    {
                        name = "Rogelio 133",
                        lastName = "Pacheco Elorza",
                        token = "123456789",
                    };
                }
                else
                {
                    response.Success = false;
                    response.Message = "Datos incorrectos";
                   
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "No se pudo procesar su solicitud";

            }

            return response;
        }

        [WebMethod]
        public object GetQuestionnaire(string token, int ID)
        {
            Response response = new Response();
            try
            {
                response.TokenOK = isValidToken(token);
                if (response.TokenOK)
                {
                    Questionnaire questionnaire = null;
                    if(questionnaire != null)
                    {
                        response.Success = true;
                        response.Data = new
                        {
                            questionnaireID = 515//questionnaire.ID
                        };
                    }
                    
                }

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "No se pudo procesar su solicitud";
            }

            return response;
        }
        [WebMethod]
        public object SaveQuestionnaire(string token, Questionnaire questionnaire)
        {
            Response response = new Response();
            try
            {
                response.TokenOK = isValidToken(token);
                if (response.TokenOK)
                {
                    //guardar cuestionario
                    response.Data = new
                    {
                        questionnaireID = 515//questionnaire.ID
                    };
                }

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "No se pudo procesar su solicitud";
            }

            return response;
        }

        private bool isValidToken( string token)
        {
            return token == "123456789";

        }


    }
}

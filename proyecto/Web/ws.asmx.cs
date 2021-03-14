using Business;
using Entities;
using Entities.custom;
using Entities.response;
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
                User userLogged = UserBusiness.ValidateLogin(user, password);

                if (userLogged == null)
                    response.Message = "Datos incorrectos";
                else if (userLogged.IDStatus == (int)Enums.UserStatus.Disabled)
                    response.Message = "Su cuenta esta inactiva";
                else
                {
                    response.Success = true;
                    response.Data = new
                    {
                        name = userLogged.Name,
                        token = userLogged.Token,
                    };
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
        public Response GetQuestionnaire(string token, string code)
        {
            Response response = new Response();
            try
            {
                response.TokenOK = UserBusiness.isValidToken(token);
                if (response.TokenOK)
                {
                    questionnaire item = QuestionnaireBusiness.getQuestionnaire(token,code);
                    if (item != null)
                    {
                        response.Success = true;
                        response.Data = item;
                    }

                }

                response.Success = true;
            }
            catch(ApplicationException aex)
            {
                response.Success = false;
                response.Message = aex.Message;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "No se pudo procesar su solicitud. Intente nuevamente";
            }

            return response;
        }

        [WebMethod]
        public Response GetQuestionnaires(string token)
        {
            Response response = new Response();
            try
            {
                response.TokenOK = UserBusiness.isValidToken(token);
                if (response.TokenOK)
                {
                    List<questionnaire> items = QuestionnaireBusiness.getQuestionnaires(token);

                    response.Success = true;
                    response.Data = items;
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
                response.TokenOK = UserBusiness.isValidToken(token);
                if (response.TokenOK)
                {
                    QuestionnaireBusiness.Save(questionnaire, token);
                    response.Data = new
                    {
                        questionnaireID = questionnaire.IDQuestionnaire
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

        [WebMethod]
        public object SaveQuestionnaireAnswer(string code,QuestionnaireAnswer exam)
        {
            Response response = new Response();
            try
            {
                response.Data = QuestionnaireBusiness.SaveQuestionnaireAnswer(code, exam);
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
        public Response ValidateCode(string code)
        {
            Response response = new Response();
            try
            {
                questionnaire data = QuestionnaireBusiness.ValidateCode(code);

                if(data != null)
                {
                    response.Data = data;
                    response.Success = true;
                }
                else
                {
                    response.Message = "Código incorrecto";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "No se pudo procesar su solicitud";
            }

            return response;
        }

        


    }
}

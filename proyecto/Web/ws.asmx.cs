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
        public object ValidateLogin(string user, string password)
        {
            object response = null;
            try
            {
                if(user == "q@a.com" && password=="123")
                {
                    response = new
                    {
                        success = true,
                        data = new  {
                            name = "Rogelio 133",
                            lastName = "Pacheco Elorza",
                            token = "123456789",
                        },
                        
                        message = "Datos incorrectos"
                    };
                }
                else
                {
                    response = new
                    {
                        success = false,
                        message = "Datos incorrectos"
                    };
                }

               
            }
            catch (Exception ex)
            {
                response = new {
                    success = false,
                    message = "No se pudo procesar su solicitud"
                };
                 
            }

            return response;
        }
    }
}

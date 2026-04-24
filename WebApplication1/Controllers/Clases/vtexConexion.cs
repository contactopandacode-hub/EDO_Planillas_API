using System.IO;
using System.Net;
using System.Text;
using System;

namespace ServicioRSNetCore.Controllers.Clases
{
    public class vtexConexion
    {
        public static string EcommerceInvoca(string str_json, string str_DireccionUrl, string str_AppToken, string str_AppKey, string str_Metodo)
        {
            HttpWebRequest obj_Request = null;
            byte[] obj_FileByte = null;
            string str_Resultado = string.Empty;

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                obj_Request = (HttpWebRequest)WebRequest.Create(string.Format("{0}", str_DireccionUrl));
                obj_Request.Method = str_Metodo;
                obj_Request.ContentType = "application/json";
                obj_Request.Headers.Add("X-VTEX-API-AppToken", str_AppToken);
                obj_Request.Headers.Add("X-VTEX-API-AppKey", str_AppKey);

                if (str_Metodo != "GET" && str_Metodo != "DELETE")
                {
                    obj_FileByte = Encoding.UTF8.GetBytes(str_json);
                    obj_Request.ContentLength = obj_FileByte.Length;

                    using (Stream obj_RequestStream = obj_Request.GetRequestStream())
                    {
                        obj_RequestStream.Write(obj_FileByte, 0, obj_FileByte.Length);
                    }
                }

                // Invoke REST Service
                using (HttpWebResponse obj_Response = (HttpWebResponse)obj_Request.GetResponse())
                {
                    using (Stream obj_ResponseStream = obj_Response.GetResponseStream())
                    {
                        using (StreamReader obj_Reader = new StreamReader(obj_ResponseStream))
                        {
                            str_Resultado = "00|" + obj_Reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException e)
            {
                using (Stream stream = e.Response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        str_Resultado = "01|" + reader.ReadToEnd();
                        // if (str_Metodo == "PUT")
                        // {
                        //     str_Resultado = "01|No se encontró IdProducto / IdSku.";
                        // }
                    }
                }
            }
            catch (Exception ex)
            {
                str_Resultado = "02|" + ex.Message;
            }
            finally
            {
                // Perform cleanup tasks if necessary
            }

            return str_Resultado;
        }
    }
}

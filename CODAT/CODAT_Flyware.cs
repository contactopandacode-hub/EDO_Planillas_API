using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using COBE;
using System.Data.Common;
using System.Data.SqlClient;

namespace CODAT
{
  
  public  class CODAT_Flyware:DbContext
    {
        public DbContext context { get; set; }
        private readonly IConfiguration configuration;
        public CODAT_Flyware(IConfiguration _configuration, DbContext _context)
        {
            configuration = _configuration;
            this.context = _context;
        }

        public COBEc_Error Registrar(COBEc_FlywirePost obj_datos)
        {
            string str_return = string.Empty;
            string str_callback_id = string.Empty;
            string str_compania = string.Empty;
            string str_TipoDocumento = string.Empty;
            string str_NumeroDocumento = string.Empty;
            COBEc_Error oBEc_Error = new COBEc_Error();
            try
            {
                str_callback_id = Eramake.eCryptography.Decrypt(obj_datos.callback_id);
                str_compania = str_callback_id.Substring(2, 8);
                str_TipoDocumento = str_callback_id.Substring(10, 2);
                str_NumeroDocumento = str_callback_id.Substring(12);

                DbCommand _Command = context.Database.GetDbConnection().CreateCommand();
                _Command.CommandType = System.Data.CommandType.StoredProcedure;
                _Command.CommandText = "SNP_CO_FlywareCobranza";
                _Command.Connection.Open();

                SqlParameter pIdPeticion = new SqlParameter("@pIdPeticion", obj_datos.id);
                _Command.Parameters.Add(pIdPeticion);

                SqlParameter pCompaniaSocio = new SqlParameter("@pCompaniaSocio", str_compania);
                _Command.Parameters.Add(pCompaniaSocio);

                SqlParameter pTipoDocumento = new SqlParameter("@pTipoDocumento", str_TipoDocumento);
                _Command.Parameters.Add(pTipoDocumento);

                SqlParameter pNumeroDocumento = new SqlParameter("@pNumeroDocumento", str_NumeroDocumento);
                _Command.Parameters.Add(pNumeroDocumento);

                SqlParameter pCallbackId = new SqlParameter("@pCallbackId", obj_datos.callback_id);
                _Command.Parameters.Add(pCallbackId);

                SqlParameter pMontoTotal = new SqlParameter("@pMontoTotal", obj_datos.amount);
                _Command.Parameters.Add(pMontoTotal);

                SqlParameter pEstado = new SqlParameter("@pEstado", obj_datos.status.ToUpper());
                _Command.Parameters.Add(pEstado);

                SqlParameter pFecha = new SqlParameter("@pFecha", obj_datos.at);
                _Command.Parameters.Add(pFecha);

                DbDataReader _reader = _Command.ExecuteReader();

                while (_reader.Read())
                {
                    if (!_reader.IsDBNull(0))
                        oBEc_Error.codigo = _reader.GetString(0);
                    if (!_reader.IsDBNull(1))
                        oBEc_Error.mensaje = _reader.GetString(1);                   

                }
                _reader.Close();
                _reader.Dispose();
                _Command.Dispose();
                _Command.Connection.Close();
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return oBEc_Error;
        }
    }
}

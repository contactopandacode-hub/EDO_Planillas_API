using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ServicioRSNetCore.Controllers.Clases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddDbContext<DbContext>(options => options.UseSqlServer(DecryptStringFromBytes_Aes(Configuration.GetConnectionString("dbSpringNetRrhh"))));
            services.AddDbContext<DbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("dbSpringNetRrhh")));

            // seguridad
            services.AddAuthentication().AddJwtBearer(
                cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;

                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = Configuration["Tokens:Issuer"],
                        ValidAudience = Configuration["Tokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                    };

                });
            //Mascara
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Integracion Planillas",
                    Version = "v1",
                    Description = "REST API by: Panda Code",
                    Contact = new OpenApiContact()
                    {
                        Name = "Walter Roman Parraga",
                        Email = "walter.roman.wr@gmail.com"
                    }

                });
                c.OperationFilter<MyHeaderFilter>();

                var xmlFile = "ServicioRSNetCore.xml";
                var xmlPath = Path.Combine(Configuration["xmlPath"],xmlFile);
                c.IncludeXmlComments(xmlPath);

                var xmlFileCobec = "COBEC.xml";
                var xmlPathCobec = Path.Combine(Configuration["xmlPathCobec"], xmlFileCobec);
                c.IncludeXmlComments(xmlPathCobec);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Interfaz Integrado Planilla");
                c.DefaultModelsExpandDepth(-1);
                c.HeadContent = @"
                               <style>
                                   /* Color de fondo del header */
                                   .swagger-ui .topbar { 
                                       background-color: #1a365d;
                                   }
       
                                   /* Botones POST */
                                   .opblock.opblock-post .opblock-summary-method {
                                       background-color: #2c5282 !important;
                                   }
       
                                   /* Bordes redondeados */
                                   .opblock {
                                       border-radius: 8px !important;
                                       margin: 8px 0 !important;
                                   }
       
                                   /* Título principal */
                                   .swagger-ui .info .title {
                                       color: #2c5282;
                                       font-weight: 600;
                                   }
       
                                   /* Hover efecto en endpoints */
                                   .opblock:hover {
                                       box-shadow: 0 2px 4px rgba(0,0,0,0.1);
                                   }
                               </style>";
            });
        }


        public static string DecryptStringFromBytes_Aes(String text)
        {

            byte[] cipherText = Convert.FromBase64String(text);

            String key = Environment.GetEnvironmentVariable("NETCORE_KEY");
            if (key == null)
                key = "hyb91p4nhvcnlmlkye17uyfz63q5jtcy";
            else if (key.Trim().Length != 32)
                key = "hyb91p4nhvcnlmlkye17uyfz63q5jtcy";

            var Key = Encoding.UTF8.GetBytes(key);
            var IV = Encoding.UTF8.GetBytes(key.Substring(0, 16));

            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            string plaintext = null;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}

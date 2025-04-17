using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Utils.Utils
{
    public class UtilsService : IUtilsService
    {
        public string GenerateKey()
        {
            string keyGenerate = Guid.NewGuid().ToString("N").Substring(0, 6);
            return keyGenerate;
        }

        /// <summary>
        /// GetCurrentTime = Procedimiento para obtener fecha y hora del servidor con timezone local
        /// </summary>
        /// <returns>Datetime time zone = "SA Pacific Standar Time"</returns>
        public DateTime GetCurrentDate()
        {
            DateTime serverTime = DateTime.Now;
            var a = "";
            List<string> b = new List<string>();
            foreach (TimeZoneInfo zoneID in TimeZoneInfo.GetSystemTimeZones())
            {
                b.Add(zoneID.Id);
            }

            DateTime _localTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(serverTime, TimeZoneInfo.Local.Id, "SA Pacific Standard Time");
            return _localTime;
        }

        /// <summary>
        /// GenerateKeyProducto = Procedimiento para crear una key de producto
        /// </summary>
        /// <param name="texto">Texto referencial del producto para obtener una key</param>
        /// <returns>ID a usar.</returns>
        public string GenerateKeyProducto(string texto)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(texto));

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                // Tomar solo los primeros 6 caracteres para el código
                string codigo = sb.ToString().Substring(0, 6);
                return codigo;
            }
        }

        /// <summary>
        /// Incrementa número a una entrada String
        /// </summary>
        /// <param name="input">Texto o string que contiene el último número.</param>
        /// <returns>Nuevo número con el formato requerido.</returns>
        public string IncrementarNumeroEnString(string input)
        {
            // Descomponer el string
            string texto = input.Substring(0, input.IndexOf('-') + 1);
            string parteNumerica = input.Substring(input.IndexOf('-') + 1);

            // Convertir la parte numérica a un número entero
            int numero = int.Parse(parteNumerica);

            // Incrementar el número en uno
            numero++;

            // Formatear el nuevo número con ceros a la izquierda
            string nuevaParteNumerica = numero.ToString("D" + parteNumerica.Length);

            // Concatenar la parte de texto y la parte numérica formateada
            string nuevoString = texto + nuevaParteNumerica;

            return nuevoString;
        }

        public string CapitalizeAfterDot(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            string[] parts = input.Split('.');
            for (int i = 0; i < parts.Length; i++)
            {
                if (!string.IsNullOrEmpty(parts[i]))
                {
                    parts[i] = char.ToUpper(parts[i][0]) + parts[i].Substring(1);
                }
            }

            return string.Join('.', parts);
        }
    }
}

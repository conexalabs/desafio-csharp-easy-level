using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace desafio_conexa.Validation
{
    public static class FormatValidation
    {
        public static bool ValidarFormatoInput<T>(params string[] inputs)
        {
            try
            {
                var conversor = TypeDescriptor.GetConverter(typeof(T));
                if (conversor != null)
                {
                    foreach (var input in inputs)
                        if (((T)conversor.ConvertFromString(input)).GetType() != typeof(T))
                            return false;
            }
                
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }


        }
        public static bool ValidarParametroVazio(params string [] inputs)
        {
            foreach (var input in inputs)
                if (string.IsNullOrEmpty(input))
                    return true;
            return false;
        }
    }
}

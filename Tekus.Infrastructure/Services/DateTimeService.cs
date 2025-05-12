using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class DateTimeService
    {
        /// <summary>
        /// Obtiene la fecha y hora actual.
        /// </summary>
        /// <returns>Fecha y hora actual en formato ISO 8601.</returns>
        public string GetCurrentDateTime()
        {
            return DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kara.Services
{
    /// <summary>
    /// The basic interface for logging.
    /// </summary>
    /// <typeparam name="TLog">The log object type.</typeparam>
    public interface ILoggingService<TLog>
        where TLog : class
    {
        /// <summary>
        /// Outputs a log into the database log table.
        /// </summary>
        /// <param name="log">The log to output.</param>
        /// <returns>The task of the asynchronous operation.</returns>
        Task OutLogAsync(TLog log);
    }
}

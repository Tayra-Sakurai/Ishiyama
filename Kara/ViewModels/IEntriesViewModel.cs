using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kara.ViewModels
{
    /// <summary>
    /// Basic interface for entity list view model.
    /// </summary>
    /// <typeparam name="TEntity">Type of the entities listed via the model.</typeparam>
    public interface IEntriesViewModel<TEntity>
        where TEntity : class, new()
    {
        /// <summary>
        /// Adds a new entry to the table.
        /// </summary>
        void Add();

        /// <summary>
        /// Saves the changes to the database.
        /// </summary>
        /// <returns>The asynchroinous task.</returns>
        Task SaveAsync();

        /// <summary>
        /// Removes the entry specified by <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity">The entity to be removed</param>
        void Remove(TEntity entity);

        /// <summary>
        /// Asynchronously loads the data from the database.
        /// </summary>
        /// <returns>The task instance.</returns>
        Task LoadAsync();

        /// <summary>
        /// The entities to be listed.
        /// </summary>
        ObservableCollection<TEntity> Entities { get; set; }

        /// <summary>
        /// Sends a message to jump to the page to show details.
        /// </summary>
        /// <param name="entity">The entity to show details.</param>
        void Detail(TEntity entity);
    }
}

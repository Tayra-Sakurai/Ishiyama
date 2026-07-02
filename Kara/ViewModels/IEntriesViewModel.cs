using CommunityToolkit.Mvvm.Input;
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
        /// <returns>The task to manage the asynchronous process.</returns>
        Task AddAsync();

        /// <summary>
        /// Gets an <see cref="IAsyncRelayCommand"/> to implement <see cref="AddAsync"/>.
        /// </summary>
        /// <value>The command to implement the funtion.</value>
        IAsyncRelayCommand AddCommand { get; }

        /// <summary>
        /// Removes the entry specified by <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity">The entity to be removed</param>
        /// <returns>The task manager of the asynchronous process.</returns>
        Task RemoveAsync(TEntity entity);

        /// <summary>
        /// Gets the instance of <see cref="IAsyncRelayCommand{TEntity}"/> of <see cref="RemoveAsync(TEntity)"/>.
        /// </summary>
        IAsyncRelayCommand<TEntity> RemoveCommand { get; }

        /// <summary>
        /// Asynchronously loads the data from the database.
        /// </summary>
        /// <returns>The task instance.</returns>
        Task LoadAsync();

        /// <summary>
        /// Gets the loading command.
        /// </summary>
        IAsyncRelayCommand LoadCommand { get; }

        /// <summary>
        /// The entities to be listed.
        /// </summary>
        /// <value>The entities in the table.</value>
        ObservableCollection<TEntity> Entities { get; set; }

        /// <summary>
        /// Sends a message to jump to the page to show details.
        /// </summary>
        /// <param name="entity">The entity to show details.</param>
        void Detail(TEntity entity);

        /// <summary>
        /// Gets the command to open the details.
        /// </summary>
        IRelayCommand<TEntity> DetailCommand { get; }
    }
}

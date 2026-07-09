using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kara.ViewModels
{
    /// <summary>
    /// Base interface for single entry.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public interface IEntryViewModel<TEntity> : INotifyDataErrorInfo
        where TEntity : class, new()
    {
        /// <summary>
        /// An <see cref="IAsyncRelayCommand"/> to have user to save the edition.
        /// </summary>
        IAsyncRelayCommand SaveCommand { get; }

        /// <summary>
        /// Loads the default existing value.
        /// </summary>
        /// <param name="entity">The existing entity.</param>
        /// <returns>The task process.</returns>
        Task LoadExistingDataAsync(TEntity entity);

        /// <summary>
        /// Gets the command to remove the entity.
        /// </summary>
        IAsyncRelayCommand RemoveCommand { get; }
    }
}

﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace TodoApp.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<ToDoItemViewModel> ToDoItems { get; } = new ObservableCollection<ToDoItemViewModel>();


        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddItemCommand))] // This attribute will invalidate the command each time this property changes
        private string? _newItemContent;

        /// <summary>
        /// Returns if a new Item can be added. We require to have the NewItem some Text
        /// </summary>
        
        private bool CanAddItem() => !string.IsNullOrWhiteSpace(NewItemContent);
        
        [RelayCommand(CanExecute = nameof(CanAddItem))]
        private void AddItem()
        {
            // Add a new item to the list
            ToDoItems.Add(new ToDoItemViewModel() { Content = NewItemContent });

            // reset the NewItemContent
            NewItemContent = null;
        }
        [RelayCommand]
        private void RemoveItem(ToDoItemViewModel item)
        {
            // Remove the given item from the list
            ToDoItems.Remove(item);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace todo_WPF;

using System.Runtime.CompilerServices;
using ToDo;

public partial class MainWindow : Window, INotifyPropertyChanged
{

    public event PropertyChangedEventHandler? PropertyChanged;
    public ObservableCollection<Task> Tasks { get; set; } = new ObservableCollection<Task>();

    private Task selectedTask;
    public Task SelectedTask
    {
        get => selectedTask;
        set => this.PropertyChangeMethod(out this.selectedTask, value);
    }


    public MainWindow()
    {
        this.DataContext = this;
        InitializeComponent();
    }
    protected void PropertyChangeMethod<T>(out T field, T value, [CallerMemberName] string propName = "")
    {
        field = value;

        if (this.PropertyChanged != null)
            this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
        
    }

    private void OpenAddWindow(object sender, RoutedEventArgs e)
    {
        new AddTaskWindow(this.Tasks).ShowDialog();
        
    }
    private void OpenInfoWindow(object sender, RoutedEventArgs e)
    {
        if(this.SelectedTask != null)
        {
            new TaskFullInfoWindow(this.SelectedTask).ShowDialog();
        }
        
    }
   private void SelectedTaskStatusUpgrade(object sender, RoutedEventArgs e) {
        if (this.SelectedTask != null)
        {
            this.SelectedTask.RaiseStatus();
            MessageBox.Show($"The status of task \"{SelectedTask.Name}\" has been upgraded to {SelectedTask.Status}");
        }

   }
    private void DeleteSelectedTask(object sender, RoutedEventArgs e)
    {
        if(SelectedTask != null)
            this.Tasks.Remove(SelectedTask);
    }
}
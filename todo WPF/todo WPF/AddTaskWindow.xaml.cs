using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace todo_WPF;


public partial class AddTaskWindow : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private readonly ObservableCollection<ToDo.Task> tasks;

    private string? newTaskName;
    public string? NewTaskName
    {
        get => newTaskName;
        set => this.PropertyChangeMethod(out newTaskName, value);
    }

    private string? newTaskDescription;

    public string? NewTaskDescription
    {
        get => newTaskDescription;
        set => this.PropertyChangeMethod(out newTaskDescription, value);
    }

    public AddTaskWindow()
    {
        InitializeComponent();

        this.DataContext = this;
    }

    public AddTaskWindow(ObservableCollection<ToDo.Task> tasks) : this()
    {
        this.tasks = tasks;
    }

    protected void PropertyChangeMethod<T>(out T field, T value, [CallerMemberName] string propName = "")
    {
        field = value;

        if (this.PropertyChanged != null)
        {
            this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }

    private void AddTaskClick(object sender, RoutedEventArgs e)
    {
        this.tasks?.Add(new ToDo.Task(name: this.NewTaskName, description: NewTaskDescription));

        this.Close();
    }
}

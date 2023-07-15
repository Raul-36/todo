using System;
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

using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using ToDo;

public partial class MainWindow : Window, INotifyPropertyChanged
{

    public event PropertyChangedEventHandler? PropertyChanged;
    public ObservableCollection<Task> Tasks { get; set; }

    private Task selectedTask;
    public Task SelectedTask
    {
        get => selectedTask;
        set => this.PropertyChangeMethod(out this.selectedTask, value);
    }


    public MainWindow()
    {
        this.Tasks = new ObservableCollection<Task>(this.LoadTasks());
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
        this.SaveTasks();

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
            this.SaveTasks();
        }

   }
    private void DeleteSelectedTask(object sender, RoutedEventArgs e)
    {
        if (SelectedTask != null)
        {
            this.Tasks.Remove(SelectedTask);
            this.SaveTasks();
        }
    }
    private IEnumerable<Task> LoadTasks()
    {
        string filePath = "Data/Tasks.json";
        var tasks = JsonSerializer.Deserialize<IEnumerable<Task>>(File.ReadAllText(filePath));
        return tasks;
    }
    private void SaveTasks()
    {
        string filePath = "Data/Tasks.json";
        File.WriteAllText(filePath, JsonSerializer.Serialize(this.Tasks));
    }
}

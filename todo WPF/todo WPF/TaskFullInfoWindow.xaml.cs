using System;
using System.Collections.Generic;
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

namespace todo_WPF
{
   
    public partial class TaskFullInfoWindow : Window, INotifyPropertyChanged
    {
       
        private  string  taskName;
        public string TaskName { get => taskName; private set => PropertyChangeMethod(out taskName, value); }
        private string taskDescription;
        public string TaskDescription  { get => taskDescription; set=> PropertyChangeMethod(out taskDescription, value); }

        
        public TaskFullInfoWindow()
        {
            this.DataContext = this;
            InitializeComponent();
        }
        public TaskFullInfoWindow(ToDo.Task task) : this()
        {
            this.TaskName = task.Name;
            this.TaskDescription = task.Description;
           
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void PropertyChangeMethod<T>(out T field, T value, [CallerMemberName] string propName = "")
        {
            field = value;

            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
            }
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

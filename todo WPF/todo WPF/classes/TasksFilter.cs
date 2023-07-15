using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace ToDo
{
    public class TasksFilter
    {
        private List<Predicate<Task>> filters = new List<Predicate<Task>>();

        public TasksFilter() { }
        public void AddFilter(Predicate<Task> filter)
        {
            filters.Add(filter);
        }
        public bool CheckTask(Task task)
        {
            foreach (var item in filters)
            {
                if (item.Invoke(task))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool NotStarted(Task task) => (task.Status == TaskStatus.NotStarted);
        public static bool InProgress(Task task) => (task.Status == TaskStatus.InProgress);
        public static bool Completed(Task task) => (task.Status == TaskStatus.Completed);
    }
}

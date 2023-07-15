using System;
using System.Text.Json.Serialization;

namespace ToDo
{
    public class Task
    {
       
        public  string Name;
        
        public  string Description;
        public TaskStatus Status { get;  set; }
        
        public string ShortInfo => $"{this.Name}: {this.Status}";
        public Task()
        {
            
        }
        public Task(string name, string description)
        {
            this.Name = name;
            this.Description = description;
            this.Status = TaskStatus.NotStarted;
        }
        public void RaiseStatus() {
            if (this.Status == TaskStatus.Completed)
                return;
            this.Status++;
        }
        public virtual string FullInfo()
        {
            return @$"{this.Name}:
    {this.Description}
    Status: {this.Status}";
        }
        public override string ToString() => this.ShortInfo;
        
    }
}

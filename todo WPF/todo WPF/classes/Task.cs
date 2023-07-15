using System;
using System.Text.Json.Serialization;

namespace ToDo
{
    public class Task
    {
        public readonly string Name;
        public readonly string Description;
        public TaskStatus Status { get; private set; }
        [JsonIgnore]
        public string ShortInfo => $"{this.Name}: {this.Status}"; 
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

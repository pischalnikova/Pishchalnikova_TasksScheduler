using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TasksSchedulerData.Entities
{
    public enum PriorityLevel
    {
        Low = 1,
        Normal = 2,
        High = 3
    }
    public enum TaskState
    {
        Created = 1,
        InWork = 2,
        Completed = 3
    }
    public class WorkTask
    {
        public static BigInteger TaskCounter = BigInteger.Zero;

        public Guid TaskId { get; set; } = Guid.NewGuid();

        public string TaskName { get; set; }

        public string TaskDescription { get; set; }

        public PriorityLevel Priority { get; set; }

        public DateTime DeadlineDate { get; set; }

        public TaskState State { get; set; } = TaskState.Created;

        public WorkTask(string name)
        {
            TaskName = name;
            TaskDescription = "";
            State = TaskState.Created;
            Priority = PriorityLevel.Normal;
        }
        public WorkTask()
        {
            State = TaskState.Created;
            Priority = PriorityLevel.Normal;
        }
    }
}

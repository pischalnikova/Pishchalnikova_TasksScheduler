using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksSchedulerData.Entities;

namespace TasksSchedulerData
{
    public class ApplContext
    {
        public List<WorkTask> Tasks;

        public ApplContext()
        {
            Tasks = new List<WorkTask>();


            DataFill();
        }

        public void DataFill()
        {
            Tasks.Add(new WorkTask
            {
                TaskName = "Помыть машину",
                TaskDescription = "Съездить на мойку",
                Priority = PriorityLevel.High,
                DeadlineDate = DateTime.Now.AddDays(1),
                State = TaskState.InWork
            });

            Tasks.Add(new WorkTask
            {
                TaskName = "Оформить кредит",
                TaskDescription = "Подготовить документы",
                Priority = PriorityLevel.High,
                DeadlineDate = DateTime.Now.AddDays(2),
                State = TaskState.Created
            });

            Tasks.Add(new WorkTask
            {
                TaskName = "Техосмотр",
                TaskDescription = "Проверить автомобиль",
                Priority = PriorityLevel.Normal,
                DeadlineDate = DateTime.Now.AddDays(3),
                State = TaskState.InWork
            });

            Tasks.Add(new WorkTask
            {
                TaskName = "Накормить коров",
                TaskDescription = "Еда",
                Priority = PriorityLevel.Normal,
                DeadlineDate = DateTime.Now.AddDays(5),
                State = TaskState.Created
            });

            Tasks.Add(new WorkTask
            {
                TaskName = "Уборка",
                TaskDescription = "Прибрать комнату",
                Priority = PriorityLevel.Low,
                DeadlineDate = DateTime.Now.AddDays(7),
                State = TaskState.Created
            });
        }
    }
}

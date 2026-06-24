using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksSchedulerData.Entities;

namespace TasksSchedulerData.Services
{
    public interface IServiceWorkTask
    {
        void Add(WorkTask task);

        void Update(WorkTask task);

        void Delete(WorkTask task);

        List<WorkTask> GetTasks();

        List<WorkTask> GetStateTasks(TaskState state);

        List<WorkTask> GetTextTasks(string txt);
    }

    public class ServiceWorkTask : IServiceWorkTask
    {
        private readonly ApplContext _context;

        public ServiceWorkTask(ApplContext context)
        {
            _context = context;
        }

        public void Add(WorkTask task)
        {
            ValidateTask(task);

            _context.Tasks.Add(task);
        }

        public void Update(WorkTask task)
        {
            ValidateTask(task);

            var target = _context.Tasks
                                 .FirstOrDefault(t => t.TaskId == task.TaskId);

            if (target == null)
            {
                throw new InvalidOperationException("Задача не найдена.");
            }

            target.TaskName = task.TaskName;
            target.TaskDescription = task.TaskDescription;
            target.Priority = task.Priority;
            target.State = task.State;
            target.DeadlineDate = task.DeadlineDate;
        }

        public void Delete(WorkTask task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            if (!_context.Tasks.Contains(task))
            {
                throw new InvalidOperationException("Задача не найдена.");
            }

            _context.Tasks.Remove(task);
        }

        public List<WorkTask> GetTasks()
        {
            return _context.Tasks;
        }

        public List<WorkTask> GetStateTasks(TaskState state)
        {
            return _context.Tasks
                           .Where(t => t.State == state)
                           .ToList();
        }

        public List<WorkTask> GetTextTasks(string text)
        {
            return _context.Tasks
                           .Where(t =>
                               t.TaskName.Contains(text, StringComparison.OrdinalIgnoreCase)
                               || t.TaskDescription.Contains(text, StringComparison.OrdinalIgnoreCase))
                           .ToList();
        }

        private static void ValidateTask(WorkTask task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            if (string.IsNullOrWhiteSpace(task.TaskName))
            {
                throw new ArgumentException("Имя задачи не задано.");
            }

            if (task.DeadlineDate <= DateTime.Now)
            {
                throw new ArgumentException("Дата выполнения должна быть больше текущей.");
            }
        }

        public void ReplaceAll(List<WorkTask> tasks)
        {
            _context.Tasks.Clear();
            _context.Tasks.AddRange(tasks);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksSchedulerData.Entities;
using TasksSchedulerData.Services;

namespace TasksSchedulerData.Services.Tests
{
    [TestClass()]
    public class ServiceWorkTaskTests
    {
        private ApplContext _context;
        private ServiceWorkTask _service;

        [TestInitialize]
        public void Initialize()
        {
            _context = new ApplContext();
            _service = new ServiceWorkTask(_context);
        }

        [TestMethod()]
        public void ServiceWorkTaskTest()
        {
            Assert.IsNotNull(_service);
            Assert.IsInstanceOfType(_service, typeof(ServiceWorkTask));
        }

        [TestMethod()]
        public void AddTest()
        {
            var task = new WorkTask("Проектирование")
            {
                TaskDescription = "Подробности",
                DeadlineDate = DateTime.Now.AddDays(1)
            };

            _service.Add(task);

            Assert.AreEqual(1, _context.Tasks.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullTask_ThrowsException()
        {
            _service.Add(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_EmptyName_ThrowsException()
        {
            var task = new WorkTask("")
            {
                DeadlineDate = DateTime.Now.AddDays(1)
            };

            _service.Add(task);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_InvalidDate_ThrowsException()
        {
            var task = new WorkTask("Проектирование")
            {
                DeadlineDate = DateTime.Now.AddDays(-1)
            };

            _service.Add(task);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            var task = new WorkTask("Первичный отчет")
            {
                DeadlineDate = DateTime.Now.AddDays(1)
            };

            _context.Tasks.Add(task);

            var updatedTask = new WorkTask("Завершенный отчет")
            {
                TaskId = task.TaskId,
                TaskDescription = "Отчет закончен",
                DeadlineDate = DateTime.Now.AddDays(2)
            };

            _service.Update(updatedTask);

            Assert.AreEqual("Завершенный отчет", _context.Tasks[0].TaskName);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Update_NotExistingTask_ThrowsException()
        {
            var task = new WorkTask("Первичный отчет")
            {
                DeadlineDate = DateTime.Now.AddDays(1)
            };

            _service.Update(task);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            var task = new WorkTask("Проект")
            {
                DeadlineDate = DateTime.Now.AddDays(1)
            };

            _context.Tasks.Add(task);

            _service.Delete(task);

            Assert.AreEqual(0, _context.Tasks.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_NullTask_ThrowsException()
        {
            _service.Delete(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Delete_NotExistingTask_ThrowsException()
        {
            var task = new WorkTask("Проект")
            {
                DeadlineDate = DateTime.Now.AddDays(1)
            };

            _service.Delete(task);
        }

        [TestMethod()]
        public void GetStateTasksTest()
        {
            _context.Tasks.Add(new WorkTask("Проект")
            {
                DeadlineDate = DateTime.Now.AddDays(1),
                State = TaskState.Created
            });

            _context.Tasks.Add(new WorkTask("Проект2")
            {
                DeadlineDate = DateTime.Now.AddDays(1),
                State = TaskState.Completed
            });

            var result = _service.GetStateTasks(TaskState.Created);

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod()]
        public void GetTextTasksTest()
        {
            _context.Tasks.Add(new WorkTask("Проект1")
            {
                TaskDescription = "Проектное решение",
                DeadlineDate = DateTime.Now.AddDays(1)
            });

            var result = _service.GetTextTasks("проект");

            Assert.AreEqual(1, result.Count);
        }
    }
}
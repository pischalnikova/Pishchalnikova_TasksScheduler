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
    public class ServiceLoadDataJsonTests
    {
        private ServiceLoadDataJson _service;
        private string _path;

        [TestInitialize]
        public void Initialize()
        {
            _service = new ServiceLoadDataJson();
            _path = "test.json";

            if (File.Exists(_path))
                File.Delete(_path);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(_path))
                File.Delete(_path);
        }

        [TestMethod()]
        public void Save_And_Load_ReturnsTasks()
        {
            var tasks = new List<WorkTask>
            {
                new WorkTask("Проект")
                {
                    TaskDescription = "Проект",
                    DeadlineDate = DateTime.Now.AddDays(1),
                    State = TaskState.Completed
                },
                new WorkTask("Отчет")
                {
                    DeadlineDate = DateTime.Now.AddDays(2)
                }
            };


            _service.Save(_path, tasks);

            var loadedTasks = _service.Load(_path);

            Assert.AreEqual(2, loadedTasks.Count);
            Assert.AreEqual("Проект", loadedTasks[0].TaskName);
            Assert.AreEqual("Отчет", loadedTasks[1].TaskName);
            Assert.AreEqual(TaskState.Completed,
                            loadedTasks[0].State);
            Assert.AreEqual(TaskState.Created,
                            loadedTasks[1].State);
        }

        [TestMethod()]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Load_NotExistingFile_ThrowsException()
        {
            _service.Load("unknown.json");
        }
    }
}
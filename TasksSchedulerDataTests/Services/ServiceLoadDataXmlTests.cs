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
    public class ServiceLoadDataXmlTests
    {
        private ServiceLoadDataXml _service;
        private string _path;

        [TestInitialize]
        public void Initialize()
        {
            _service = new ServiceLoadDataXml();
            _path = "test.xml";

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
                        DeadlineDate = DateTime.Now.AddDays(1),
                        State = TaskState.Completed
                    },
                    new WorkTask("Отчет")
                    {
                        DeadlineDate = DateTime.Now.AddDays(2)
                    }
            };

            _service.Save(_path, tasks);

            var loaded = _service.Load(_path);

            Assert.AreEqual(2, loaded.Count);
            Assert.AreEqual("Проект", loaded[0].TaskName);
            Assert.AreEqual("Отчет", loaded[1].TaskName);
            Assert.AreEqual(TaskState.Completed,
                            loaded[0].State);
            Assert.AreEqual(TaskState.Created,
                            loaded[1].State);
        }


        [TestMethod()]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Load_NotExistingFile_ThrowsException()
        {
            _service.Load("unknown.xml");
        }
    }
}
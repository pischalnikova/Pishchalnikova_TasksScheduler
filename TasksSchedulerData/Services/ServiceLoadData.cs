using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TasksSchedulerData.Entities;

namespace TasksSchedulerData.Services
{
    public interface IServiceLoadData
    {
        void Save(string path, List<WorkTask> tasks);

        List<WorkTask> Load(string path);
    }
    public class ServiceLoadDataJson : IServiceLoadData
    {
        public void Save(string path, List<WorkTask> tasks)
        {
            string json = JsonSerializer.Serialize(
                tasks,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(path, json);
        }

        public List<WorkTask> Load(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Файл не найден.", path);
            }

            string json = File.ReadAllText(path);

            return JsonSerializer.Deserialize<List<WorkTask>>(json)
                   ?? new List<WorkTask>();
        }
    }

    public class ServiceLoadDataXml : IServiceLoadData
    {
        public void Save(string path, List<WorkTask> tasks)
        {
            XmlSerializer serializer =
                new XmlSerializer(typeof(List<WorkTask>));

            using var stream = File.Create(path);

            serializer.Serialize(stream, tasks);
        }

        public List<WorkTask> Load(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Файл не найден.", path);
            }

            XmlSerializer serializer =
                new XmlSerializer(typeof(List<WorkTask>));

            using var stream = File.OpenRead(path);

            return (List<WorkTask>)serializer.Deserialize(stream)!;
        }
    }
}

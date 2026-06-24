using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TasksScheduler.Controls;

namespace TasksScheduler
{
    public class Commands
    {
        static Commands()
        {
            SaveJson = new RoutedCommand("SaveJson", typeof(MainWindow));
            LoadJson = new RoutedCommand("LoadJson", typeof(MainWindow));
            SaveXml = new RoutedCommand("SaveXml", typeof(MainWindow));
            LoadXml = new RoutedCommand("LoadXml", typeof(MainWindow));


            TaskAdd = new RoutedCommand("TaskAdd", typeof(MainWindow));
            TaskEdit = new RoutedCommand("TaskEdit", typeof(MainWindow));
            TaskDelete = new RoutedCommand("TaskDelete", typeof(MainWindow));
        }

        public static RoutedCommand SaveJson { get; set; }

        public static RoutedCommand LoadJson { get; set; }

        public static RoutedCommand SaveXml { get; set; }

        public static RoutedCommand LoadXml { get; set; }

        public static RoutedCommand TaskAdd { get; set; }

        public static RoutedCommand TaskEdit { get; set; }

        public static RoutedCommand TaskDelete { get; set; }

    }
}

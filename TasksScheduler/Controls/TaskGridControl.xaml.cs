using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TasksScheduler.Views;
using TasksSchedulerData;
using TasksSchedulerData.Entities;
using TasksSchedulerData.Services;

namespace TasksScheduler.Controls
{
    /// <summary>
    /// Логика взаимодействия для TaskGridControl.xaml
    /// </summary>
    public partial class TaskGridControl : UserControl
    {
        private readonly ServiceWorkTask _taskService;
        private readonly IServiceLoadData _jsonService;
        private readonly IServiceLoadData _xmlService;

        public TaskGridControl()
        {
            InitializeComponent();

            var context = new ApplContext();

            _taskService = new ServiceWorkTask(context);
            _jsonService = new ServiceLoadDataJson();
            _xmlService = new ServiceLoadDataXml();


            cbState.Items.Add("Все");

            foreach (var state in Enum.GetValues(typeof(TaskState)))
            {
                cbState.Items.Add(state);
            }

            cbState.SelectedIndex = 0;

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            tasksGrid.ItemsSource = null;
            tasksGrid.ItemsSource = _taskService.GetTasks();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var window = new TaskEditWindow();

                if (window.ShowDialog() == true)
                {
                    _taskService.Add(window.Task);
                    RefreshGrid();
                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = tasksGrid.SelectedItem as WorkTask;

                if (selected == null)
                {
                    MessageBox.Show("Выберите задачу.");
                    return;
                }

                var copy = new WorkTask
                {
                    TaskId = selected.TaskId,
                    TaskName = selected.TaskName,
                    TaskDescription = selected.TaskDescription,
                    Priority = selected.Priority,
                    State = selected.State,
                    DeadlineDate = selected.DeadlineDate
                };

                var window = new TaskEditWindow(copy);

                if (window.ShowDialog() == true)
                {
                    _taskService.Update(window.Task);
                    RefreshGrid();
                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var task = tasksGrid.SelectedItem as WorkTask;

                if (task == null)
                {
                    MessageBox.Show("Выберите задачу.");
                    return;
                }

                _taskService.Delete(task);

                RefreshGrid();
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            List<WorkTask> result;

            string text = tbSearch.Text.Trim();

            // Если введен текст
            if (!string.IsNullOrWhiteSpace(text))
            {
                result = _taskService.GetTextTasks(text);
            }
            else
            {
                result = _taskService.GetTasks();
            }

            // Дополнительно фильтруем по статусу
            if (cbState.SelectedItem is TaskState state)
            {
                result = result
                    .Where(t => t.State == state)
                    .ToList();
            }

            tasksGrid.ItemsSource = null;
            tasksGrid.ItemsSource = result;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            tbSearch.Clear();

            cbState.SelectedIndex = 0;

            RefreshGrid();
        }

        private void SaveJson_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _jsonService.Save("tasks.json", _taskService.GetTasks());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadJson_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var tasks = _jsonService.Load("tasks.json");

                _taskService.ReplaceAll(tasks);

                RefreshGrid();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveXml_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _xmlService.Save(
                "tasks.xml",
                _taskService.GetTasks());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadXml_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var tasks = _xmlService.Load("tasks.xml");

                _taskService.ReplaceAll(tasks);


                RefreshGrid();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

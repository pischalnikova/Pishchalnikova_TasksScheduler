using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using TasksSchedulerData.Entities;

namespace TasksScheduler.Views
{
    /// <summary>
    /// Логика взаимодействия для TaskEditWindow.xaml
    /// </summary>
    public partial class TaskEditWindow : Window
    {
        public WorkTask Task { get; private set; }

        public TaskEditWindow()
        {
            InitializeComponent();

            cbPriority.ItemsSource =
                Enum.GetValues(typeof(PriorityLevel));

            cbState.ItemsSource =
                Enum.GetValues(typeof(TaskState));

            Task = new WorkTask();
        }

        public TaskEditWindow(WorkTask task) : this()
        {
            Task = task;

            tbName.Text = task.TaskName;
            tbDescription.Text = task.TaskDescription;
            cbPriority.SelectedItem = task.Priority;
            cbState.SelectedItem = task.State;
            dpDeadline.SelectedDate = task.DeadlineDate;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tbName.Text))
                    throw new ArgumentException("Название задачи не может быть пустым.");

                if (cbPriority.SelectedItem == null)
                    throw new ArgumentException("Выберите приоритет задачи.");

                if (cbState.SelectedItem == null)
                    throw new ArgumentException("Выберите статус задачи.");

                if (dpDeadline.SelectedDate == null)
                    throw new ArgumentException("Укажите дату выполнения.");

                Task.TaskName = tbName.Text;
                Task.TaskDescription = tbDescription.Text;

                Task.Priority = (PriorityLevel)cbPriority.SelectedItem;
                Task.State = (TaskState)cbState.SelectedItem;

                Task.DeadlineDate = dpDeadline.SelectedDate.Value;

                DialogResult = true;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Ошибка ввода",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }
    }
}

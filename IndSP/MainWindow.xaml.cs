using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace IndSP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    [Serializable]
    public partial class MainWindow : Window
    {
        ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
        ObservableCollection<Post> Posts = new ObservableCollection<Post>();
        ObservableCollection<Employee> selectedEmplCol;
        string emplFile = "Employees.dat";
        string postsFile = "Posts.dat";
        BinaryFormatter formatter = new BinaryFormatter();
        string selectedBy = "";
        public MainWindow()
        {
            InitializeComponent();
            lbxPosts.ItemsSource = Posts;
            EditEmpl.ItemsSource = employees;
            cbxPosts.ItemsSource = Posts;

        }

        private void addEmplBtn_Click(object sender, RoutedEventArgs e)
        {
            employees.Add(new Employee());
            Console.WriteLine(employees.Count);
        }

        private void addPostBtn_Click(object sender, RoutedEventArgs e)
        {
            Posts.Add(new Post());
        }

        private void selectByAwardsBtn_Click(object sender, RoutedEventArgs e)
        {
            selectedBy = "selectByAwards";
            selectedEmpl.ItemsSource = null;
            selectedEmplCol = new ObservableCollection<Employee>(employees.Where(empl => empl.HaveAward == true));
            selectedEmpl.ItemsSource = selectedEmplCol;
            foreach (Employee emplo in selectedEmplCol)
            {
                Console.WriteLine(emplo.TNumber);
            }
        }

        private void delPostBtn_Click(object sender, RoutedEventArgs e)
        {
            if (lbxPosts.SelectedItem != null)
            {
                Posts.Remove((Post)lbxPosts.SelectedItem);
            }
        }

        private void delEmplBtn_Click(object sender, RoutedEventArgs e)
        {
            if (EditEmpl.SelectedItem != null)
            {
                if (employees.Count != 0)
                {
                    employees.Remove((Employee)EditEmpl.SelectedItem);
                }
            }
        }

        private void slectByAgeBtn_Click(object sender, RoutedEventArgs e)
        {
            selectedBy = "selectByAge";
            selectedEmpl.ItemsSource = null;
            TimeSpan gap = new TimeSpan(21900, 0, 0, 0);
            selectedEmplCol = new ObservableCollection<Employee>(employees.Where(empl => (DateTime.Now.Subtract(empl.BithdayDate)) >= gap));
            selectedEmpl.ItemsSource = selectedEmplCol;
            foreach (Employee emplo in selectedEmplCol)
            {
                Console.WriteLine(emplo.TNumber);
            }
        }

        private void selectByAvgSalary_Click(object sender, RoutedEventArgs e)
        {
            double avg = 0;
            selectedBy = "selectByAwgSalary";
            foreach (Employee emp in employees)
            {
                avg += Int32.Parse(emp.Salary);
            }
            avg /= employees.Count;
            selectedEmpl.ItemsSource = null;
            selectedEmplCol = new ObservableCollection<Employee>(employees.Where(empl => Int32.Parse(empl.Salary) > avg));
            selectedEmpl.ItemsSource = selectedEmplCol;
            foreach (Employee emplo in selectedEmplCol)
            {
                Console.WriteLine(emplo.TNumber);
            }
        }

        private void selectBirthday_Click(object sender, RoutedEventArgs e)
        {
            DateTime maxRange = DateTime.Now.AddDays(20);
            DateTime temp;
            selectedBy = "selectByBirthday";
            selectedEmpl.ItemsSource = null;
            selectedEmplCol = new ObservableCollection<Employee>(employees.Where(empl =>
            (empl.BithdayDate <= maxRange && (empl.BithdayDate.AddYears(DateTime.Today.Year - empl.BithdayDate.Year)) >= DateTime.Today && (empl.BithdayDate.AddYears(DateTime.Now.Year - empl.BithdayDate.Year)) <= maxRange)));
            selectedEmpl.ItemsSource = selectedEmplCol;
            foreach (Employee emp in employees)
            {
                temp = emp.BithdayDate.AddYears(DateTime.Now.Year - emp.BithdayDate.Year);
                Console.WriteLine(temp);
                if (emp.BithdayDate <= maxRange && temp >= DateTime.Today && temp <= maxRange)
                {
                    Console.WriteLine("\n\n\n");
                    Console.WriteLine(temp);
                }
            }
        }

        private void delAllBtn_Click(object sender, RoutedEventArgs e)
        {
            selectedEmpl.SelectAll();
            foreach (Employee emp in selectedEmplCol.ToList())
            {
                selectedEmplCol.Remove(emp);
            }
            MessageBox.Show("Все було видалено з цього списку \n Для того щоб видалити вибраних робітників з глобального списку замініть selectedEmplCol на employees в тілі циклу");
        }

        private void saveSelectedBtn_Click(object sender, RoutedEventArgs e)
        {
            FileStream output = new FileStream(selectedBy + ".txt", FileMode.Create);
            string outputStr = "";
            foreach (Employee empl in selectedEmplCol)
            {
                outputStr += "\n" + empl;
            }
            byte[] arr = System.Text.Encoding.Default.GetBytes(outputStr);
            output.Write(arr, 0, arr.Length);
            output.Close();
        }
        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            save();
        }

        private void loadBtn_Click(object sender, RoutedEventArgs e)
        {
            load();
        }
        private void load()
        {
            FileStream loadStreamEpml = new FileStream(emplFile, FileMode.OpenOrCreate);
            FileStream loadStreamPosts = new FileStream(postsFile, FileMode.OpenOrCreate);
            employees = (ObservableCollection<Employee>)formatter.Deserialize(loadStreamEpml);
            Posts = (ObservableCollection<Post>)formatter.Deserialize(loadStreamPosts);
            lbxPosts.ItemsSource = null;
            EditEmpl.ItemsSource = null;
            cbxPosts.ItemsSource = null;
            lbxPosts.ItemsSource = Posts;
            EditEmpl.ItemsSource = employees;
            cbxPosts.ItemsSource = Posts;
            loadStreamEpml.Close();
            loadStreamPosts.Close();
        }
        private void save()
        {
            FileStream saveStreamEpml = new FileStream(emplFile, FileMode.Create);
            FileStream saveStreamPosts = new FileStream(postsFile, FileMode.Create);
            formatter.Serialize(saveStreamEpml, employees);
            formatter.Serialize(saveStreamPosts, Posts);
            saveStreamEpml.Close();
            saveStreamPosts.Close();
        }
    }
}

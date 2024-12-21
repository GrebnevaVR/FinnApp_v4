using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using System.Configuration;
using System.ComponentModel;

namespace FinnApp_v4
{
    public class ProjectAmount
    {
        public string ProjectName { get; set; }
        public string Amount { get; set; }

        public ProjectAmount(string projectName, string amount)
        {
            ProjectName = projectName;
            Amount = amount;
        }
    }
    internal class ApplicationViewModel 
    
    {
        ApplicationContext db = new ApplicationContext();
        RelayCommand? addCommand;
        RelayCommand? editCommand;
        RelayCommand? deleteCommand;
        public ObservableCollection<Budget> Budgets { get; set; }
        private ObservableCollection<string> _projectNames;
        public ObservableCollection<string> ProjectNames
        {
            get { return _projectNames; }
            set
            {
                _projectNames = value;
                
            }
        }
        private ObservableCollection<string> _userNames;
        public ObservableCollection<string> UserNames
        {
            get { return _userNames; }
            set
            {
                _userNames = value;

            }        
        }
        private ObservableCollection<string> _amountProject;
        public ObservableCollection<ProjectAmount> AmountProject;
        
        public ApplicationViewModel()
        {
            SQLitePCL.Batteries.Init();
            db.Database.EnsureCreated();
            db.Budgets.Load();
            Budgets = db.Budgets.Local.ToObservableCollection();
            LoadProjectNames();
            LoadUserNames();
            LoadAmountProject();
        }
        public void LoadUserNames()
        {
            string connectionString = "Data Source=Budgets.db";

            UserNames = new ObservableCollection<string>();

            //            string connectionString = ConfigurationManager.ConnectionStrings["BudgetsDatabase"].ConnectionString;


            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT DISTINCT User FROM Budgets";
                using (var command = new SqliteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserNames.Add(reader["User"].ToString());
                        }
                    }
                }
            }

        }
        public void LoadProjectNames()
        {
            string connectionString = "Data Source=Budgets.db";
            
            ProjectNames = new ObservableCollection<string>();

            //            string connectionString = ConfigurationManager.ConnectionStrings["BudgetsDatabase"].ConnectionString;

            
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT DISTINCT ProjectName FROM Budgets";
                using (var command = new SqliteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProjectNames.Add(reader["ProjectName"].ToString());
                        }
                    }
                }
            }

        }
        public void LoadAmountProject()
        {
            string connectionString = "Data Source=Budgets.db";

            AmountProject = new ObservableCollection<ProjectAmount>();
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT ProjectName, sum(Amount) as s FROM Budgets GROUP BY ProjectName";
                using (var command = new SqliteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string projectName = reader["ProjectName"].ToString();
                            string amount = reader["s"].ToString();
                            AmountProject.Add(new ProjectAmount(projectName, amount));
                        }
                    }
                }
            }
        }
        // команда добавления
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand((o) =>
                  {
                      BudgetWindow budgetWindow = new BudgetWindow(new Budget());
                      if (budgetWindow.ShowDialog() == true)
                      {
                          Budget budget = budgetWindow.Budget;                          
                          db.Budgets.Add(budget);
                          db.SaveChanges();
                      }
                  }));
            }
        }
        // команда редактирования
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand((selectedItem) =>
                  {
                      // получаем выделенный объект
                      Budget? budget = selectedItem as Budget;
                      if (budget == null) return;

                      Budget vm = new Budget
                      {
                          Id = budget.Id,
                          CompanyName = budget.CompanyName,
                          User= budget.User,
                          PostUser = budget.PostUser,
                          Amount = budget.Amount,
                          ProjectName = budget.ProjectName,

                      };
                      BudgetWindow budgetWindow = new BudgetWindow(vm);


                      if (budgetWindow.ShowDialog() == true)
                      {
                          budget.CompanyName = budgetWindow.Budget.CompanyName;
                          budget.ProjectName= budgetWindow.Budget.ProjectName;
                          budget.Amount= budgetWindow.Budget.Amount;
                          budget.User= budgetWindow.Budget.User;
                          budget.PostUser= budgetWindow.Budget.PostUser;                          
                          db.Entry(budget).State = EntityState.Modified;
                          db.SaveChanges();
                      }
                  }));
            }
        }
        // команда удаления
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand((selectedItem) =>
                  {
                      // получаем выделенный объект
                      Budget? budget = selectedItem as Budget;
                      if (budget == null) return;
                      db.Budgets.Remove(budget);
                      db.SaveChanges();
                  }));
            }
        }
    }
}


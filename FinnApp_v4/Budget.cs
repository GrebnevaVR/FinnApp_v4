using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FinnApp_v4
{
    public class Budget : INotifyPropertyChanged
    {
    
        string? companyname;
        int amount;
        string? user;
        string? postuser;
        string? projectname;
        public int Id { get; set; }
        public string? CompanyName
        {
            get { return companyname; }
            set
            {
                companyname = value;
                OnPropertyChanged("CompanyName");
            }
        }
        public string? ProjectName
        {
            get { return projectname; }
            set
            {
                projectname = value;
                OnPropertyChanged("ProjectName");
            }
        }
        public string? PostUser
        {
            get { return postuser; }
            set
            {
                postuser = value;
                OnPropertyChanged("PostUser");
            }
        }
        public string? User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }
        public int Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                OnPropertyChanged("Amount");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
            public void OnPropertyChanged([CallerMemberName] string prop = "")
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    
}

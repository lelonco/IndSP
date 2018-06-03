using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndSP
{
    [Serializable]
    class Employee : INotifyPropertyChanged
    {
        string name;
        string tNomber;
        Post post;
        DateTime birthdayDate;
        string salary;
        bool haveAward = false;
        public Employee()
        {
            this.name = "";
            this.tNomber = "";
            this.post = new Post();
            this.birthdayDate = DateTime.Now;
            this.salary = "0";
            this.haveAward = false;
        }

        public Employee(string name, string tNomber, Post post, DateTime birthdayDate, string salary, bool haveAward)
        {
            this.name = name;
            this.tNomber = tNomber;
            this.post = post;
            this.birthdayDate = birthdayDate;
            this.salary = salary;

        }

        public string Name
        {
            get { return name; }
            set
            {
                if (value != this.name)
                {
                    this.name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public string TNumber
        {
            get { return tNomber; }
            set
            {
                if (value != this.tNomber)
                {
                    this.tNomber = value;
                    OnPropertyChanged("TNumber");
                }
            }
        }
        public Post Post
        {
            get { return post; }
            set
            {
                if (value != this.post)
                {
                    this.post = value;
                    OnPropertyChanged("Post");
                }
            }
        }
        public DateTime BithdayDate
        {
            get { return birthdayDate; }
            set
            {
                if (value != this.birthdayDate)
                {
                    this.birthdayDate = value;
                    OnPropertyChanged("BithdayDate");
                }
            }
        }
        public string Salary
        {
            get { return salary; }
            set
            {
                if (value != this.salary)
                {
                    this.salary = value;
                    OnPropertyChanged("Salary");
                }
            }
        }
        public bool HaveAward
        {
            get { return haveAward; }
            set
            {
                if (this.haveAward != value)
                {
                    this.haveAward = value;
                    OnPropertyChanged("HaveAward");
                }
            }
        }
        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public override string ToString()
        {
            return String.Format("Табельний номер:{0} Ім'я:{1} Дата Народження:{2} Посада:{3} Оклад:{4} Наявність нагород:{5}", tNomber, name, birthdayDate, post, salary, haveAward);
        }
    }
}

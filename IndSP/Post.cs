using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndSP
{
    [Serializable]
    class Post : INotifyPropertyChanged
    {
        string postName;
        public Post() { }
        public string PostName
        {
            get { return postName; }
            set
            {
                if (this.postName != value)
                {
                    this.postName = value;
                    OnPropertyChanged("PostName");
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
            return postName;
        }
    }
}

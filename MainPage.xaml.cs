using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections;
using System.Threading;
using System.Windows.Threading;
using System.ComponentModel.DataAnnotations;

namespace GridTest
{
    public partial class MainPage : UserControl
    {


        public ObservableCollection<Item> Data { get; set; }

        public MainPage()
        {
            InitializeComponent();





            Data = new ObservableCollection<Item>();

            for (int i = 0; i < 10; i++)
            {
                var it = new Item() { Value = i, Name = "", IsReady = false };

                Data.Add(it);
            }


            this.DataContext = this;
        }




    }



    public class Item : INotifyPropertyChanged, INotifyDataErrorInfo
    {

        Dictionary<string, string> _errors = new Dictionary<string, string>();
        
        bool isNotifing = false;

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (value != name && isNotifing)
                {
                    name = value;
                    var propertyName = "Name";
                    ValidateAsync(propertyName, value);
                }
                isNotifing = true;
            }
        }



        

        private void ValidateAsync(string propertyName, string value)
        {

            if (propertyName == "Name")
            {
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += (sd, args) =>
                {
                    //simulate an async request
                    Thread.Sleep(1000);
                    args.Result = args.Argument;
                };

                bw.RunWorkerCompleted += (sd, args) =>
                {
                    var val = args.Result as string;

                    if (val != "VALID" && val != "")
                    {                        
                        _errors.Add("Name",  "Invalid Name" );                        
                    }
                    else
                    {
                        _errors.Remove(propertyName);
                    }
                    
                    NotifyErrorsChanged("Name");
                    NotifyPropertyChanged("HasErrors");                    

                };

                bw.RunWorkerAsync(value);

            }
        }


        public int Value { get; set; }
        public bool IsReady { get; set; }



        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            
            if (!String.IsNullOrWhiteSpace(propertyName) && _errors.ContainsKey(propertyName))
            {                
                return _errors.Values;
            }
            else
                return null;
        }

        public bool HasErrors
        {
            get { return _errors.Count > 0; }
        }



        private void NotifyErrorsChanged(string propertyName)
        {

            if (ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            }

        }


        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}

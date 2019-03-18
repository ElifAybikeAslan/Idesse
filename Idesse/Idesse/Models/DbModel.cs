using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Idesse.Models
{
    public class DbModel:INotifyPropertyChanged
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Hospital { get; set; }

        [MaxLength(255)]
        public string HospitalInformation { get; set; }

        // public FontAttributes fontAttributes { get; set; }
        private FontAttributes _fontAttributes;

        public FontAttributes fontAttributes
        {
            get
            {
                return _fontAttributes;
            }

            set
            {
                _fontAttributes = value;
                OnPropertyChanged("fontAttributes");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

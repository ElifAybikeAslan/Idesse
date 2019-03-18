using Idesse.Controls;
using Idesse.Helper;
using Idesse.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Idesse.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PersonInsertPage : ContentPage
	{

        public PersonInsertPage ()
		{
			InitializeComponent ();
		}

        public SelectableObservableCollection<DbModel> Items { get; set; }

        public void OnInsert(object sender,EventArgs e)
        {
            SQLiteManager manager = new SQLiteManager();
            DbModel _dbModel = new DbModel();
            _dbModel.Name = txtDoctorName.Text;
            _dbModel.Hospital = txtHospital.Text;
            _dbModel.fontAttributes = FontAttributes.None;
            _dbModel.HospitalInformation = txtHospitalInformation.Text;

            int isInserted = manager.Insert(_dbModel);
            if (isInserted > 0)
            {
                DisplayAlert("Başarılı", _dbModel.Name + "eklendi.", "Ok");
            }
            else
            {
                DisplayAlert("Başarısız", _dbModel.Name + "eklenmedi.", "Ok");
            }


        }

    }
}
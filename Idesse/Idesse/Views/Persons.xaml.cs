using Idesse.Controls;
using Idesse.Helper;
using Idesse.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Idesse.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Persons : ContentPage
	{
        SQLiteManager manager;
        List<DbModel> dbModel = new List<DbModel>();
        public SelectableObservableCollection<DbModel> Items { get; set; }
        public ICommand RemoveSelectedCommand { get; }
        private bool enableMultiSelect;

        public Persons ()
		{
			InitializeComponent ();
            
            manager = new SQLiteManager();
            enableMultiSelect = true;
            dbModel = manager.GetAll().ToList();
            Items = new SelectableObservableCollection<DbModel>(dbModel);
            Data();
            lstDB.ItemsSource = Items;
            RemoveSelectedCommand = new Command(OnRemoveSelected);
            BindingContext = this;
            

            /* listview refresh*/
            lstDB.RefreshCommand = new Command(() =>
            {
                OnUpdate();
                lstDB.IsRefreshing = false;
            });
        }

        /** Data */
        private void Data()
        {
            string documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = System.IO.Path.Combine(documentPath, App.DbName);
            var combined = Path.Combine(documentPath, path);
            var conn = new SQLite.SQLiteConnection(path);
            conn.Table<DbModel>();
            var dbModel = new DbModel();
            for (int i = 0; i < 100; i++)
            {
                dbModel.Name = "Ahmet Çetin Dirgen";
                dbModel.Hospital = "Özel Medline Adana Hastanesi ";
                dbModel.HospitalInformation = "Bu Ay: 6/3 | YTD: 5/0 | Ay.Ort. 0/4";
                conn.Insert(dbModel);
            }
        }

        /**  Multi select enable */
        public bool EnableMultiSelect
        {
            get { return enableMultiSelect; }
            set
            {
                enableMultiSelect = value;
                OnPropertyChanged();
            }
        }

        /** remove item */
        private void OnRemoveSelected()
        {
            var selectedItems = Items.SelectedItems.ToArray();
            Task<bool> action = DisplayAlert("", "Kaydı silmek istediğinize emin misiniz?", "Evet", "Hayır");
            action.ContinueWith(task =>
            {
                if (task.Result)
                    foreach (var item in selectedItems)
                    {
                        manager.Delete(item.Id);
                    }
            });
        }
        
        /** update item */
        private void OnUpdate()
        {
            List<DbModel> dBItems = manager.GetAll().ToList();
            Items = new SelectableObservableCollection<DbModel>(dBItems);
            lstDB.ItemsSource = Items;
            BindingContext = this;
        }

        /** Person insert page */
        private void OnInsertMenu(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PersonInsertPage());
        }

        /**  Calendar */
        private void Planla_Clicked(object sender, EventArgs e)
        {
            datepicker.Focus();
        }

        /**  Bottom menu activation */
        private void Person_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            menu.IsVisible = true;
        }

        /**  SearchBar */
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyboard = PersonSearchBar.Text;
            var dBItems = manager.GetAll().Where(x => x.Name.ToLower().Contains(keyboard.ToLower())).ToList();
            var result = dBItems.Count();
            if (result > 0)
            {
                Items = new SelectableObservableCollection<DbModel>(dBItems);
                lstDB.ItemsSource = Items;
            }
        }

        /** Bold Selected */
        private void OnBoldSelected_Clicked(object sender, EventArgs e)
        {
            var selectedItems = Items.SelectedItems.ToArray();
            foreach (var item in selectedItems)
            {
                item.fontAttributes = FontAttributes.Bold;
            }
        }

        /** Bottom bar back button */
        private void Back_Clicked(object sender, EventArgs e)
        {
            menu.IsVisible = false;
        }
    }
}
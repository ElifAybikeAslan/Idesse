using Idesse.MenuItems;
using Idesse.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Idesse
{
    public partial class MainPage : MasterDetailPage
    {
        public List<MasterPageItem> menuList { get; set; }

        public MainPage()
        {
            InitializeComponent();

            menuList = new List<MasterPageItem>();

            var pagePerson = new MasterPageItem() { Title = "Kişiler", Icon = "person.png", TargetType = typeof(Persons) };
            var pageSettings = new MasterPageItem() { Title = "Ayarlar", Icon = "settings.png", TargetType = typeof(Settings) };



            menuList.Add(pagePerson);
            menuList.Add(pageSettings);


            navigationDrawerList.ItemsSource = menuList;

            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Persons)));

            this.BindingContext = new
            {
                Header = "",
                Image = "profileImage.png",
                Footer = "Elif Aybike Aslan"
            };
        }

        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;

            Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            IsPresented = false;
        }
    }
}

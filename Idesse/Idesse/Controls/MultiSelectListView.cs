using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Idesse.Controls
{
    public static class MultiSelectListView
    {
        public static readonly BindableProperty IsMultiSelectProperty =
            BindableProperty.CreateAttached("IsMultiSelect", typeof(bool), typeof(ListView), false, propertyChanged: OnIsMultiSelectChanged);

        public static bool GetIsMultiSelect(BindableObject view) => (bool)view.GetValue(IsMultiSelectProperty);

        public static void SetIsMultiSelect(BindableObject view, bool value) => view.SetValue(IsMultiSelectProperty, value);

        private static void OnIsMultiSelectChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var listView = bindable as ListView;
            if (listView != null)
            {

                listView.ItemSelected -= OnItemSelected;


                if (true.Equals(newValue))
                {
                    listView.ItemSelected += OnItemSelected;
                }
            }
        }

        private static void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as SelectableItem;
            if (item != null)
            {

                item.IsSelected = !item.IsSelected;
            }


            ((ListView)sender).SelectedItem = null;
        }
    }
}

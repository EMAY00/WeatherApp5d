using System;
using FiveDayWeatherApp;
using Android.App;
using Android.OS;
using Android.Widget;
using FiveDayWeatherApp.Core;

namespace FiveDayWeatherApp
{
    [Activity(Label = "WeekForecastActivity")]
    public class WeekForecastActivity : Activity
    {
        ListView list;
        SearchView searchBar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Weather_list);

            list = FindViewById<ListView>(Resource.Id.listView1);
            searchBar = FindViewById<SearchView>(Resource.Id.searchView1);

            var button = FindViewById<Button>(Resource.Id.button1);

            button.Click += Button_Click;
        }


        private async void Button_Click(object sender, EventArgs e)
        {
            string location = (searchBar.Query).ToString();
            var forecasts = await Core.Core.GetFiveDaysWeather(location);

            if (forecasts != null)
            {
                list.Adapter = new CustomAdapter(this, forecasts);
            }
        }
    }
}
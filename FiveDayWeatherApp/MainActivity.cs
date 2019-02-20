using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

using System;
using Android.Content;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter;

namespace FiveDayWeatherApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/MyTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button search;
        EditText input;
        TextView temperature;
        TextView pressure;
        TextView windSpeed;
        ImageView weatherIcon;
        ProgressBar progressBar;
        Button forecast;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            AppCenter.Start("002f8002-3c02-43a5-85fb-8d217f818bdf", typeof(Analytics), typeof(Crashes));

            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            search = FindViewById<Button>(Resource.Id.search);
            input = FindViewById<EditText>(Resource.Id.input);
            temperature = FindViewById<TextView>(Resource.Id.temperature);
            pressure = FindViewById<TextView>(Resource.Id.pressure);
            windSpeed = FindViewById<TextView>(Resource.Id.windSpeed);
            weatherIcon = FindViewById<ImageView>(Resource.Id.weatherIcon);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
            forecast = FindViewById<Button>(Resource.Id.forecast);
            var toolbar = FindViewById<Toolbar>(Resource.Layout.Toolbar);
            SetActionBar(toolbar);

            search.Click += Button_Click;
            forecast.Click += ForeCast_Click;

            SwapWeather();
            SwapProgressBar();
        }

        private void ForeCast_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(WeekForecastActivity));
            intent.PutExtra("input", input.Text);
            StartActivity(intent);
        }

        private async void Button_Click(object sender, System.EventArgs e)
        {
            if (temperature.Visibility == Android.Views.ViewStates.Visible)
                SwapWeather();

            SwapProgressBar();
            var weather = await Core.GetWeather(input.Text);
            if (weather != null)
            {
                temperature.Text = weather.Temperature;
                pressure.Text = weather.Pressure;
                windSpeed.Text = weather.WindSpeed;

                weatherIcon.SetImageResource(Resources.GetIdentifier(weather.ImageName, "drawable", PackageName));
                SwapWeather();
            }

            SwapProgressBar();
        }

        private void SwapWeather()
        {
            if (temperature.Visibility == Android.Views.ViewStates.Invisible)
            {
                temperature.Visibility = Android.Views.ViewStates.Visible;
                pressure.Visibility = Android.Views.ViewStates.Visible;
                windSpeed.Visibility = Android.Views.ViewStates.Visible;
                weatherIcon.Visibility = Android.Views.ViewStates.Visible;
                forecast.Visibility = Android.Views.ViewStates.Visible;
            }
            else
            {
                temperature.Visibility = Android.Views.ViewStates.Invisible;
                pressure.Visibility = Android.Views.ViewStates.Invisible;
                windSpeed.Visibility = Android.Views.ViewStates.Invisible;
                weatherIcon.Visibility = Android.Views.ViewStates.Invisible;
                forecast.Visibility = Android.Views.ViewStates.Invisible;
            }
        }

        private void SwapProgressBar()
        {
            if (progressBar.Visibility == Android.Views.ViewStates.Invisible)
                progressBar.Visibility = Android.Views.ViewStates.Visible;
            else
                progressBar.Visibility = Android.Views.ViewStates.Invisible;
        }
    }
}
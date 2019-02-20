using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using FiveDayWeatherApp.Core;

namespace FiveDayWeatherApp
{
    public class CustomAdapter : BaseAdapter<Forecast>
    {
        List<Forecast> items;
        Activity context;
      

        public CustomAdapter(Activity context, List<Forecast> items) : base()
        {
            this.context = context;
            this.items = items;
        }

 
        public override Forecast this[int position]
        {
            get { return items[position]; }
        }

        public override int Count { get { return items.Count; } }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            string picture = items[position].Description;
            View view = convertView;
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.CustomRow, null);
          
            view.FindViewById<TextView>(Resource.Id.dateTxt).Text = items[position].Date;
            switch (picture)
            {
                case ("01d"):
                    view.FindViewById<ImageView>(Resource.Id.weatherPicture).SetImageResource(Resource.Drawable.sun);
                    break;

                case ("02d"):
                    view.FindViewById<ImageView>(Resource.Id.weatherPicture).SetImageResource(Resource.Drawable.CloudAndSun);
                    break;

                case ("04d"):
                    view.FindViewById<ImageView>(Resource.Id.weatherPicture).SetImageResource(Resource.Drawable.cloud);
                    break;

                case ("10d"):
                    view.FindViewById<ImageView>(Resource.Id.weatherPicture).SetImageResource(Resource.Drawable.rain);
                    break;

                case ("13d"):
                    view.FindViewById<ImageView>(Resource.Id.weatherPicture).SetImageResource(Resource.Drawable.snow);
                    break;

                case ("50d"):
                    view.FindViewById<ImageView>(Resource.Id.weatherPicture).SetImageResource(Resource.Drawable.Fog);
                    break;

            }
            view.FindViewById<TextView>(Resource.Id.tempMax).Text = items[position].maxTemperature;
            view.FindViewById<TextView>(Resource.Id.tempMin).Text = " / " + items[position].minTemperature;

            return view;

        }
    }
}
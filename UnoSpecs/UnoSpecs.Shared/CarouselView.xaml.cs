using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace UnoSpecs
{
	public sealed partial class CarouselView : UserControl
	{
		public static readonly DependencyProperty ItemsSourceProperty = FlipView.ItemsSourceProperty;

		public object ItemsSource
		{
			get => flipView.ItemsSource;
			set
			{
				var old = flipView.ItemsSource;
				flipView.ItemsSource = value;

				Count = flipView.Items?.Count ?? 0;
				if (old is INotifyCollectionChanged oldCollectionChanged)
				{
					oldCollectionChanged.CollectionChanged -= OnCollectionChanged;
				}
				if (value is INotifyCollectionChanged collectionChanged)
				{
					collectionChanged.CollectionChanged += OnCollectionChanged;
				}
			}
		}

		void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			Count = flipView.Items?.Count ?? 0;
		}

		public int Count
		{
			get { return (int)GetValue(CountProperty); }
			set { SetValue(CountProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Count.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty CountProperty =
			DependencyProperty.Register("Count", typeof(int), typeof(CarouselView), new PropertyMetadata(0));

		public CarouselView()
		{
			this.InitializeComponent();
		}
	}
}

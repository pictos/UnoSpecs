using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UnoSpecs;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainPage : Page
{
	public ObservableCollection<object> Items { get; } = new(new[] { "Hello", "there", "!" });

	public MainPage()
	{
		this.InitializeComponent();
		DataContext = this;

		//Can use the C# methods directly

		btn.Click += (_, __) =>
		{
			FliperAttachedProperty.GoNext(pipsPager2);
		};

		btnBack.Click += (_, ___) =>
		{
			this.Frame.GoBack();
		};
	}

}


//class FirstPage : Page
//{
//	public FirstPage()
//	{
//		var btn = new Button
//		{
//			VerticalAlignment = VerticalAlignment.Center,
//			Content = "Go back",
//		};

//		btn.Click += (_, __) =>
//		{
//			this.Frame.Navigate(typeof(MainPage));
//		};

//		Content = btn;
//	}
//}
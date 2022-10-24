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
	public ObservableCollection		<object> Items { get; } = new(new[] { "Hello", "there", "!" });

	public MainPage()
	{
		this.InitializeComponent();
		DataContext = this;
		//btn.Click += (_, __) =>
		//{
		//	if (Random.Shared.Next(0, 10) % 2 == 0)
		//		Items.Add($"New value {Items.Count}");
		//	else
		//		Items.RemoveAt(0);
		//};

		//flipView.ManipulationDelta += FlipView_ManipulationDelta;


		btn.Click += (_, __) =>
		{
			var grid = new Grid();

			var bt1 = new Button
			{
				Content = "Previous",
				HorizontalAlignment = HorizontalAlignment.Left
			};
			//FliperAttachedProperty.SetPrevious(bt1, flipView);


			var bt2 = new Button
			{
				Content = "Next",
				HorizontalAlignment = HorizontalAlignment.Right
			};
			FliperAttachedProperty.SetNext(bt2, flipView);

			grid.Children.Add(bt1);
			grid.Children.Add(bt2);

			flipView.Items.Add(grid);
		};
	}

	void FlipView_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
	{
		if (e.Delta.Translation.X != 0)
		{
			e.Handled = true;
		}
	}
}

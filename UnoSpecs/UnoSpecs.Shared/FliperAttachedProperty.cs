using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;

namespace UnoSpecs
{
	partial class FliperAttachedProperty : DependencyObject
	{
		public static readonly DependencyProperty FlipOwnerProperty =
			DependencyProperty.RegisterAttached("FlipOwner", typeof(FlipView), typeof(FliperAttachedProperty), new PropertyMetadata(null, OnDataChanged));

		static void OnDataChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			if (args.NewValue == args.OldValue && args.NewValue is null)
				return;

			var flipView = (FlipView)args.NewValue;

			var pipsPager = (PipsPager)dependencyObject;


			// For some reason Binding doesn't work here
			//var itemsCountBinding = new Binding
			//{
			//	Mode = BindingMode.OneWay,
			//	Source = flipView.Items,
			//	Path = new("Count")
			//};

			flipView.Items.VectorChanged += (_, __) =>
			{
				pipsPager.NumberOfPages = flipView.Items.Count;
			};

			var selectedIndexBinding = new Binding
			{
				Mode = BindingMode.TwoWay,
				Source = flipView,
				Path = new PropertyPath(nameof(flipView.SelectedIndex))
			};

			pipsPager.SetBinding(PipsPager.SelectedPageIndexProperty, selectedIndexBinding);
			//pipsPager.SetBinding(PipsPager.NumberOfPagesProperty, itemsCountBinding);

			pipsPager.NumberOfPages = flipView.Items.Count;
		}

		public static void SetFlipOwner(UIElement element, FlipView value) =>
			element.SetValue(FlipOwnerProperty, value);

		public static FlipView GetFlipOwner(UIElement element) => (FlipView)element.GetValue(FlipOwnerProperty);
	}
}

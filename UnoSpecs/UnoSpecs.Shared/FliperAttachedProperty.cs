using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using System;
using System.Windows.Input;

namespace UnoSpecs;

partial class FliperAttachedProperty : DependencyObject
{
	public static readonly DependencyProperty PipsPagerProperty =
		DependencyProperty.RegisterAttached("PipsPager", typeof(PipsPager), typeof(FliperAttachedProperty), new PropertyMetadata(null, OnPipsPagerChanged));

	static void OnPipsPagerChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
	{
		if (args.NewValue == args.OldValue && args.NewValue is null)
			return;

		var flipView = (FlipView)dependencyObject;

		var pipsPager = (PipsPager)args.NewValue;

		// For some reason Binding doesn't work here
		//var itemsCountBinding = new Binding
		//{
		//	Mode = BindingMode.OneWay,
		//	Source = flipView.Items,
		//	Path = new("Count")
		//};

		// can this cause memory leaks?
		// Should we have some WeakEventManager?
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

	public static void SetFlipOwner(FlipView element, PipsPager value) =>
		element.SetValue(PipsPagerProperty, value);

	public static PipsPager GetFlipOwner(FlipView element) => (PipsPager)element.GetValue(PipsPagerProperty);

	public static void GoNext(UIElement element)
	{
		var flipView = (FlipView)element;

		var index = flipView.SelectedIndex;
		index++;
		if (index >= flipView.Items.Count)
			return;

		flipView.SelectedIndex = index;
	}
	public static readonly DependencyProperty NextProperty =
		DependencyProperty.RegisterAttached("Next", typeof(FlipView), typeof(FliperAttachedProperty), new PropertyMetadata(null, OnNextChanged));

	static void OnNextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is not Button btn || e.NewValue is null)
			return;

		var flipView = (FlipView)e.NewValue;

		btn.Click -= Btn_Click;
		btn.Click += Btn_Click;
		
		void Btn_Click(object sender, RoutedEventArgs e)
		{
			GoNext(flipView);
		}
	}


	public static void SetNext(Button element, FlipView value) => element.SetValue(NextProperty, value);

	public static FlipView GetNext(Button element) => (FlipView)element.GetValue(NextProperty);

	

	public static void GoBack(UIElement element)
	{
		var flipView = (FlipView)element;

		var index = flipView.SelectedIndex;
		index--;
		if (index < 0)
			return;

		flipView.SelectedIndex = index;
	}

	//public static void OnNextCommand(Button nextButton)
	//{

	//	new Command(_ => GoNext(flipView));
	//}

	//public static ICommand OnBackCommand(FlipView element) =>
	//new Command((o) => GoBack(element));
}


// Just a silly ICommand implementation
public class Command : ICommand
{
	private Action<object> action;
	private bool canExecute;
	public event EventHandler CanExecuteChanged;

	public Command(Action<object> action, bool canExecute)
	{
		this.action = action;
		this.canExecute = canExecute;
	}


	public Command(Action<object> action)
		: this(action, true)
	{
	}

	public bool CanExecute(object parameter)
	{
		return canExecute;
	}

	public void Execute(object parameter)
	{
		action(parameter);
	}
}

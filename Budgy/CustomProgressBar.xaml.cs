using Microsoft.Maui.Controls.Shapes;

namespace Budgy;

public partial class CustomProgressBar : ContentView
{
	public static readonly BindableProperty ProgressProperty =
		BindableProperty.Create(
			nameof(Progress),
			typeof(double),
			typeof(CustomProgressBar),
			defaultValue: 0.0,
			propertyChanged: (bindable, oldValue, newValue) => ((CustomProgressBar)bindable).UpdateWidth());

	public static readonly BindableProperty ProgressColorProperty =
		BindableProperty.Create(
			nameof(ProgressColor),
			typeof(Color),
			typeof(CustomProgressBar),
			defaultValue: Colors.White);

	public static readonly BindableProperty CornerRadiusProperty =
		BindableProperty.Create(
			nameof(CornerRadius),
			typeof(double),
			typeof(CustomProgressBar),
			defaultValue: 5.0);

	public double Progress
	{
		get => (double)GetValue(ProgressProperty);
		set => SetValue(ProgressProperty, value);
	}

	public Color ProgressColor
	{
		get => (Color)GetValue(ProgressColorProperty);
		set => SetValue(ProgressColorProperty, value);
    }

	public double CornerRadius
	{
		get => (double)GetValue(CornerRadiusProperty);
		set => SetValue(CornerRadiusProperty, value);
    }


	public CustomProgressBar()
	{
		InitializeComponent();
	}

    private void Border_SizeChanged(object sender, EventArgs e)
    {
		UpdateWidth();
    }

	private void UpdateWidth()
	{
		

        if (ProgressBarGrid == null || LeftColumn == null || RightColumn == null)
            return;

        double clampedProgress = Math.Clamp(Progress, 0.0, 1.0);

        // Wir verändern einfach die Sternchen-Breiten des Grids
        LeftColumn.Width = new GridLength(clampedProgress, GridUnitType.Star);
        RightColumn.Width = new GridLength(1.0 - clampedProgress, GridUnitType.Star);
    }
}
using System;
using Microsoft.Maui.Primitives;

namespace Microsoft.Maui.Layouts
{
	public static class LayoutExtensions
	{
		public static Size ComputeDesiredSize(this IFrameworkElement frameworkElement, double widthConstraint, double heightConstraint)
		{
			_ = frameworkElement ?? throw new ArgumentNullException(nameof(frameworkElement));

			if (frameworkElement.Handler == null)
			{
				return Size.Zero;
			}

			var margin = frameworkElement.GetMargin();

			// Adjust the constraints to account for the margins
			widthConstraint -= margin.HorizontalThickness;
			heightConstraint -= margin.VerticalThickness;

			// Determine whether the external constraints or the requested size values will determine the measurements
			widthConstraint = LayoutManager.ResolveConstraints(widthConstraint, frameworkElement.Width);
			heightConstraint = LayoutManager.ResolveConstraints(heightConstraint, frameworkElement.Height);

			// Ask the handler to do the actual measuring								
			var measureWithMargins = frameworkElement.Handler.GetDesiredSize(widthConstraint, heightConstraint);

			// Account for the margins when reporting the desired size value
			return new Size(measureWithMargins.Width + margin.HorizontalThickness,
				measureWithMargins.Height + margin.VerticalThickness);
		}

		public static Rectangle ComputeFrame(this IFrameworkElement frameworkElement, Rectangle bounds)
		{
			Thickness margin = frameworkElement.GetMargin();

			var frameWidth = frameworkElement.HorizontalLayoutAlignment == LayoutAlignment.Fill
				? Math.Max(0, bounds.Width - margin.HorizontalThickness)
				: frameworkElement.DesiredSize.Width;

			var frameHeight = frameworkElement.VerticalLayoutAlignment == LayoutAlignment.Fill
				? Math.Max(0, bounds.Height - margin.VerticalThickness)
				: frameworkElement.DesiredSize.Height;

			var frameX = AlignHorizontal(frameworkElement, bounds, margin);
			var frameY = AlignVertical(frameworkElement, bounds, margin);

			return new Rectangle(frameX, frameY, frameWidth, frameHeight);
		}

		static Thickness GetMargin(this IFrameworkElement frameworkElement)
		{
			if (frameworkElement is IView view)
				return view.Margin;

			return Thickness.Zero;
		}

		static double AlignHorizontal(IFrameworkElement frameworkElement, Rectangle bounds, Thickness margin) 
		{
			double frameX = 0;

			switch (frameworkElement.HorizontalLayoutAlignment)
			{
				case LayoutAlignment.Fill:

					frameX = bounds.X + margin.Left;
					break;

				case LayoutAlignment.Start:

					// TODO ezhart Once https://github.com/dotnet/maui/pull/415 is merged this needs to account for FlowDirection
					frameX = bounds.X + margin.Left;
					break;

				case LayoutAlignment.Center:

					var widthWithMargin = frameworkElement.DesiredSize.Width + margin.HorizontalThickness;
					frameX = (bounds.Width - widthWithMargin) / 2;
					break;

				case LayoutAlignment.End:

					// TODO ezhart Once https://github.com/dotnet/maui/pull/415 is merged this needs to account for FlowDirection
					frameX = bounds.Width - margin.Right - frameworkElement.DesiredSize.Width;
					break;
			}

			return frameX;
		}

		static double AlignVertical(IFrameworkElement frameworkElement, Rectangle bounds, Thickness margin) 
		{
			double frameY = 0;

			switch (frameworkElement.VerticalLayoutAlignment)
			{
				case LayoutAlignment.Fill:

					frameY = bounds.Y + margin.Top;
					break;

				case LayoutAlignment.Start:

					frameY = bounds.Y + margin.Top;
					break;

				case LayoutAlignment.Center:

					var heightWithMargin = frameworkElement.DesiredSize.Height + margin.VerticalThickness;
					frameY = (bounds.Height - heightWithMargin) / 2;
					break;

				case LayoutAlignment.End:

					frameY = bounds.Height - margin.Bottom - frameworkElement.DesiredSize.Height;
					break;
			}

			return frameY;
		}
	}
}

using Microsoft.Maui.Layouts;
using NSubstitute;
using Xunit;
using Microsoft.Maui.Primitives;
using System.Collections.Generic;

namespace Microsoft.Maui.UnitTests.Layouts
{
	[Category(TestCategory.Core, TestCategory.Layout)]
	public class LayoutExtensionTests
	{
		[Fact]
		public void FrameExcludesMargin()
		{
			var element = Substitute.For<IView>();
			var margin = new Thickness(20);
			element.Margin.Returns(margin);

			var bounds = new Rectangle(0, 0, 100, 100);
			var frame = element.ComputeFrame(bounds);

			// With a margin of 20 all the way around, we expect the actual frame
			// to be 60x60, with an x,y position of 20,20

			Assert.Equal(20, frame.Top);
			Assert.Equal(20, frame.Left);
			Assert.Equal(60, frame.Width);
			Assert.Equal(60, frame.Height);
		}

		[Theory]
		[InlineData(LayoutAlignment.Fill)]
		[InlineData(LayoutAlignment.Start)]
		[InlineData(LayoutAlignment.Center)]
		[InlineData(LayoutAlignment.End)]
		public void FrameSizeGoesToZeroWhenMarginsExceedBounds(LayoutAlignment layoutAlignment)
		{
			var element = Substitute.For<IView>();
			var margin = new Thickness(200);
			element.Margin.Returns(margin);
			element.HorizontalLayoutAlignment.Returns(layoutAlignment);
			element.VerticalLayoutAlignment.Returns(layoutAlignment);

			var bounds = new Rectangle(0, 0, 100, 100);
			var frame = element.ComputeFrame(bounds);

			// The margin is simply too large for the bounds; since negative widths/heights on a frame don't make sense,
			// we expect them to collapse to zero

			Assert.Equal(0, frame.Height);
			Assert.Equal(0, frame.Width);
		}

		[Fact]
		public void DesiredSizeIncludesMargin()
		{
			var widthConstraint = 400;
			var heightConstraint = 655;

			var handler = Substitute.For<IViewHandler>();
			var element = Substitute.For<IView>();
			var margin = new Thickness(20);

			// Our "native" control will request a size of (100,50) when we call GetDesiredSize
			handler.GetDesiredSize(Arg.Any<double>(), Arg.Any<double>()).Returns(new Size(100, 50));
			element.Handler.Returns(handler);
			element.Margin.Returns(margin);

			var desiredSize = element.ComputeDesiredSize(widthConstraint, heightConstraint);

			// Because the actual ("native") measurement comes back with (100,50)
			// and the margin on the IFrameworkElement is 20, the expected width is (100 + 20 + 20) = 140
			// and the expected height is (50 + 20 + 20) = 90

			Assert.Equal(140, desiredSize.Width);
			Assert.Equal(90, desiredSize.Height);
		}

		public static IEnumerable<object[]> AlignmentTestData() 
		{
			yield return new object[] { LayoutAlignment.Start, 0, 100, Thickness.Zero };
			yield return new object[] { LayoutAlignment.Center, 100, 100, Thickness.Zero };
			yield return new object[] { LayoutAlignment.End, 200, 100, Thickness.Zero };
			yield return new object[] { LayoutAlignment.Fill, 0, 300, Thickness.Zero };

			// Even margin
			var margin = new Thickness(10);
			yield return new object[] { LayoutAlignment.Start, 10, 100, margin };
			yield return new object[] { LayoutAlignment.Center, 90, 100, margin };
			yield return new object[] { LayoutAlignment.End, 190, 100, margin };
			yield return new object[] { LayoutAlignment.Fill, 10, 280, margin };

			// Lopsided margin
			margin = new Thickness(5, 5, 10, 10);
			yield return new object[] { LayoutAlignment.Start, 5, 100, margin };
			yield return new object[] { LayoutAlignment.Center, 92.5, 100, margin };
			yield return new object[] { LayoutAlignment.End, 190, 100, margin };
			yield return new object[] { LayoutAlignment.Fill, 5, 285, margin };
		}

		[Theory]
		[MemberData(nameof(AlignmentTestData))]
		public void FrameAccountsForHorizontalLayoutAlignment(LayoutAlignment layoutAlignment, double x, double width,
			Thickness margin) 
		{
			var widthConstraint = 300;
			var heightConstraint = 50;
			var viewSize = new Size(100, 50);

			var element = Substitute.For<IView>();

			element.Margin.Returns(margin);
			element.DesiredSize.Returns(viewSize);
			element.HorizontalLayoutAlignment.Returns(layoutAlignment);

			var frame = element.ComputeFrame(new Rectangle(0, 0, widthConstraint, heightConstraint));

			Assert.Equal(x, frame.Left);
			Assert.Equal(width, frame.Width);
		}

		[Theory]
		[MemberData(nameof(AlignmentTestData))]
		public void FrameAccountsForVerticalLayoutAlignment(LayoutAlignment layoutAlignment, double y, double height, 
			Thickness margin)
		{
			var widthConstraint = 50;
			var heightConstraint = 300;
			var viewSize = new Size(50, 100);

			var element = Substitute.For<IView>();

			element.Margin.Returns(margin);
			element.DesiredSize.Returns(viewSize);
			element.VerticalLayoutAlignment.Returns(layoutAlignment);

			var frame = element.ComputeFrame(new Rectangle(0, 0, widthConstraint, heightConstraint));

			Assert.Equal(y, frame.Top);
			Assert.Equal(height, frame.Height);
		}
	}
}

using Maui.Controls.Sample.Controls;
using Microsoft.Maui;
using Microsoft.Extensions.DependencyInjection;

namespace Maui.Controls.Sample
{
	public class MainWindow : Window
	{
		public MainWindow() : this(App.Current.Services.GetRequiredService<IPage>())
		{
		}

		public MainWindow(IPage page)
		{
			Page = page;
		}
	}
}

using System.Threading.Tasks;
using Microsoft.Maui.DeviceTests.Stubs;
using Microsoft.Maui.Handlers;
using UIKit;
using Xunit;

namespace Microsoft.Maui.DeviceTests
{
	public partial class SearchBarHandlerTests
	{
		[Fact(DisplayName = "CharacterSpacing Initializes Correctly")]
		public async Task CharacterSpacingInitializesCorrectly()
		{
			string originalText = "Test";
			var xplatCharacterSpacing = 4;

			var slider = new SearchBarStub()
			{
				CharacterSpacing = xplatCharacterSpacing,
				Text = originalText
			};

			var values = await GetValueAsync(slider, (handler) =>
			{
				return new
				{
					ViewValue = slider.CharacterSpacing,
					NativeViewValue = GetNativeText(handler)
				};
			});

			Assert.Equal(xplatCharacterSpacing, values.ViewValue);
			Assert.NotEqual(originalText, values.NativeViewValue);
		}

		UISearchBar GetNativeEntry(SearchBarHandler searchBarHandler) =>
			(UISearchBar)searchBarHandler.View;

		string GetNativeText(SearchBarHandler searchBarHandler) =>
			GetNativeEntry(searchBarHandler).Text;
	}
}
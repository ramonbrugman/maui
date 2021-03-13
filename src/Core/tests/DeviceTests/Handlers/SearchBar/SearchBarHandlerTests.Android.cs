using System.Linq;
using System.Threading.Tasks;
using Android.Widget;
using Microsoft.Maui.DeviceTests.Stubs;
using Microsoft.Maui.Handlers;
using Xunit;
using SearchView = AndroidX.AppCompat.Widget.SearchView;

namespace Microsoft.Maui.DeviceTests
{
	public partial class SearchBarHandlerTests
	{
		[Fact(DisplayName = "CharacterSpacing Initializes Correctly")]
		public async Task CharacterSpacingInitializesCorrectly()
		{
			var xplatCharacterSpacing = 4;

			var searchBar = new SearchBarStub()
			{
				CharacterSpacing = xplatCharacterSpacing,
				Text = "Test"
			};

			float expectedValue = searchBar.CharacterSpacing.ToEm();

			var values = await GetValueAsync(searchBar, (handler) =>
			{
				return new
				{
					ViewValue = searchBar.CharacterSpacing,
					NativeViewValue = GetNativeCharacterSpacing(handler)
				};
			});

			Assert.Equal(xplatCharacterSpacing, values.ViewValue);
			Assert.Equal(expectedValue, values.NativeViewValue, EmCoefficientPrecision);
		}

		SearchView GetNativeSearchBar(SearchBarHandler searchBarHandler) =>
			(SearchView)searchBarHandler.View;

		string GetNativeText(SearchBarHandler searchBarHandler) =>
			GetNativeSearchBar(searchBarHandler).Query;

		double GetNativeCharacterSpacing(SearchBarHandler searchBarHandler)
		{
			var searchView = GetNativeSearchBar(searchBarHandler);
			var editText = searchView.GetChildrenOfType<EditText>().FirstOrDefault();

			if (editText != null)
			{
				return editText.LetterSpacing;
			}

			return -1;
		}

		string GetNativePlaceholder(SearchBarHandler searchBarHandler) =>
			GetNativeSearchBar(searchBarHandler).QueryHint;
	}
}
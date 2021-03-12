using System.Linq;
using Android.Widget;
using SearchView = AndroidX.AppCompat.Widget.SearchView;

namespace Microsoft.Maui
{
	public static class SearchBarExtensions
	{
		public static void UpdateText(this SearchView searchView, ISearchBar searchBar)
		{
			searchView.SetQuery(searchBar.Text, false);
		}

		public static void UpdateCharacterSpacing(this SearchView searchView, ISearchBar searchBar)
		{
			searchView.UpdateCharacterSpacing(searchBar, null);
		}

		public static void UpdateCharacterSpacing(this SearchView searchView, ISearchBar searchBar, EditText? editText)
		{
			editText ??= searchView.GetChildrenOfType<EditText>().FirstOrDefault();

			if (editText != null)
			{
				editText.LetterSpacing = searchBar.CharacterSpacing.ToEm();
			}
		}
	}
}

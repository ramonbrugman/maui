using UIKit;

namespace Microsoft.Maui
{
	public static class SearchBarExtensions
	{
		public static void UpdateText(this UISearchBar uiSearchBar, ISearchBar searchBar)
		{
			uiSearchBar.Text = searchBar.Text;
		}

		public static void UpdateCharacterSpacing(this UISearchBar uiSearchBar, ISearchBar searchBar)
		{
			uiSearchBar.UpdateCharacterSpacing(searchBar, null);
		}

		public static void UpdateCharacterSpacing(this UISearchBar uiSearchBar, ISearchBar searchBar, UITextField? textField)
		{
			textField ??= uiSearchBar.FindDescendantView<UITextField>();

			if (textField == null)
				return;

			textField.AttributedText = textField.AttributedText?.AddCharacterSpacing(searchBar.Text, searchBar.CharacterSpacing);
		}
	}
}
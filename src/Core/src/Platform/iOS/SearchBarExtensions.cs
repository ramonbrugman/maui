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

			var textAttr = textField.AttributedText?.WithCharacterSpacing(searchBar.CharacterSpacing);

			if (textAttr != null)
				textField.AttributedText = textAttr;
		}

		public static void UpdatePlaceholder(this UISearchBar uiSearchBar, ISearchBar searchBar)
		{
			uiSearchBar.Placeholder = searchBar.Placeholder;
		}
	}
}
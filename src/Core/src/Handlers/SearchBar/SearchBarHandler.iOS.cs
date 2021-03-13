using UIKit;

namespace Microsoft.Maui.Handlers
{
	public partial class SearchBarHandler : AbstractViewHandler<ISearchBar, UISearchBar>
	{
		static UITextField? TextField;

		protected override UISearchBar CreateNativeView() => new UISearchBar();

		protected override void SetupDefaults(UISearchBar nativeView)
		{
			TextField ??= nativeView.FindDescendantView<UITextField>();
		}

		public static void MapText(SearchBarHandler handler, ISearchBar searchBar)
		{
			handler.TypedNativeView?.UpdateText(searchBar);
		}

		public static void MapCharacterSpacing(SearchBarHandler handler, ISearchBar searchBar)
		{
			handler.TypedNativeView?.UpdateCharacterSpacing(searchBar, TextField);
		}

		public static void MapPlaceholder(SearchBarHandler handler, ISearchBar searchBar)
		{
			handler.TypedNativeView?.UpdatePlaceholder(searchBar);
		}
	}
}
using System.Linq;
using Android.Widget;
using SearchView = AndroidX.AppCompat.Widget.SearchView;

namespace Microsoft.Maui.Handlers
{
	public partial class SearchBarHandler : AbstractViewHandler<ISearchBar, SearchView>
	{
		static EditText? EditText;

		protected override SearchView CreateNativeView()
		{
			return new SearchView(Context);
		}

		protected override void SetupDefaults(SearchView nativeView)
		{
			EditText ??= nativeView.GetChildrenOfType<EditText>().FirstOrDefault();
		}

		public static void MapText(SearchBarHandler handler, ISearchBar searchBar)
		{
			handler.TypedNativeView?.UpdateText(searchBar);
		}

		public static void MapCharacterSpacing(SearchBarHandler handler, ISearchBar searchBar)
		{
			handler.TypedNativeView?.UpdateCharacterSpacing(searchBar, EditText);
		}
	}
}
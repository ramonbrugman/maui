namespace Microsoft.Maui
{
	/// <summary>
	/// Represents a View used to initiating a search.
	/// </summary>
	public interface ISearchBar : IView, IPlaceholder // ITextInput, IText
	{
		/// <summary>
		/// Gets a string containing the query text in the SearchBar.
		/// </summary>
		string Text { get; }

		/// <summary>
		/// Gets the character spacing.
		/// </summary>
		double CharacterSpacing { get; }
	}
}
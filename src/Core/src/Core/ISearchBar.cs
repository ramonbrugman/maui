namespace Microsoft.Maui
{
	/// <summary>
	/// Represents a View used to initiating a search.
	/// </summary>
	public interface ISearchBar : IView
	{
		/// <summary>
		/// 
		/// </summary>
		string Text { get; }

		/// <summary>
		/// Gets a string containing the query text in the SearchBar.
		/// </summary>
		double CharacterSpacing { get; }
	}
}
﻿namespace Microsoft.Maui.DeviceTests.Stubs
{
	public partial class SearchBarStub : StubBase, ISearchBar
	{
		string _text;

		public string Text { get => _text; set => SetProperty(ref _text, value); }

		public double CharacterSpacing { get; set; }
	}
}
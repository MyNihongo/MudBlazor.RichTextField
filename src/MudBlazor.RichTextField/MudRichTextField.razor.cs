namespace MudBlazor;

// TODO: https://github.com/dotnet/aspnetcore/issues/9974
// Would be nice to have it implemented
public partial class MudRichTextField : IAsyncDisposable
{
	private readonly string _id = Guid.NewGuid().ToString();
	private readonly DotNetObjectReference<InnerHtmlChangedInvokable> _innerHtmlChangedInvokable;

	private string _value = string.Empty;
	private bool _hasBeenRendered, _isInternalSet;

	public MudRichTextField()
	{
		var invokable = new InnerHtmlChangedInvokable(this);
		_innerHtmlChangedInvokable = DotNetObjectReference.Create(invokable);
	}

	[Parameter]
	public string Label { get; set; } = string.Empty;

	[Parameter]
	public Variant Variant { get; set; } = Variant.Text;

	[Parameter]
	public bool HasBold { get; set; } = true;

	[Parameter]
	public bool HasItalic { get; set; } = true;

	[Parameter]
	public bool HasUnderline { get; set; } = true;

	[Parameter]
	public string Value
	{
		get => _value;
		set
		{
			_value = value;

			if (_hasBeenRendered && !_isInternalSet)
			{
				Task.Run(() => SetInnerHtmlFromValueAsync(value));
			}
		}
	}

	[Parameter]
	public EventCallback<string> ValueChanged { get; set; }

	internal MudRichTextToolbar? Toolbar { get; set; }

	private string VariantString => Variant.ToString().ToLower();

	private bool HasToolbar => HasBold || HasItalic || HasUnderline;

	private string InputContainerClasses => new CssBuilder("mud-input")
		.AddClass($"mud-input-{VariantString}")
		.AddClass("mud-input-underline", Variant != Variant.Outlined)
		.AddClass("mud-shrink", !string.IsNullOrEmpty(_value))
		.Build();

	private string InputClasses => new CssBuilder("mud-input-slot")
		.AddClass("mud-input-root")
		.AddClass("mud-input-root-richtext")
		.AddClass("mud-input-root-richtext-toolbar", HasToolbar)
		.AddClass($"mud-input-root-{VariantString}")
		.Build();

	private string LabelClasses => new CssBuilder("mud-input-label")
		.AddClass("mud-input-label-animated")
		.AddClass($"mud-input-label-{VariantString}")
		.AddClass("mud-input-label-inputcontrol")
		.AddClass("mud-input-label-richtext", HasToolbar)
		.Build();

	// Rendering of a MarkupString does not seem to work well wil the MutationObserver (infinite notifications, weird innerHTML output, etc.)
	// For this reason don't use blazor rendering and handle everything in JavaScript (kudos...)
	protected override bool ShouldRender()
	{
		return false;
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (!firstRender)
			return;

		_hasBeenRendered = true;

		if (!string.IsNullOrEmpty(_value))
		{
			await SetInnerHtmlFromValueAsync(_value)
				.ConfigureAwait(false);
		}

		await _jsRuntime.InitAsync(_id, _innerHtmlChangedInvokable)
			.ConfigureAwait(false);
	}

	public ValueTask DisposeAsync()
	{
		GC.SuppressFinalize(this);

		_innerHtmlChangedInvokable.Dispose();
		return _jsRuntime.UnloadAsync(_id);
	}

	internal async Task SetValueFromInnerHtmlAsync(string innerHtml)
	{
		if (_isInternalSet)
			return;

		try
		{
			_isInternalSet = true;
			_value = innerHtml.ToValue();

			await InvokeAsync(() => ValueChanged.InvokeAsync(_value))
				.ConfigureAwait(false);
		}
		finally
		{
			_isInternalSet = false;
		}
	}

	private async Task SetInnerHtmlFromValueAsync(string value)
	{
		if (_isInternalSet)
			return;

		try
		{
			_isInternalSet = true;
			value = value.ToInnerHtml();

			await _jsRuntime.SetInnerHtmlAsync(_id, value)
				.ConfigureAwait(false);
		}
		finally
		{
			_isInternalSet = false;
		}
	}
}

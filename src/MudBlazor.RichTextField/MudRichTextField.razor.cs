namespace MudBlazor;

// TODO: https://github.com/dotnet/aspnetcore/issues/9974
// Would be nice to have it implemented
public partial class MudRichTextField : IAsyncDisposable
{
	private readonly string _id = Guid.NewGuid().ToString();
	private readonly DotNetObjectReference<InnerHtmlChangedInvokable> _innerHtmlChangedInvokable;

	private string _value = string.Empty;

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
	public string Value
	{
		get => _value;
		set => _value = value;
	}

	[Parameter]
	public EventCallback<string> ValueChanged { get; set; }

	private string VariantString => Variant.ToString().ToLower();

	private string InputContainerClasses => new CssBuilder("mud-input")
		.AddClass($"mud-input-{VariantString}")
		.AddClass("mud-input-underline", () => Variant != Variant.Outlined)
		.Build();

	private string InputClasses => new CssBuilder("mud-input-slot")
		.AddClass("mud-input-root")
		.AddClass("mud-richtextinput-root")
		.AddClass($"mud-input-root-{VariantString}")
		.Build();

	private string LabelClasses => new CssBuilder("mud-input-label")
		.AddClass("mud-input-label-animated")
		.AddClass($"mud-input-label-{VariantString}")
		.AddClass("mud-input-label-inputcontrol")
		.Build();

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (!firstRender)
			return;

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
		_value = innerHtml.FromInnerHtml();
		
		await InvokeAsync(() => ValueChanged.InvokeAsync(_value))
			.ConfigureAwait(false);
	}
}

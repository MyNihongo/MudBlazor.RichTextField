MudBlazorRichTextEdit = {
	observers: {}
};

MudBlazorRichTextEdit.init = (elementId) => {
	console.log(elementId);
	MudBlazorRichTextEdit.observers[elementId] = () => {
		console.log("disposed of " + elementId);
	};
}

MudBlazorRichTextEdit.dispose = (elementId) => {
	var observer = MudBlazorRichTextEdit.observers[elementId];
	if (!observer) {
		return;
	}

	observer();
	delete MudBlazorRichTextEdit.observers[elementId];
}
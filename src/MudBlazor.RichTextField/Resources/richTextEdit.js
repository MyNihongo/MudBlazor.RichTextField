MudBlazorRichTextEdit = {
	observers: {}
};

MudBlazorRichTextEdit.init = (elementId) => {
	const target = document.getElementById(elementId);
	if (!target) {
		return;
	}

	const callback = function () {
		console.log("smth has changed");
	};

	const observer = new MutationObserver(callback);
	const config = { childList: true, characterData: true, subtree: true };

	observer.observe(target, config);
	MudBlazorRichTextEdit.observers[elementId] = () => observer.disconnect();
}

MudBlazorRichTextEdit.dispose = (elementId) => {
	var observer = MudBlazorRichTextEdit.observers[elementId];
	if (!observer) {
		return;
	}

	observer();
	delete MudBlazorRichTextEdit.observers[elementId];
}
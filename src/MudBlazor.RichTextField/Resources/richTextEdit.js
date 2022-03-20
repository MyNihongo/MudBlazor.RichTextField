MudBlazorRichTextEdit = {
	observers: {}
};

const mudShrink = "mud-shrink";

MudBlazorRichTextEdit.init = (elementId, dotNetInvokable) => {
	const target = document.getElementById(elementId);
	if (!target) {
		return;
	}

	const callback = async function () {
		if (target.innerHTML && target.innerHTML !== "<br>") {
			addMudShrink(target.parentElement);
		} else {
			removeMudShrink(target.parentElement);
		}

		await dotNetInvokable.invokeMethodAsync("SetValue", target.innerHTML);
	};

	const observer = new MutationObserver(callback);
	const config = { childList: true, characterData: true, subtree: true };

	observer.observe(target, config);
	MudBlazorRichTextEdit.observers[elementId] = () => observer.disconnect();
}

MudBlazorRichTextEdit.setInnerHtml = (elementId, innerHtml) => {
	const target = document.getElementById(elementId);
	if (target) {
		target.innerHTML = innerHtml;
	}
}

MudBlazorRichTextEdit.dispose = (elementId) => {
	var observer = MudBlazorRichTextEdit.observers[elementId];
	if (!observer) {
		return;
	}

	observer();
	delete MudBlazorRichTextEdit.observers[elementId];
}

function addMudShrink(element) {
	if (!element || element.classList.contains(mudShrink)) {
		return;
	}

	element.classList.add(mudShrink);
}

function removeMudShrink(element) {
	if (!element || !element.classList.contains(mudShrink)) {
		return;
	}

	element.classList.remove(mudShrink);
}
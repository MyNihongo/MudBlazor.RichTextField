MudBlazorRichTextEdit = {
	observers: {},
	selections: {}
};

const mudShrink = "mud-shrink";

// https://codesandbox.io/s/caret-coordinates-index-contenteditable-9tq3o?from-embed
MudBlazorRichTextEdit.init = (elementId, dotNetInvokable) => {
	const target = document.getElementById(elementId);
	if (!target) {
		return;
	}

	const onInnerHtmlChanged = async function () {
		if (target.innerHTML && target.innerHTML !== "<br>") {
			addMudShrink(target.parentElement);
		} else {
			removeMudShrink(target.parentElement);
		}

		await dotNetInvokable.invokeMethodAsync("SetValue", target.innerHTML);
	};

	const onClick = function(e) {
		console.log(window.getSelection(), e.target);
	};

	const onKeyUp = function() {
		console.log("keyup");
	};

	const observer = new MutationObserver(onInnerHtmlChanged);
	const config = { childList: true, characterData: true, subtree: true };

	observer.observe(target, config);
	target.addEventListener("click", onClick);
	target.addEventListener("keyup", onKeyUp);

	MudBlazorRichTextEdit.observers[elementId] = () => {
		target.removeEventListener("keyup", onKeyUp);
		target.removeEventListener("click", onClick);
		observer.disconnect();

		const selection = MudBlazorRichTextEdit.selections[elementId];
		if (selection) {
			delete MudBlazorRichTextEdit.selections[elementId];
		}
	};
}

MudBlazorRichTextEdit.setInnerHtml = (elementId, innerHtml) => {
	const target = document.getElementById(elementId);
	if (target) {
		target.innerHTML = innerHtml;
	}
}

MudBlazorRichTextEdit.dispose = (elementId) => {
	var observer = MudBlazorRichTextEdit.observers[elementId];
	if (observer) {
		observer();
		delete MudBlazorRichTextEdit.observers[elementId];
	}
}

function setCurrentSelection(elementId, target) {
	MudBlazorRichTextEdit.selections[elementId] = {
		target: target
	};
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

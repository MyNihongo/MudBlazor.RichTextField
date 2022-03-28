MudBlazorRichTextEdit = {
	observers: {},
	selections: {},
	elementCandidates: {}
};

const mudShrink = "mud-shrink";

MudBlazorRichTextEdit.init = (elementId, dotNetInvokable) => {
	const target = document.getElementById(elementId);
	if (!target) {
		return;
	}

	const onInnerHtmlChanged = async function () {
		if (isNotNullOrWhitespace(target.innerText)) {
			addMudShrink(target.parentElement);
		} else {
			removeMudShrink(target.parentElement);
		}

		await dotNetInvokable.invokeMethodAsync("SetValue", target.innerHTML);
	};

	const onClick = async function () {
		const selectionTarget = setCurrentSelection(elementId);
		await setFormatSelectionAsync(selectionTarget, dotNetInvokable);
	};

	const onKeyUp = async function (e) {
		const selectionTarget = setCurrentSelection(elementId, e.keyCode);
		await setFormatSelectionAsync(selectionTarget, dotNetInvokable);
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

MudBlazorRichTextEdit.applyFormatting = (elementId, tagName, isActive) => {
	const selection = MudBlazorRichTextEdit.selections[elementId];
	if (!selection) {
		return isActive;
	}

	const range = document.createRange();
	range.setStart(selection.startContainer, selection.startOffset);
	range.setEnd(selection.endContainer, selection.endOffset);
	setWindowSelectionRange(range);

	if (isSelectionEmpty(selection)) {
		MudBlazorRichTextEdit.elementCandidates[elementId] = {
			startContainer: selection.startContainer,
			startOffset: selection.startOffset,
			tagName: tagName,
			isActive: isActive
		};
	} else {
		
	}

	return isActive;
}

MudBlazorRichTextEdit.dispose = (elementId) => {
	var observer = MudBlazorRichTextEdit.observers[elementId];
	if (observer) {
		observer();
		delete MudBlazorRichTextEdit.observers[elementId];
	}
}

function setCurrentSelection(elementId, keyCode) {
	const selection = window.getSelection();
	if (selection.rangeCount === 0) {
		return undefined;
	}

	const range = selection.getRangeAt(0);
	applyElementCandidate(elementId, range, keyCode);

	const prevSelection = MudBlazorRichTextEdit.selections[elementId];
	const currentElement = getSelectionElement(range.startContainer);

	MudBlazorRichTextEdit.selections[elementId] = {
		startContainer: range.startContainer,
		startOffset: range.startOffset,
		endContainer: range.endContainer,
		endOffset: range.endOffset
	};

	if (prevSelection) {
		const prevElement = getSelectionElement(prevSelection);
		if (currentElement === prevElement) {
			return undefined;
		} else {
			return currentElement;
		}
	} else {
		return currentElement;
	}
}

function applyElementCandidate(elementId, selection, keyCode) {
	if (keyCode && isSelectionEmpty(selection)) {
		const candidate = MudBlazorRichTextEdit.elementCandidates[elementId];
		if (candidate && candidate.startContainer === selection.startContainer) {
			// Keys like `shift`, `control`, etc., so we do not want to reset the element
			if (candidate.startOffset === selection.startOffset) {
				return;
			}

			// Make sure that the position is not moved by arrow keys
			const endOffset = 1;
			if (candidate.startOffset === selection.startOffset - endOffset && (keyCode < 37 || keyCode > 40)) {
				const element = getSelectionElement(selection.startContainer);
				const startIndex = getSelectionIndex(element, candidate);

				let newElement;
				if (isChildOf(element, candidate.tagName)) {
					const nodeLength = getNodeLength(selection.startContainer);
					if (selection.startOffset === nodeLength) {
						return;
					} else {
						newElement = splitElement(element, candidate.tagName, startIndex, endOffset);
						return;
					}
				} else {
					newElement = insertNewElement(element, candidate.tagName, startIndex, endOffset);
				}

				const range = document.createRange();
				range.setStart(newElement, endOffset);
				range.collapse(true);
				setWindowSelectionRange(range);
			}
		}
	}

	delete MudBlazorRichTextEdit.elementCandidates[elementId];
}

async function setFormatSelectionAsync(target, dotNetInvokable) {
	if (!target) {
		return;
	}

	let isBoldActive = false, isItalicActive = false, isUnderlineActive = false;

	// RichText root always has the ID
	while (target.parentElement && !target.parentElement.id) {
		switch (target.tagName) {
			case "B":
				isBoldActive = true;
				break;
			case "I":
				isItalicActive = true;
				break;
			case "U":
				isUnderlineActive = true;
				break;
		}

		target = target.parentElement;
	}

	const toolbarOptions = {
		isBoldActive: isBoldActive,
		isItalicActive: isItalicActive,
		isUnderlineActive: isUnderlineActive
	};

	await dotNetInvokable.invokeMethodAsync("SetToolbarOptions", toolbarOptions);
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

function insertNewElement(element, tagName, startIndex, endOffset) {
	const innerHtml =
		element.innerHTML.substring(0, startIndex) +
		`<${tagName}>` +
		element.innerHTML.substring(startIndex, startIndex + endOffset) +
		`</${tagName}>` +
		element.innerHTML.substring(startIndex + endOffset, element.innerHTML.length);

	element.innerHTML = innerHtml;
	return getNodeAt(element, startIndex + endOffset);
}

function splitElement(element, tagName, startIndex, endOffset) {
	const innerHtml =
		element.innerHTML.substring(0, startIndex) +
		`</${tagName}>` +
		element.innerHTML.substring(startIndex, startIndex + endOffset) +
		`<${tagName}>` +
		element.innerHTML.substring(startIndex + endOffset, element.innerHTML.length);

	if (element.tagName === tagName) {
		element.outerHTML = `<${tagName}>${innerHtml}</${tagName}>`;
	} else {
		element.innerHTML = innerHtml;
	}
}

function isNotNullOrWhitespace(str) {
	return str && str.match(/^\s*$/) === null;
}

function isSelectionEmpty(selection) {
	return selection.startContainer === selection.endContainer && selection.startOffset === selection.endOffset;
}

function isChildOf(element, elementTag) {
	// RichText root always has the ID
	while (element.parentElement && !element.parentElement.id) {
		if (element.tagName === elementTag) {
			return true;
		}

		element = element.parentElement;
	}

	return false;
}

function getSelectionIndex(parentElement, candidate) {
	if (parentElement === candidate.startContainer) {
		return candidate.startOffset;
	}

	let index = 0;
	for (let i = 0; i < parentElement.childNodes.length; i++) {
		if (parentElement.childNodes[i] === candidate.startContainer) {
			return index + candidate.startOffset;
		}

		index += getNodeLength(parentElement.childNodes[i]);
	}

	return index;
}

function getNodeAt(parentElement, index) {
	let currentIndex = 0;
	for (let i = 0; i < parentElement.childNodes.length; i++) {
		const nodeLength = getNodeLength(parentElement.childNodes[i]);
		if (index >= currentIndex && index < currentIndex + nodeLength) {
			return parentElement.childNodes[i];
		}

		currentIndex += nodeLength;
	}

	return undefined;
}

function getNodeLength(node) {
	if (node instanceof Element) {
		return node.outerHTML.length;
	} else {
		return node.textContent.length;
	}
}

function getSelectionElement(container) {
	return container instanceof Element
		? container
		: container.parentElement;
}

function setWindowSelectionRange(range) {
	const windowSelection = window.getSelection();
	windowSelection.removeAllRanges();
	windowSelection.addRange(range);
}

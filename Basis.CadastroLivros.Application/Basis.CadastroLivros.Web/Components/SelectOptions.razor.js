export function initializeSelectOptions(element, dotNetHelper) {
    const clickHandler = (event) => {
        if (!element.contains(event.target)) {
            dotNetHelper.invokeMethodAsync('HandleClickOutside');
        }
    };

    element._clickHandler = clickHandler;
    document.addEventListener('click', clickHandler);
}

export function disposeSelectOptions(element) {
    if (element._clickHandler) {
        document.removeEventListener('click', element._clickHandler);
        delete element._clickHandler;
    }
}
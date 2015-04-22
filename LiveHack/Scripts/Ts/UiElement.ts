class UiElement {
    public elementId: string;
    protected htmlElement: HTMLElement;

    constructor(id: string) {
        this.elementId = id;
        var template: HTMLScriptElement = <HTMLScriptElement>document.getElementById("template_" + this.elementId);
        if (template == null) {
            throw new Error("Tried to instantiate non-existing template");
        }
        var parser = new DOMParser();
        var fragment = parser.parseFromString(template.innerHTML, "text/html");
        this.htmlElement = <HTMLElement>fragment.body.firstElementChild;
    }

    public show(): void {
        this.htmlElement = <HTMLElement>document.body.appendChild(this.htmlElement);
    }

    public hide(): void {
        this.htmlElement = <HTMLElement>this.htmlElement.parentElement.removeChild(this.htmlElement);
    }
}
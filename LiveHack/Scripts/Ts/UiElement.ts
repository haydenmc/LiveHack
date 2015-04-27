class UiElement {
    public elementId: string;
    public title: string;
    protected parentElement: HTMLElement;
    protected htmlElement: HTMLElement;

    constructor(id: string, parent?: HTMLElement) {
        this.elementId = id;
        this.parentElement = parent;
        var template: HTMLScriptElement = <HTMLScriptElement>document.getElementById("template_" + this.elementId);
        if (template == null) {
            throw new Error("Tried to instantiate non-existing template");
        }
        var parser = new DOMParser();
        var fragment = parser.parseFromString(template.innerHTML, "text/html");
        this.htmlElement = <HTMLElement>fragment.body.firstElementChild;
    }

    public getHtmlElement(): HTMLElement {
        return this.htmlElement;
    }

    public show(): void {
        if (this.parentElement != null) {
            this.htmlElement = <HTMLElement>this.parentElement.appendChild(this.htmlElement);
        } else {
            this.htmlElement = <HTMLElement>document.body.appendChild(this.htmlElement);
        }
    }

    public hide(): void {
        this.htmlElement = <HTMLElement>this.htmlElement.parentElement.removeChild(this.htmlElement);
    }

    public destroy(): void {
        this.hide();
        this.htmlElement = null;
        this.parentElement = null;
    }
}
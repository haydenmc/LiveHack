/// <reference path="../UiElement.ts" />

class ContentPaneElement extends UiElement {
    constructor() {
        super("ContentPane");
        new ChatElement(<HTMLElement>this.htmlElement).show();
    }
}
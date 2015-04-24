/// <reference path="../UiElement.ts" />
/// <reference path="ChatElement.ts" />

class ContentPaneElement extends UiElement {
    constructor() {
        super("ContentPane");
        new ChatElement(<HTMLElement>this.htmlElement).show();
    }
}
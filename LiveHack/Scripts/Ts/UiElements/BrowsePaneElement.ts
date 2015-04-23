/// <reference path="../UiElement.ts" />
/// <reference path="ContentPaneElement.ts" />

class BrowsePaneElement extends UiElement {
    public contentPaneElement: ContentPaneElement;

    constructor(contentPane: ContentPaneElement) {
        this.contentPaneElement = contentPane;
        super("BrowsePane");
    }
}
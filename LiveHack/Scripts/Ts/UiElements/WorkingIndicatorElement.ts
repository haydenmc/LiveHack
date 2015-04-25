/// <reference path="../uielement.ts" />
class WorkingIndicator extends UiElement {
    private workItems: number = 0;

    constructor() {
        super("WorkingIndicator");
    }

    public pushWorkItem(): void {
        this.workItems++;
        if (this.workItems > 0) {
            if (this.htmlElement.classList.contains("closed")) {
                this.htmlElement.classList.remove("closed");
            }
        }
    }

    public popWorkItem(): void {
        this.workItems--;
        if (this.workItems <= 0) {
            if (!this.htmlElement.classList.contains("closed")) {
                this.htmlElement.classList.add("closed");
            }
        }
    }
}
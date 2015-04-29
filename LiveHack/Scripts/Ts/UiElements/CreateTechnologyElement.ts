/// <reference path="../uielement.ts" />
/// <reference path="../application.ts" />

class CreateTechnologyElement extends UiElement {
    private get processing(): boolean {
        return this._processing;
    }

    private set processing(value: boolean) {
        if (this._processing != value) {
            this._processing = value;
            var inputElements = this.htmlElement.querySelectorAll("input");
            if (value) {
                Application.instance.workingIndicator.pushWorkItem();
                for (var i = 0; i < inputElements.length; i++) {
                    (<HTMLInputElement>inputElements.item(i)).disabled = true;
                }
            } else {
                Application.instance.workingIndicator.popWorkItem();
                for (var i = 0; i < inputElements.length; i++) {
                    (<HTMLInputElement>inputElements.item(i)).disabled = false;
                }
            }
        }
    }

    private _processing: boolean;

    constructor() {
        super("CreateTechnology");
        this._processing = false;
        this.htmlElement.querySelector("a.button.cancel").addEventListener("click",(ev) => {
            this.destroy();
        });

        this.htmlElement.querySelector("a.button.create").addEventListener("click",(ev) => {
            this.submit();
        });
    }

    public submit(): void {
        if (this.processing) {
            return;
        }
        this.processing = true;

        var name = (<HTMLInputElement>this.htmlElement.querySelector('input[name="name"]')).value;

        Application.instance.dataSource.createTechnology(name).then((technology) => {
            this.processing = false;
            this.hide();
        },(error) => {
                alert("Error creating technology: " + error);
                this.processing = false;
            });
    }
}
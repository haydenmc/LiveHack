/// <reference path="../Application.ts" />

class CreateAnnouncementElement extends UiElement {
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
        super("CreateAnnouncement");
        this._processing = false;
        this.htmlElement.querySelector("a.button.cancel").addEventListener("click", (ev) => {
            this.destroy();
        });

        this.htmlElement.querySelector("a.button.post").addEventListener("click", (ev) => {
            this.submit();
        });
    }

    public submit(): void {
        if (this.processing) {
            return;
        }
        this.processing = true;

        var title = (<HTMLInputElement>this.htmlElement.querySelector('input[name="title"]')).value;
        var body = (<HTMLTextAreaElement>this.htmlElement.querySelector('textarea')).value;

        Application.instance.dataSource.createAnnouncement(title, body).then((announcement) => {
            this.processing = false;
            this.hide();
        },(error) => {
                alert("Error posting announcement: " + error);
                this.processing = false;
            });
    }
}
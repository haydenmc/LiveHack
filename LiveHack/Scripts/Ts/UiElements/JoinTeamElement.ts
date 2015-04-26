/// <reference path="../Application.ts" />
/// <reference path="../UiElement.ts" />

class JoinTeam extends UiElement {
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
        super("JoinTeam");

        // Set not processing (all fields editable)
        this._processing = false;

        // Bind events
        this.htmlElement.querySelector("a.button.cancel").addEventListener("click", (ev) => {
            this.hide();
        });

        this.htmlElement.querySelector("a.button.join").addEventListener("click",(ev) => {
            this.joinTeam();
        });

        var inputElements = this.htmlElement.querySelectorAll("input");
        for (var i = 0; i < inputElements.length; i++) {
            inputElements.item(i).addEventListener('keypress', (e: KeyboardEvent) => {
                var key = e.which || e.keyCode;
                if (key === 13) { // 13 is enter
                    this.joinTeam();
                }
            });
        }

        this.htmlElement.querySelector("form").addEventListener("submit", (ev) => {
            ev.preventDefault();
            this.joinTeam();
        });
    }

    public joinTeam(): void {
        if (this.processing) {
            return;
        }
        // validate
        var accessCode = (<HTMLInputElement>this.htmlElement.querySelector('input[type="text"]')).value;
        if (accessCode.length <= 0) {
            alert("You must enter an access code.");
            return;
        }

        this.processing = true;

        Application.instance.dataSource.joinTeam(accessCode).then((newTeam) => {
            Application.instance.dataSource.team = newTeam;
            this.processing = false;
            this.hide();
        },(error) => {
                alert("Error joining team: " + error);
                this.processing = false;
            });
    }
}
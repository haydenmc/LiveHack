/// <reference path="../Application.ts" />
/// <reference path="../UiElement.ts" />

class CreateTeam extends UiElement {
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
        super("CreateTeam");

        // Set not processing (all fields editable)
        this._processing = false;

        // Bind events
        this.htmlElement.querySelector("a.button.cancel").addEventListener("click", (ev) => {
            this.hide();
        });

        this.htmlElement.querySelector("a.button.create").addEventListener("click", (ev) => {
            this.createTeam();
        });

        var inputElements = this.htmlElement.querySelectorAll("input");
        for (var i = 0; i < inputElements.length; i++) {
            inputElements.item(i).addEventListener('keypress',(e: KeyboardEvent) => {
                var key = e.which || e.keyCode;
                if (key === 13) { // 13 is enter
                    this.createTeam();
                }
            });
        }

        this.htmlElement.querySelector("form").addEventListener("submit", (ev) => {
            ev.preventDefault();
            this.createTeam();
        });
    }

    public createTeam(): void {
        if (this.processing) {
            return;
        }
        // validate
        var teamName = (<HTMLInputElement>this.htmlElement.querySelector('input[type="text"]')).value;
        if (teamName.length <= 0) {
            alert("You must enter a team name.");
            return;
        }

        this.processing = true;

        Application.instance.dataSource.createTeam(teamName).then((newTeam) => {
            // reload the team pane here.
            this.processing = false;
            this.hide();
        },(error) => {
                alert("Error creating team: " + error);
                this.processing = false;
            });
    }
}
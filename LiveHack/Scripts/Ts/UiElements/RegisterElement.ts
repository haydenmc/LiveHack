/// <reference path="../Application.ts" />

class RegisterElement extends UiElement {
    private get processing(): boolean {
        return this._processing;
    }

    private set processing(value: boolean) {
        if (this._processing != value) {
            this._processing = value;
            var inputElements = this.htmlElement.querySelectorAll("input");
            if (value) {
                for (var i = 0; i < inputElements.length; i++) {
                    (<HTMLInputElement>inputElements.item(i)).disabled = true;
                }
            } else {
                for (var i = 0; i < inputElements.length; i++) {
                    (<HTMLInputElement>inputElements.item(i)).disabled = false;
                }
            }
        }
    }

    private _processing: boolean;

    constructor() {
        super("Register");
        this.processing = false;

        // Bind events
        this.htmlElement.querySelector("a.button.cancel").addEventListener("click",(ev) => {
            new LogInElement().show();
            this.hide();
        });
        this.htmlElement.querySelector("a.button.register").addEventListener("click",(ev) => {
            this.submitRegistration();
        });
        // Enter key for all inputs
        var inputElements = this.htmlElement.querySelectorAll("input");
        for (var i = 0; i < inputElements.length; i++) {
            inputElements.item(i).addEventListener('keypress',(e: KeyboardEvent) => {
                var key = e.which || e.keyCode;
                if (key === 13) { // 13 is enter
                    this.submitRegistration();
                }
            });
        }
    }

    public submitRegistration(): void {
        if (this.processing) {
            return;
        }
        // Collect data from inputs
        var email = (<HTMLInputElement>this.htmlElement.querySelector('input[name="email"]')).value;
        var displayName = (<HTMLInputElement>this.htmlElement.querySelector('input[name="displayName"]')).value;
        var password = (<HTMLInputElement>this.htmlElement.querySelector('input[name="password"]')).value;
        var confirmPassword = (<HTMLInputElement>this.htmlElement.querySelector('input[name="confirmPassword"]')).value;

        // validation
        if (password != confirmPassword) {
            alert("Your passwords do not match!"); // TODO: replace with nicer dialog
            return;
        }

        // Submit registration
        this.processing = true;

        Application.instance.dataSource.register(email, displayName, password).then(() => {
            Application.instance.dataSource.authenticate(email, password).then(() => {
                Application.instance.loggedIn();
                this.hide();
            }, () => {
                    alert("Error logging in after completing registration. Please try to log in again.");
                    new LogInElement().show();
                    this.hide();
                });
        }, (error) => {
                alert("There was an error processing your registration: " + error);
                this.processing = false;
            });
    }
}
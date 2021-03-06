﻿/// <reference path="../Application.ts" />

class LogInElement extends UiElement {
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
        super("LogIn");

        this._processing = false;

        // Bind events ... 
        this.htmlElement.querySelector("a.button.register").addEventListener("click", (ev) => {
            new RegisterElement().show();
            this.hide();
        });
        this.htmlElement.querySelector("a.button.login").addEventListener("click", (ev) => {
            this.processLogIn();
        });

        var inputElements = this.htmlElement.querySelectorAll("input");
        for (var i = 0; i < inputElements.length; i++) {
            inputElements.item(i).addEventListener('keypress', (e: KeyboardEvent) => {
                var key = e.which || e.keyCode;
                if (key === 13) { // 13 is enter
                    this.processLogIn();
                }
            });
        }
    }

    public processLogIn(): void {
        if (this.processing) {
            return;
        }
        this.processing = true;

        var username = (<HTMLInputElement>this.htmlElement.querySelector("input[type=email]")).value;
        var password = (<HTMLInputElement>this.htmlElement.querySelector("input[type=password]")).value;
        Application.instance.dataSource.authenticate(username, password).then((value) => {
            this.processing = false;
            Application.instance.loggedIn();
            this.hide();
        }, (error) => {
            alert("There was an error logging in: " + error);
            this.processing = false;
        });
    }
}
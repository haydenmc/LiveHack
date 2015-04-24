/// <reference path="../Application.ts" />

class LogInElement extends UiElement {
    private _processingLogIn: boolean = false;

    constructor() {
        super("LogIn");

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
        if (this._processingLogIn) {
            return;
        }
        this._processingLogIn = true;
        var inputElements = this.htmlElement.querySelectorAll("input");
        for (var i = 0; i < inputElements.length; i++) {
            (<HTMLInputElement>inputElements.item(i)).disabled = true;
        }
        var username = (<HTMLInputElement>this.htmlElement.querySelector("input[type=email]")).value;
        var password = (<HTMLInputElement>this.htmlElement.querySelector("input[type=password]")).value;
        Application.instance.dataSource.authenticate(username, password).then((value) => {
            this._processingLogIn = false;
            Application.instance.loggedIn();
            this.hide();
        }, (error) => {
            alert("Error!");
            this._processingLogIn = false;
            for (var i = 0; i < inputElements.length; i++) {
                (<HTMLInputElement>inputElements.item(i)).disabled = false;
            }
        });
    }
}
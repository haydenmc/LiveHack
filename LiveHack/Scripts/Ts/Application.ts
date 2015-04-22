/* Core references */
/// <reference path="UiElement.ts" />

/* UI elements */
/// <reference path="UiElements/LogInElement.ts" />

class Application {
    public static instance: Application;
    constructor() {
        Application.instance = this;
    }

    public start(): void {
        // show a log-in dialog
        new LogInElement().show();
    }
}

window.addEventListener("load",(ev) => {
    new Application().start();
});
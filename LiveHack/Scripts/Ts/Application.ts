/* External typings */
/// <reference path="../typings/es6-promise.d.ts" />

/* Core references */
/// <reference path="UiElement.ts" />

/* Data references */
/// <reference path="Data/JsonRequest.ts" />
/// <reference path="Data/DataSource.ts" />

/* Models */
/// <reference path="Data/Models/AuthResponse.ts" />

/* UI elements */
/// <reference path="UiElements/LogInElement.ts" />

class Application {
    public static instance: Application;
    public dataSource: DataSource;
    constructor() {
        Application.instance = this;
        this.dataSource = new DataSource();
    }

    public start(): void {
        // show a log-in dialog
        new LogInElement().show();
    }
}

window.addEventListener("load",(ev) => {
    new Application().start();
});
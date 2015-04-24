/* External typings */
/// <reference path="../typings/es6-promise.d.ts" />

/* Core references */
/// <reference path="UiElement.ts" />

/* Data references */
/// <reference path="Data/JsonRequest.ts" />
/// <reference path="Data/DataSource.ts" />
/// <reference path="Data/SignalR/LiveHackHub.ts" />

/* Models */
/// <reference path="Data/Models/AuthResponse.ts" />

/* UI elements */
/// <reference path="UiElements/LogInElement.ts" />
/// <reference path="UiElements/TeamPaneElement.ts" />
/// <reference path="UiElements/ContentPaneElement.ts" />
/// <reference path="UiElements/BrowsePaneElement.ts" />
/// <reference path="UiElements/RegisterElement.ts" />

class Application {
    public static instance: Application;
    public dataSource: DataSource;
    public teamPane: TeamPaneElement;
    public contentPane: ContentPaneElement;
    public browsePane: BrowsePaneElement;

    constructor() {
        Application.instance = this;
        this.dataSource = new DataSource();
    }

    public start(): void {
        // show a log-in dialog
        new LogInElement().show();
    }

    public loggedIn(): void {
        this.dataSource.liveHackHub.connect();
        this.teamPane = new TeamPaneElement();
        this.teamPane.show();
        this.contentPane = new ContentPaneElement();
        this.contentPane.show();
        this.browsePane = new BrowsePaneElement(this.contentPane);
        this.browsePane.show();
    }
}

window.addEventListener("load",(ev) => {
    new Application().start();
});
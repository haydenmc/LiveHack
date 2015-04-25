/// <reference path="../UiElement.ts" />
/// <reference path="CreateTeamElement.ts" />

class TeamPaneElement extends UiElement {
    constructor() {
        super("TeamPane");

        this.htmlElement.querySelector("a.button.create").addEventListener("click",() => {
            new CreateTeam().show();
        });
    }
}
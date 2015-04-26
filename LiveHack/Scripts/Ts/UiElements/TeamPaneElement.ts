/// <reference path="../UiElement.ts" />
/// <reference path="CreateTeamElement.ts" />
/// <reference path="JoinTeamElement.ts" />

class TeamPaneElement extends UiElement {
    constructor() {
        super("TeamPane");
        this.htmlElement.querySelector("a.button.create").addEventListener("click",() => {
            new CreateTeam().show();
        });
        this.htmlElement.querySelector("a.button.join").addEventListener("click",() => {
            new JoinTeam().show();
        });

        // Subscribe to datamodel changes
        Application.instance.dataSource.propertyChanged.subscribe((propertyChanged) => {
            if (propertyChanged == "team") {
                this.update();
            }
        });

        // Update to current team model
        this.update();
    }

    public update(): void {
        // update the UI with the new team information
        var newTeam = Application.instance.dataSource.team;
        if (newTeam == null) {
            // If we don't have a team, display the no team prompt
            (<HTMLElement>this.htmlElement.querySelector("h1")).innerHTML = "Your Team";
            (<HTMLElement>this.htmlElement.querySelector(".noTeam")).style.display = "";
            (<HTMLElement>this.htmlElement.querySelector(".hasTeam")).style.display = "none";
            return;
        }
        (<HTMLElement>this.htmlElement.querySelector("h1")).innerHTML = newTeam.name;
        (<HTMLElement>this.htmlElement.querySelector(".noTeam")).style.display = "none";
        (<HTMLElement>this.htmlElement.querySelector(".hasTeam")).style.display = "";
        (<HTMLElement>this.htmlElement.querySelector(".accessCode")).innerHTML = newTeam.accessCode;
        // Populate members list
        var members = <HTMLUListElement>this.htmlElement.querySelector("ul.members");
        members.style.display = "";
        members.innerHTML = "";
        for (var i = 0; i < newTeam.owners.length; i++) {
            var member = document.createElement("li");
            member.innerHTML = newTeam.owners[i].displayName;
            members.appendChild(member);
        }
    }
}
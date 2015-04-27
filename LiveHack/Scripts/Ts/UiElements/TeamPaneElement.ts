/// <reference path="../UiElement.ts" />
/// <reference path="CreateTeamElement.ts" />
/// <reference path="JoinTeamElement.ts" />
/// <reference path="ChatElement.ts" />

class TeamPaneElement extends UiElement {
    public teamChat: ChatElement;
    private teamId: string;

    private callbackNewOwner = (args: { chatId: string; user: User; }) => {
        if (this.teamId == args.chatId) {
            this.newOwner(args.user);
        }
    };

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

        // Bind event handlers
        Application.instance.dataSource.subscribe(DataEvent.NewChatOwner, this.callbackNewOwner);
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
        this.teamId = newTeam.id;
        (<HTMLElement>this.htmlElement.querySelector("h1")).innerHTML = newTeam.name;
        (<HTMLElement>this.htmlElement.querySelector(".noTeam")).style.display = "none";
        (<HTMLElement>this.htmlElement.querySelector(".hasTeam")).style.display = "";
        (<HTMLElement>this.htmlElement.querySelector(".accessCode")).innerHTML = newTeam.accessCode;
        // Populate members list
        var members = <HTMLUListElement>this.htmlElement.querySelector("ul.members");
        members.style.display = "";
        members.innerHTML = "";
        for (var i = 0; i < newTeam.owners.length; i++) {
            this.newOwner(newTeam.owners[i]);
        }
        if (typeof this.teamChat !== 'undefined') {
            this.teamChat.destroy();
        }
        this.teamChat = new ChatElement(<HTMLElement>this.htmlElement.querySelector(".teamChat"), newTeam.name + " Chat", newTeam.id);
        this.teamChat.show();
    }

    public newOwner(user: User) {
        var members = <HTMLUListElement>this.htmlElement.querySelector("ul.members");
        var member = document.createElement("li");
        member.style.borderLeftColor = ColorHasher.guidToColor(user.id);
        member.innerHTML = user.displayName;
        members.appendChild(member);
    }
}
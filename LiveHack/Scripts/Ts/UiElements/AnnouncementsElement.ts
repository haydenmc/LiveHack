/// <reference path="../UiElement.ts" />
/// <reference path="../Application.ts" />

class AnnouncementsElement extends UiElement {

    public get elementAnnouncementsList(): HTMLUListElement {
        return <HTMLUListElement>this.htmlElement;
    }

    constructor(parentElement: HTMLElement) {
        super("Announcements", parentElement);
        this.title = "Announcements";
        this.fetchAnnouncements();
    }

    public fetchAnnouncements(): void {
        // Fetch announcements
        Application.instance.workingIndicator.pushWorkItem();
        Application.instance.dataSource.getAnnouncements().then((announcements) => {
            this.elementAnnouncementsList.innerHTML = "";
            for (var i = 0; i < announcements.length; i++) {
                var announcementElement = document.createElement("li");
                var announcementTitle = document.createElement("h1");
                announcementTitle.innerHTML = announcements[i].title;
                var announcementBody = document.createElement("div");
                announcementBody.classList.add("body");
                announcementBody.innerHTML = announcements[i].body;
                announcementElement.appendChild(announcementTitle);
                announcementElement.appendChild(announcementBody);
                this.elementAnnouncementsList.appendChild(announcementElement);
            }
            Application.instance.workingIndicator.popWorkItem();
        },(error) => {
                alert("Error fetching announcements: " + error);
                Application.instance.workingIndicator.popWorkItem();
            });
    }
}
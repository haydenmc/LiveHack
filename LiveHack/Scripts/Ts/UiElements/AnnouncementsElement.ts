/// <reference path="../UiElement.ts" />
/// <reference path="../Application.ts" />

class AnnouncementsElement extends UiElement {

    private announcements: ObservableArray<Announcement>;

    private callback_receivedNewAnnouncement = (ann: Announcement) => {
        this.addAnnouncement(ann);
    }

    private callback_addNewAnnouncement = (args: { item: Announcement; position: number; }) => {
        this.addAnnouncement(args.item);
    };

    public get elementAnnouncementsList(): HTMLUListElement {
        return <HTMLUListElement>this.htmlElement.querySelector("ul.announcements");
    }

    constructor(parentElement: HTMLElement) {
        super("Announcements", parentElement);
        this.title = "Announcements";
        this.announcements = new ObservableArray<Announcement>();
        this.announcements.itemAdded.subscribe(this.callback_addNewAnnouncement);
        this.fetchAnnouncements();

        this.htmlElement.querySelector("a.button.addButton").addEventListener("click",() => {
            new CreateAnnouncementElement().show();
        });

        if (!Application.instance.dataSource.user.isOrganizer) {
            (<HTMLElement>this.htmlElement.querySelector("a.button.addButton")).style.display = "none";
        }

        Application.instance.dataSource.subscribe(DataEvent.NewAnnouncement, this.callback_receivedNewAnnouncement);
    }

    public fetchAnnouncements(): void {
        // Fetch announcements
        Application.instance.workingIndicator.pushWorkItem();
        Application.instance.dataSource.getAnnouncements().then((announcements) => {
            this.elementAnnouncementsList.innerHTML = "";
            for (var i = announcements.length - 1; i >= 0; i--) {
                this.addAnnouncement(announcements[i]);
            }
            Application.instance.workingIndicator.popWorkItem();
        },(error) => {
                alert("Error fetching announcements: " + error);
                Application.instance.workingIndicator.popWorkItem();
            });
    }

    private addAnnouncement(ann: Announcement) {
        var announcementElement = document.createElement("li");
        var announcementTitle = document.createElement("h1");
        announcementTitle.innerHTML = ann.title;
        var announcementSubtitle = document.createElement("div");
        announcementSubtitle.classList.add("time");
        announcementSubtitle.innerHTML = moment(ann.dateTimeCreated).fromNow();
        var announcementBody = document.createElement("div");
        announcementBody.classList.add("body");
        announcementBody.innerHTML = ann.body;
        announcementElement.appendChild(announcementTitle);
        announcementElement.appendChild(announcementSubtitle);
        announcementElement.appendChild(announcementBody);
        this.elementAnnouncementsList.insertBefore(announcementElement, this.elementAnnouncementsList.firstChild);
    }
}
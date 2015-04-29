/// <reference path="../UiElement.ts" />
/// <reference path="../Data/ObservableArray.ts" />
/// <reference path="ChatElement.ts" />
/// <reference path="AnnouncementsElement.ts" />

class ContentBrowserElement extends UiElement {
    private currentElement: UiElement;
    private globalElements: ObservableArray<UiElement>;
    private techElements: ObservableArray<UiElement>;

    public get elementGlobalElementList(): HTMLElement {
        return <HTMLElement>this.htmlElement.querySelector("ul.globalElements");
    }

    public get elementTechElementList(): HTMLElement {
        return <HTMLElement>this.htmlElement.querySelector("ul.techElements");
    }

    public get elementContentPane(): HTMLElement {
        return <HTMLElement>this.htmlElement.querySelector("div.contentPane");
    }

    private callback_receivedNewTechnology = (tech: Technology) => {
        var el = new ChatElement(this.elementContentPane, tech.name, tech.id);
        this.techElements.push(el);
    }

    public callback_globalElementAdded = (arg: { item: UiElement; position: number; }) => {
        var newElementListing = document.createElement("li");
        newElementListing.innerHTML = arg.item.title;

        // TODO: Bind on-click and such here
        newElementListing.addEventListener("click",() => {
            this.selectElement(arg.item);
        });
        
        var existingElements = this.elementGlobalElementList.querySelectorAll("li");
        if (existingElements.length <= 0) {
            this.elementGlobalElementList.appendChild(newElementListing);
        } else {
            this.elementGlobalElementList.insertBefore(newElementListing, existingElements.item(arg.position));
        }
    };

    public callback_techElementAdded = (arg: { item: UiElement; position: number; }) => {
        var newElementListing = document.createElement("li");
        newElementListing.innerHTML = arg.item.title;

        // TODO: Bind on-click and such here
        newElementListing.addEventListener("click",() => {
            this.selectElement(arg.item);
        });

        var existingElements = this.elementTechElementList.querySelectorAll("li");
        if (existingElements.length <= 0) {
            this.elementTechElementList.appendChild(newElementListing);
        } else {
            this.elementTechElementList.insertBefore(newElementListing, existingElements.item(arg.position));
        }
    };

    constructor() {
        super("ContentBrowser");
        this.globalElements = new ObservableArray<UiElement>();
        this.techElements = new ObservableArray<UiElement>();

        this.globalElements.itemAdded.subscribe(this.callback_globalElementAdded);
        var globalChat = new ChatElement(this.elementContentPane, "Global Chat");
        this.globalElements.push(globalChat);
        this.globalElements.push(new AnnouncementsElement(this.elementContentPane));

        this.selectElement(globalChat);

        // Get all technologies
        this.techElements.itemAdded.subscribe(this.callback_techElementAdded);
        Application.instance.workingIndicator.pushWorkItem();
        Application.instance.dataSource.getTechnologies().then((technologies) => {
            for (var i = 0; i < technologies.length; i++) {
                var techChat = new ChatElement(this.elementContentPane, Sanitizer.htmlEscape(technologies[i].name), technologies[i].id);
                this.techElements.push(techChat);
            }
            Application.instance.workingIndicator.popWorkItem();
        },(error) => {
                alert("Could not fetch technologies: " + error);
                Application.instance.workingIndicator.popWorkItem();
            });

        this.htmlElement.querySelector("a.button.newTech").addEventListener("click",() => {
            new CreateTechnologyElement().show();
        });

        // Subscribe to new technologies
        Application.instance.dataSource.subscribe(DataEvent.NewTechnology, this.callback_receivedNewTechnology);
    }

    public selectElement(element: UiElement) {
        if (this.currentElement != null) {
            this.currentElement.hide();
            this.findListItem(this.currentElement).classList.remove("selected");
        }
        this.currentElement = element;
        this.currentElement.show();
        this.findListItem(this.currentElement).classList.add("selected");
    }

    /**
     * Finds the list item in the browse pane associated with the specified UiElement
     * @param {UiElement} element - The element to search for
     * @returns {HTMLLIElement} - The list item element, or null
     */
    public findListItem(element: UiElement): HTMLLIElement {
        // Find the element listing
        var globalIndex = this.globalElements.indexOf(element);
        var techIndex = this.techElements.indexOf(element);

        var listItem: HTMLLIElement;
        if (globalIndex >= 0) {
            listItem = <HTMLLIElement>this.elementGlobalElementList.querySelectorAll("li")[globalIndex];
        } else if (techIndex >= 0) {
            listItem = <HTMLLIElement>this.elementTechElementList.querySelectorAll("li")[techIndex];
        }
        return listItem;
    }
}
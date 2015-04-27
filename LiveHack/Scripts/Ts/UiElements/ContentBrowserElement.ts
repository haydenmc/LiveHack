/// <reference path="../UiElement.ts" />
/// <reference path="../Data/ObservableArray.ts" />
/// <reference path="ChatElement.ts" />
/// <reference path="AnnouncementsElement.ts" />

class ContentBrowserElement extends UiElement {
    private currentElement: UiElement;
    private globalElements: ObservableArray<UiElement>;

    public get elementGlobalElementList(): HTMLElement {
        return <HTMLElement>this.htmlElement.querySelector("ul.globalElements");
    }

    public get elementContentPane(): HTMLElement {
        return <HTMLElement>this.htmlElement.querySelector("div.contentPane");
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

    constructor() {
        super("ContentBrowser");
        this.globalElements = new ObservableArray<UiElement>();
        this.globalElements.itemAdded.subscribe(this.callback_globalElementAdded);
        var globalChat = new ChatElement(this.elementContentPane, "Global Chat");
        this.globalElements.push(globalChat);
        this.globalElements.push(new AnnouncementsElement(this.elementContentPane));

        this.selectElement(globalChat);
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

        var listItem: HTMLLIElement;
        if (globalIndex >= 0) {
            listItem = <HTMLLIElement>this.elementGlobalElementList.querySelectorAll("li")[globalIndex];
        }
        return listItem;
    }
}
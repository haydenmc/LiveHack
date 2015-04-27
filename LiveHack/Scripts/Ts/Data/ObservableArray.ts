﻿class ObservableArray<T> {
    /**
     * Event handler that is fired when items are added
     */
    public itemAdded: EventHandler<{ item: T; position: number; }>;

    /**
     * Event handler that is fired when items are removed
     */
    public itemRemoved: EventHandler<{ item: T; position: number; }>;

    /**
     * Property that returns the size of the array
     */
    public get size(): number {
        return this.itemStore.length;
    }

    /**
     * Backing array store for the ObservableArray
     */
    private itemStore: Array<T>;

    /**
     * An array that fires events when items are added or removed.
     * @constructor
     */
    constructor() {
        this.itemStore = new Array<T>();
        this.itemAdded = new EventHandler<{ item: T; position: number; }>();
        this.itemRemoved = new EventHandler<{ item: T }>();
    }

    /**
     * Adds an item to the array
     */
    public push(item: T): void {
        this.itemStore.push(item);
        this.itemAdded.fire({ item: item, position: this.itemStore.length - 1 });
    }

    /**
     * Gets an item from the array at the specified index
     * @param {number} index - The index to fetch the item at
     */
    public get(index: number): T {
        return this.itemStore[index];
    }

    /**
     * Removes a specified item from the array
     * @param {T} item - The item to remove from the array
     */
    public remove(item: T): void {
        var index = this.itemStore.indexOf(item);
        if (index < 0) {
            throw "Item not found in array";
        }
        this.itemStore = this.itemStore.splice(index);
        this.itemRemoved.fire({ item: item, position: index });
    }

    /**
     * Removes the item at the specified index
     * @param {number} index - the index at which to remove the item
     */
    public removeAt(index: number): void {
        if (index > this.size - 1) {
            throw "Index outside of array bounds.";
        }
        var item = this.itemStore[index];
        this.itemStore = this.itemStore.splice(index);
        this.itemRemoved.fire({ item: item, position: index });
    }

    /**
     * Retrieves the index of the specified item in the array
     * @returns {number} - The index of the specified item, or -1
     */
    public indexOf(item: T): number {
        return this.itemStore.indexOf(item);
    }
}
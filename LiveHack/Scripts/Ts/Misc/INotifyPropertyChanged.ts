/// <reference path="EventHandler.ts" />

interface INotifyPropertyChanged {
    propertyChanged: EventHandler<string>;
}
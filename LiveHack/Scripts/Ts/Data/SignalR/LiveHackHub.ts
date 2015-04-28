/// <reference path="../Models/Message.ts" />
/// <reference path="../../../Typings/jquery.d.ts" />
/// <reference path="../../../Typings/signalr.d.ts" />

interface SignalR {
    liveHackHub: any;
}

class LiveHackHub {
    public isConnected: boolean = false;
    private dataSource: DataSource;
    private hub = $.connection.liveHackHub;

    constructor(dataSource: DataSource) {
        this.dataSource = dataSource;
        this.hub.client.hello = () => {
            alert("Hello from server.");
        };
        this.hub.client.messageReceived = (message: Message) => {
            this.dataSource.fire(DataEvent.NewMessage, message);
        }
        this.hub.client.newChatOwner = (chatId: string, user: User) => {
            this.dataSource.fire(DataEvent.NewChatOwner, {
                chatId: chatId, user: user
            });
        };
        this.hub.client.newAnnouncement = (ann: Announcement) => {
            this.dataSource.fire(DataEvent.NewAnnouncement, ann);
        };
    }

    public connect(): void {
        $.connection.hub.start().done(() => {
            this.isConnected = true;
        });
    }

    public sendMessage(message: { chatId: string; body: string }) {
        this.hub.server.sendMessage(message);
    }
}
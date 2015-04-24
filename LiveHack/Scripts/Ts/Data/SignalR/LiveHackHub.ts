/// <reference path="../Models/Message.ts" />

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
            this.dataSource.fireEvent(DataEvent.NewMessage, message);
        }
    }

    public connect(): void {
        $.connection.hub.start().done(() => {
            this.isConnected = true;
        });
    }

    public sendMessage(message: { body: string }) {
        this.hub.server.sendMessage(message);
    }
}
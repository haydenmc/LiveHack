/// <reference path="../Application.ts" />
/// <reference path="../UiElement.ts" />
class ChatElement extends UiElement {
    private callbackReceivedMessage = (msg) => {
        this.receivedMessage(msg);
    };

    constructor(parent: HTMLElement) {
        super("Chat", parent);
        Application.instance.dataSource.subscribe(DataEvent.NewMessage, this.callbackReceivedMessage);
        this.htmlElement.querySelector("input.messageInput").addEventListener('keypress',(e: KeyboardEvent) => {
            var key = e.which || e.keyCode;
            if (key === 13) { // 13 is enter
                this.sendMessage((<HTMLInputElement>this.htmlElement.querySelector("input.messageInput")).value);
                (<HTMLInputElement>this.htmlElement.querySelector("input.messageInput")).value = "";
            }
        });
    }

    public sendMessage(body: string) {
        var message = { body: body };
        Application.instance.dataSource.liveHackHub.sendMessage(message);
    }

    public receivedMessage(arg: any) {
        var message: Message = arg;
        var messageNode = document.createElement("li");
        messageNode.innerHTML = '<div class="body">' + message.body + '</div><div class="sender">' + message.sender.displayName + '</div>';
        this.htmlElement.querySelector("ul.messages").appendChild(messageNode);
    }
}
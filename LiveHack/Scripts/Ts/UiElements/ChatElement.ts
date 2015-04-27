/// <reference path="../Application.ts" />
/// <reference path="../UiElement.ts" />
class ChatElement extends UiElement {
    public chatId: string;

    private get elementMessageList(): HTMLUListElement {
        return <HTMLUListElement>this.htmlElement.querySelector("ul.messages");
    }

    private callbackReceivedMessage = (msg) => {
        this.receivedMessage(msg);
    };

    constructor(parent: HTMLElement, title?: string, chatId?: string) {
        super("Chat", parent);
        if (typeof title !== 'undefined') {
            this.title = title;
        } else {
            this.title = "Chat";
        }
        this.chatId = chatId;
        Application.instance.dataSource.subscribe(DataEvent.NewMessage, this.callbackReceivedMessage);
        this.htmlElement.querySelector("input.messageInput").addEventListener('keypress',(e: KeyboardEvent) => {
            var key = e.which || e.keyCode;
            if (key === 13) { // 13 is enter
                this.sendMessage((<HTMLInputElement>this.htmlElement.querySelector("input.messageInput")).value);
                (<HTMLInputElement>this.htmlElement.querySelector("input.messageInput")).value = "";
            }
        });

        // Fetch chat history
        Application.instance.dataSource.getChatMessages(this.chatId).then((messages) => {
            for (var i = 0; i < messages.length; i++) {
                this.insertMessage(messages[i]);
            }
        },(error) => {
                alert("Error fetching chat history: " + error);
            });
    }

    public insertMessage(message: Message) {
        // Are we scrolled to the bottom?
        var messageList = <HTMLUListElement>this.htmlElement.querySelector("ul.messages");
        var scrollToBottom = false;
        if (messageList.scrollHeight - (messageList.scrollTop + messageList.clientHeight) < 16) {
            scrollToBottom = true;
        }
        var messageNode = document.createElement("li");
        messageNode.style.borderLeftColor = ColorHasher.guidToColor(message.sender.id);
        messageNode.innerHTML = '<div class="body">' + message.body + '</div><div class="sender">' + message.sender.displayName + '</div>';
        messageList.appendChild(messageNode);
        if (scrollToBottom) {
            messageList.scrollTop = messageList.scrollHeight - messageList.clientHeight;
        }
    }

    public sendMessage(body: string) {
        var message = { chatId: this.chatId, body: body };
        Application.instance.dataSource.liveHackHub.sendMessage(message);
    }

    public receivedMessage(arg: any) {
        var message: Message = arg;
        if (message.chatId != this.chatId) {
            return;
        }
        this.insertMessage(message);
    }

    public show() {
        super.show();
        // Scroll to bottom
        this.elementMessageList.scrollTop = this.elementMessageList.scrollHeight - this.elementMessageList.clientHeight;
    }

    public destroy() {
        super.destroy();
        Application.instance.dataSource.unsubscribe(DataEvent.NewMessage, this.callbackReceivedMessage);
    }
}
/// <reference path="User.ts" />

class Message {
    public messageId: string;
    public chatId: string;
    public body: string;
    public sender: User;
    public sentDateTime: string;
}
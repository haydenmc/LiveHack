/// <reference path="models/technology.ts" />
/// <reference path="Models/Announcement.ts" />
/// <reference path="Models/Team.ts" />
/// <reference path="Models/AuthResponse.ts" />
/// <reference path="SignalR/LiveHackHub.ts" />
/// <reference path="../../Typings/es6-promise.d.ts" />
/// <reference path="JsonRequest.ts" />
/// <reference path="../Misc/EventHandler.ts" />
/// <reference path="../Misc/INotifyPropertyChanged.ts" />

enum DataEvent {
    NewMessage = 100,
    NewChatOwner = 200,
    NewAnnouncement = 300,
    NewTechnology = 400
};

class DataSource implements INotifyPropertyChanged {
    public static APIPATH = "/api";
    private _authInfo: AuthResponse;
    public liveHackHub: LiveHackHub;

    public propertyChanged: EventHandler<string>;

    private callbacks: { [eventName: number]: EventHandler<any> };

    /* Data models */
    /* User */
    public get user(): User {
        return this._user;
    }
    public set user(value: User) {
        if (this._user != value) {
            this._user = value;
            this.propertyChanged.fire("user");
        }
    }
    private _user: User;

    /* Team */
    public get team(): Team {
        return this._team;
    }
    public set team(value: Team) {
        if (this._team != value) {
            this._team = value;
            this.propertyChanged.fire("team");
        }
    }
    private _team: Team;

    constructor() {
        this.liveHackHub = new LiveHackHub(this);
        this.propertyChanged = new EventHandler<string>();
        this.callbacks = {};
    }

    public authenticate(email: string, password: string): Promise<AuthResponse> {
        return new Promise<AuthResponse>((resolve: (result: AuthResponse) => void, reject: (error) => void) => {
            JsonRequest.httpPost<AuthResponse>('/Token', { Username: email, Password: password, grant_type: "password" }).then((success) => {
                this._authInfo = success;
                resolve(success);
            },(error) => {
                    reject(error);
                });
        });
    }

    public register(email: string, displayname: string, password: string): Promise<any> {
        return new Promise<any>((resolve: (result) => void, reject: (error) => void) => {
            JsonRequest.httpPost<AuthResponse>('/api/Account/Register', { Email: email, DisplayName: displayname, Password: password }).then((success) => {
                resolve(success);
            },(error) => {
                reject(error);
            });
        });
    }

    public getUser(userId?: string): Promise<User> {
        var userUrl = "";
        if (typeof userId !== 'undefined') {
            userUrl = "/" + userId;
        }
        return new Promise<User>((resolve: (result: User) => void, reject: (error) => void) => {
            JsonRequest.httpGet<User>('/api/Account' + userUrl, this._authInfo.access_token).then((success) => {
                resolve(success);
            },(error) => {
                    reject(error);
                });
        });
    }

    public createTeam(teamName: string): Promise<Team> {
        return new Promise<Team>((resolve: (result: Team) => void, reject: (error) => void) => {
            JsonRequest.httpPost<Team>('/api/Team', { TeamName: teamName }, this._authInfo.access_token).then((success) => {
                resolve(success);
            },(error) => {
                    reject(error);
                });
        });
    }

    public joinTeam(accessCode: string): Promise<Team> {
        return JsonRequest.httpPost<Team>('/api/Team/' + accessCode + '/Join', {}, this._authInfo.access_token);
    }
    
    public getTeam(teamId?: string): Promise<Team> {
        var teamUrl = "";
        if (typeof teamId !== 'undefined') {
            teamUrl = "/" + teamId;
        }
        return new Promise<Team>((resolve: (result: Team) => void, reject: (error) => void) => {
            JsonRequest.httpGet<Team>('/api/Team' + teamUrl, this._authInfo.access_token).then((success) => {
                resolve(success);
            },(error) => {
                    reject(error);
                });
        });
    }

    public getChatMessages(chatId?: string): Promise<Array<Message>> {
        var chatUrl = "";
        if (typeof chatId !== 'undefined') {
            chatUrl = "/" + chatId;
        }
        return JsonRequest.httpGet<Array<Message>>('/api/Chat' + chatUrl + '/Messages', this._authInfo.access_token);
    }

    public getAnnouncements(): Promise<Array<Announcement>> {
        return JsonRequest.httpGet<Array<Announcement>>('/api/Announcement', this._authInfo.access_token);
    }

    public createAnnouncement(title: string, body: string): Promise<Announcement> {
        return JsonRequest.httpPost<Announcement>('/api/Announcement', { title: title, body: body }, this._authInfo.access_token);
    }

    public getTechnologies(): Promise<Array<Technology>> {
        return JsonRequest.httpGet<Array<Technology>>('/api/Technology', this._authInfo.access_token);
    }

    public createTechnology(name: string): Promise<Technology> {
        return JsonRequest.httpPost<Technology>('/api/Technology', { name: name }, this._authInfo.access_token);
    }

    public subscribe(eventName: DataEvent, callback: (arg: any) => void) {
        if (this.callbacks[eventName] == null) {
            this.callbacks[eventName] = new EventHandler<any>();
        }
        this.callbacks[eventName].subscribe(callback);
    }

    public unsubscribe(eventName: DataEvent, callback: (arg: any) => void) {
        if (this.callbacks[eventName] != null) {
            this.callbacks[eventName].unSubscribe(callback);
        }
    }

    public fire(eventName: DataEvent, arg: any) {
        var callbacks = this.callbacks[eventName];
        if (callbacks == null) {
            return;
        }
        callbacks.fire(arg);
    }
}
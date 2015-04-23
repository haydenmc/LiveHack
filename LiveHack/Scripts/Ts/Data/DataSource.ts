/// <reference path="Models/AuthResponse.ts" />
/// <reference path="SignalR/LiveHackHub.ts" />
/// <reference path="../../Typings/es6-promise.d.ts" />
/// <reference path="JsonRequest.ts" />

enum DataEvent {
    NewMessage = 100
};

class DataSource {
    public static APIPATH = "/api";
    private _authInfo: AuthResponse;
    public liveHackHub: LiveHackHub;

    private callbacks: { [eventName: number]: Array<(arg: any) => void> };

    constructor() {
        this.liveHackHub = new LiveHackHub(this);
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

    public subscribe(eventName: DataEvent, callback: (arg: any) => void) {
        if (this.callbacks[eventName] == null) {
            this.callbacks[eventName] = new Array<(arg: any) => void>();
        }
        this.callbacks[eventName].push(callback);
    }

    public unsubscribe(eventName: DataEvent, callback: (arg: any) => void) {
        if (this.callbacks[eventName] == null) {
            var index = this.callbacks[eventName].indexOf(callback);
            this.callbacks[eventName] = this.callbacks[eventName].splice(index, 1);
        }
    }

    public fireEvent(eventName: DataEvent, arg: any) {
        var callbacks = this.callbacks[eventName];
        if (callbacks == null) {
            return;
        }
        for (var i = 0; i < callbacks.length; i++) {
            callbacks[i](arg);
        }
    }
}
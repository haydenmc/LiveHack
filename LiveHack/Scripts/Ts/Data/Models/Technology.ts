/// <reference path="User.ts" />

class Technology {
    public id: string;
    public name: string;
    public description: string;
    public owners: Array<User>;
    public users: Array<User>;
    public dateTimeCreated: string;
}
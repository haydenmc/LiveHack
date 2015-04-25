/// <reference path="User.ts" />

class Team {
    public id: string;
    public name: string;
    public description: string;
    public accessCode: string;
    public owners: Array<User>;
    public dateTimeCreated: string;
}
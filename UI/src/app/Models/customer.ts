export class Customer {
    name!: string;
    currentPatchVersion!: string;
    serverIp! : string;
    lastCheckIn! : Date;
    constructor(){
        this.name = '';
        this.currentPatchVersion = '';
        this.serverIp = '';
        this.lastCheckIn =new Date();
    }
}
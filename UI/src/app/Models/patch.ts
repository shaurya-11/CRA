export class Patch {
    productId : number;
    fileName! : string;
    version! : string;
    description! : string;
    releasedOn! : string;
    constructor(){
        this.productId = 0;
        this.fileName = '';
        this.description = '';
        this.version= '';
        this.releasedOn = '';
    }
}

import { Observer } from './observer';
import { INotify } from './../../Interface/iNotify';

export class SubjectString extends Observer<string> implements INotify<string>{
    private text: string = "";

    constructor() {
        super();
    }

    notify(model: string): void {
        this.text = model;
        super.notify(this.text);
    }
}
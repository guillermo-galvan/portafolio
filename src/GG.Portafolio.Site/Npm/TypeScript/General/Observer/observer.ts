import { INotify } from './../../Interface/iNotify';

export abstract class Observer<T> implements INotify<T>{

    private observers: INotify<T>[];

    constructor() {
        this.observers = [];
    }

    subscribe(o:INotify<T>) {
        this.observers.push(o);
    }

    unsubscribe(o:any) {
        this.observers = this.observers.filter(e => e instanceof o !== true);
    }

    notify(model: T): void {

        this.observers.forEach(obs => {
            obs.notify(model);
        });
    }

}
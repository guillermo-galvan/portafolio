import { INotify } from './../../Interface/iNotify';
import { changeCaracter } from './../functions';

export class ObserverInnerHtml implements INotify<string> {

    private element: HTMLElement;

    constructor(idElement: string) {
        this.element = document.getElementById(idElement);
    }

    notify(model: string) {
        this.element.innerHTML = changeCaracter(model);
    }
}
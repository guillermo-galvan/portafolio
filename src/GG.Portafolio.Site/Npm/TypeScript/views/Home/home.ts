import { ILoadPage } from './../../Interface/iLoadPage';
import { Page } from './../../General/page';
import { TxtRotate } from './../../General/txtRotate';

export class Home extends Page implements ILoadPage {
    #txtRotate: TxtRotate;
    #link: string;

    constructor() {
        super("home");
        let link = document.getElementById("linkHome") as HTMLInputElement;
        this.#link = link.value;
    }

    async load() {
        if (!this.isLoaded) {
            await fetch(this.#link)
                .then(response => {
                    if (response.ok) {
                        return response.text();
                    }
                    else {
                        return "";
                    }
                })
                .then(result => {
                    this.page.innerHTML = result;
                    this.loadTxtRotate();
                })
                .catch(x => {
                    console.log(x);
                })
                .finally(() => this.isLoaded = true);
        }
        else {
            this.loadTxtRotate();
        }
    }

    unLoad(): void {
        if (this.isLoaded) {
            this.#txtRotate.ContinueTxtRotate = false;
        }
        
    }

    private loadTxtRotate() {

        if (!this.#txtRotate) {
            let text = this.page.querySelector(`span[class="txt-rotate color-text-red"]`);

            let toRotate: string[] = Array.from<string>(JSON.parse(text.getAttribute('data-rotate')));
            let period: string = text.getAttribute('data-period');

            if (!period) {
                period = "";
            }

            this.#txtRotate = new TxtRotate(text, toRotate, parseInt(period, 10));
        }
        

        this.#txtRotate.ContinueTxtRotate = true;
        this.#txtRotate.validateTime(this.#txtRotate);
    }
}
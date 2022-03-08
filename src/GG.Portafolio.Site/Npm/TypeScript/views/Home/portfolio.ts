import { ILoadPage } from './../../Interface/iLoadPage';
import { Page } from './../../General/page';

export class Portfolio extends Page implements ILoadPage {
    #link: string;
    constructor() {
        super("portfolio");
        let link = document.getElementById("linkPortfolio") as HTMLInputElement;
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
                })
                .catch(x => {
                    console.log(x);
                })
                .finally(() => this.isLoaded = true);
        }
    }

    unLoad(): void {
    }
}
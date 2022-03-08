import { ILoadPage } from './../../Interface/iLoadPage';
import { Page } from './../../General/page';

export class Blog extends Page implements ILoadPage {
    #link: string;

    constructor() {
        super("blog");
        let link = document.getElementById("linkBlog") as HTMLInputElement;
        this.#link = link.value;
    }

    async load() {
        if (!this.isLoaded) {
            this.isLoaded = true
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
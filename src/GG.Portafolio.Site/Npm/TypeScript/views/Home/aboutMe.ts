import { ILoadPage } from './../../Interface/iLoadPage';
import { Page } from './../../General/page';

export class AboutMe extends Page implements ILoadPage {
    #link: string;
    #scrtipLoad: boolean = false;
    constructor() {
        super("aboutme");
        let link = document.getElementById("linkAboutMe") as HTMLInputElement;
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
                    this.execScript();
                })
                .catch(x => {
                    console.log(x);
                })
                .finally(() => this.isLoaded = true);
        }
        else if (!this.#scrtipLoad) {
            this.execScript();
        }

    }

    unLoad(): void {

    }

    private execScript() {
        let script = <HTMLScriptElement>document.getElementById("scriptViewAboutMe");
        if (script) {
            try {
                eval(script.text);
            } catch (e) {
                console.error("Error:", e);
            } finally {
                this.#scrtipLoad = true;
            }
        }
    }
}
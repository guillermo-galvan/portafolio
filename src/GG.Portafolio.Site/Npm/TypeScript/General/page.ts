export abstract class Page {
    #page: Element;
    #menu: Element;
    #isloaded: boolean;

    constructor(id: string) {
        this.#page = document.getElementById(id);
        this.#menu = document.querySelector(`a[href="#${id}"]`);
        this.#isloaded = false;
    }

    get page(): Element {
        return this.#page;
    }

    get menu(): Element {
        return this.#menu;
    }

    set isLoaded(value: boolean) {
        this.#isloaded = value;
    }

    get isLoaded() {
        return this.#isloaded;
    }
}
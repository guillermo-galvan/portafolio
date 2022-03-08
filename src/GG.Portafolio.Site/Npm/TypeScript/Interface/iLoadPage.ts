export interface ILoadPage {
    page: Element;
    menu: Element;
    isLoaded: boolean;
    load(): void;
    unLoad(): void;
}
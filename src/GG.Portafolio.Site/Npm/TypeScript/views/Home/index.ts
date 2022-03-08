import { PageChange } from './PageChange';

window.onload = function load() {
    let view = document.getElementById("loadedView") as HTMLInputElement;
    PageChange.Init(parseInt(view.value));
};